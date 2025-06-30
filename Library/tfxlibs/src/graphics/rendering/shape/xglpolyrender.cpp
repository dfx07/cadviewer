#include "graphics/rendering/shape/xglpolyrender.h"
#include "graphics/rendering/shader/xglshader.h"

#include "graphics/rendering/OpenGL/glew.h"

__BEGIN_NAMESPACE__

GLPolyRender::GLPolyRender()
{
}

GLPolyRender::~GLPolyRender()
{
	ReleaseBuffer();
}

bool GLPolyRender::CreateShader()
{
	auto pGLProgram = std::make_shared<GLShaderProgram>();

	std::unordered_map<ShaderStage, std::string> shaderSrc;
	shaderSrc[ShaderStage::Vertex]   = "shaders/shape/poly.vert";
	shaderSrc[ShaderStage::Fragment] = "shaders/shape/poly.frag";
	shaderSrc[ShaderStage::Geometry] = "shaders/shape/poly.geom";

	if (!pGLProgram->LoadShaders(shaderSrc))
	{
		assert(0);

		return false;
	}

	m_pProgram = pGLProgram;
	m_pBinder = std::make_shared<GLShaderDataBinder>(pGLProgram->GetProgramID());

	return true;
}

bool GLPolyRender::CreateBuffers()
{
	glGenVertexArrays(1, &m_nVao);
	glBindVertexArray(m_nVao);

	glGenBuffers(1, &m_nVbo);
	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	glBufferData(GL_ARRAY_BUFFER, m_vecRenderData.size() * sizeof(PolyVertexData), m_vecRenderData.data(), GL_STATIC_DRAW);

	glGenBuffers(1, &m_nEbo);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEbo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, m_vecIndices.size() * sizeof(GLuint), m_vecIndices.data(), GL_STATIC_DRAW);

	// Layout
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(PolyVertexData), (void*)offsetof(PolyVertexData, position));
	glEnableVertexAttribArray(0);

	glVertexAttribPointer(1, 4, GL_FLOAT, GL_FALSE, sizeof(PolyVertexData), (void*)offsetof(PolyVertexData, color));
	glEnableVertexAttribArray(1);

	glVertexAttribPointer(2, 1, GL_FLOAT, GL_FALSE, sizeof(PolyVertexData), (void*)offsetof(PolyVertexData, thickness));
	glEnableVertexAttribArray(2);

	glVertexAttribPointer(3, 1, GL_INT, GL_FALSE, sizeof(PolyVertexData), (void*)offsetof(PolyVertexData, polygonID));
	glEnableVertexAttribArray(3);

	glBindVertexArray(0);

	return true;
}

void GLPolyRender::UpdateVertexBuffer()
{
	if (m_nVao == 0 || m_nEbo == 0 || m_nVbo == 0)
	{
		ReleaseBuffer();
		if (CreateBuffers() == false)
			return;
	}

	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	if (m_bReloadBufferFlag)
	{
		// Dung lượng thay đổi → cấp phát lại
		glBufferData(GL_ARRAY_BUFFER, m_vecRenderData.size() * sizeof(PolyVertexData), m_vecRenderData.data(), GL_DYNAMIC_DRAW);
	}
	else
	{
		// Kích thước không đổi → update nhanh
		glBufferSubData(GL_ARRAY_BUFFER, 0, m_vecRenderData.size() * sizeof(PolyVertexData), m_vecRenderData.data());
	}

	glBindVertexArray(m_nVao);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEbo);

	if (m_bReloadIndexFlags)
	{
		// Dung lượng thay đổi → cấp phát lại
		glBufferData(GL_ELEMENT_ARRAY_BUFFER, m_vecIndices.size() * sizeof(GLuint), m_vecIndices.data(), GL_DYNAMIC_DRAW);
	}
	else
	{
		// Kích thước không đổi → update nhanh
		glBufferSubData(GL_ELEMENT_ARRAY_BUFFER, 0, m_vecIndices.size() * sizeof(GLuint), m_vecIndices.data());
	}

	glBindVertexArray(0);

	m_bReloadBufferFlag = false;
	m_bReloadIndexFlags = false;
}

void GLPolyRender::ReleaseBuffer()
{
	if (m_nVbo)
		glDeleteBuffers(1, &m_nVbo);

	if (m_nEbo)
		glDeleteBuffers(1, &m_nEbo);

	if (m_nVao)
		glDeleteVertexArrays(1, &m_nVao);
}

void GLPolyRender::Draw(const Mat4& view, const Mat4& proj, const Vec2& viewport, const float& zoom /*= 1.f*/)
{
	if (!BindShader())
		return;

	if (m_vecRenderData.empty())
		return;

	m_pBinder->SetMat4("u_Proj", tfx::ValuePtr(proj));
	m_pBinder->SetMat4("u_View", tfx::ValuePtr(view));
	m_pBinder->SetMat4("u_Model", tfx::ValuePtr(m_matModel));
	m_pBinder->SetVec2("u_Viewport", tfx::ValuePtr(viewport));
	m_pBinder->SetFloat("u_zZoom", zoom);

	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glDisable(GL_MULTISAMPLE);
	glDepthMask(GL_FALSE);
	//glDisable(GL_DEPTH_TEST);

	glBindVertexArray(m_nVao);
	glDrawElements(GL_LINES_ADJACENCY, m_vecRenderData.size() * 4, GL_UNSIGNED_INT, 0);

	glEnable(GL_MULTISAMPLE);
	glEnable(GL_DEPTH_TEST);

	UnbindShader();
}

bool GLPolyRender::BindShader()
{
	if (m_pProgram == nullptr)
		return false;

	return m_pProgram->Use();
}

void GLPolyRender::UnbindShader()
{
	if (m_pProgram == nullptr)
		return;

	m_pProgram->UnUse();
}

void GLPolyRender::Remake()
{
	// TODO: Implement this method if needed
}

void GLPolyRender::Update(float deltaTime)
{

}

__END_NAMESPACE__


