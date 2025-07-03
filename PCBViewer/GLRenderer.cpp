#include "GLRenderer.h"
#include "GLRenderEngine.h"

GLRenderer::GLRenderer()
{
}

GLRenderer::~GLRenderer()
{
}

void GLRenderer::SetViewPort(const int x, const int y, const int width, const int height)
{
	m_viewPort.x = x;
	m_viewPort.y = x;
	m_viewPort.width = width;
	m_viewPort.height = height;
}

void GLRenderer::SetClearColor(const float r, const float g, const float b, const float a)
{
	m_v4ClearColor = Vec4(r, g, b, a);
}

void GLRenderer::Render(std::vector<DrawObjectPtr>& model)
{
	glViewport(m_viewPort.x, m_viewPort.y, m_viewPort.width, m_viewPort.height);

	for (auto& pModel : model)
	{
		RenderDataPtr pRenderData = m_RenderCache->GetOrCreateRenderData(pModel);

		if (pRenderData)
		{
			m_RenderEngine->DrawRenderData(pRenderData, Mat4(1.f));
		}
	}
}


void GLRenderer::Update(float deltaTime)
{
}
