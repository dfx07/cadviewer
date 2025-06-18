#include "graphics/rendering/shape/xpolyrender.h"
#include "graphics/rendering/shader/xglshader.h"

#include "graphics/rendering/OpenGL/glew.h"

__BEGIN_NAMESPACE__

PolyRender::PolyRender()
{
}

PolyRender::~PolyRender()
{

}

bool PolyRender::CreateShader()
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

void PolyRender::Draw()
{
	if (m_pProgram == nullptr)
		return;
	if (!BindShader())
		return;
	if (m_vecRenderData.empty())
		return;
	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glEnableClientState(GL_VERTEX_ARRAY);
	glVertexPointer(3, GL_FLOAT, 0, m_vecRenderData.data());
	glDrawArrays(GL_TRIANGLES, 0, static_cast<GLsizei>(m_vecRenderData.size() / 3));
	glDisableClientState(GL_VERTEX_ARRAY);


	GL_LINES_ADJACENCY_EXT
}

bool PolyRender::BindShader()
{
	if (m_pProgram == nullptr)
		return false;

	return m_pProgram->Use();
}

void PolyRender::UnbindShader()
{
	if (m_pProgram == nullptr)
		return;

	m_pProgram->UnUse();
}

void PolyRender::Update(float deltaTime)
{

}

__END_NAMESPACE__


