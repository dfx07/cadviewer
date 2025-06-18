#include "graphics/rendering/shape/xpolyrender.h"
#include "graphics/rendering/shader/xglshader.h"

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

	const std::unordered_map<ShaderStage, std::string> shaderSrc;

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


