#include "GLRenderer.h"
#include "GLRenderEngine.h"
#include "RenderCache.h"
#include "GLRenderDataBuilder.h"
#include "RenderContext.h"

#include "graphics/camera/xcamera.h"
#include "graphics/rendering/OpenGL/glew.h"

#include "DrawObject.h"



GLRenderer::GLRenderer(tfx::CameraPtr pCamera)
	: Renderer(pCamera)
{
	m_RenderBuilder = std::make_shared<GLRenderDataBuilder>();
	m_RenderCache = std::make_shared<RenderCache>(m_RenderBuilder);

	m_RenderEngine = std::make_shared<GLRenderEngine>();
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
	if (!m_pContext->IsValid())
		return;

	if (!m_pContext->MakeCurrentContext())
		return;

	glClearColor(m_v4ClearColor.r, m_v4ClearColor.g, m_v4ClearColor.b, m_v4ClearColor.a);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glEnable(GL_BLEND);
	glEnable(GL_DEPTH_TEST);

	glViewport(m_viewPort.x, m_viewPort.y, m_viewPort.width, m_viewPort.height);

	RenderContextPtr pRenderContext = std::make_shared<RenderContext>();

	pRenderContext->m_Proj = m_pCamera->GetProjMatrix();
	pRenderContext->m_View = m_pCamera->GetViewMatrix();
	pRenderContext->m_ViewPort = m_pCamera->GetView();
	pRenderContext->m_Zoom = m_pCamera->GetZoom();

	for (auto& pDrawObj : model)
	{
		RenderDataPtr pRenderData = m_RenderCache->GetOrCreateRenderData(pDrawObj);

		auto pMaterial = pDrawObj->GetComponent<MaterialComponent>();
		auto pTransform = pDrawObj->GetComponent<TransformComponent>();

		if (pRenderData)
		{
			m_RenderEngine->DrawRenderData(pRenderData, pRenderContext, pMaterial, pTransform);
		}
	}

	m_pContext->SwapBuffer();
}


void GLRenderer::Update(float deltaTime)
{
}
