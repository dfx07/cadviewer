#include "PCBView.h"
#include "graphics/rendering/xopenglrenderer.h"
#include "graphics/camera/xcamera.h"


PCBView::PCBView() : NotifyObject(),
	m_hHandle(NULL),
	m_nHeight(0),
	m_nWidth(0)
{

}

PCBView::~PCBView()
{
	DeleteContext();
}

bool PCBView::Create(HWND hWnd, const int nWidth, const int nHeight)
{
	m_hHandle = hWnd;

	m_nWidth = nWidth;
	m_nHeight = nHeight;

	return true;
}

void PCBView::Destroy()
{
	DeleteContext();
}

bool PCBView::CreateContext(ContextConfig ctx_conf)
{
	if (m_hHandle == NULL)
		return false;

	if (m_pContext != nullptr && m_pContext->IsValid())
		DeleteContext();

	DeviceContextConfig config;
	config.UseOpenGLExtension(ctx_conf.m_bUseContextExt ? true : false);
	config.SetAntiliasingLevel(static_cast<int>(ctx_conf.m_nAntialiasingLevel));

	m_pContext = std::make_shared<OpenGLDeviceContext>(config);
	if (!m_pContext->CreateContext(m_hHandle))
	{
		m_pContext.reset();
		return false;
	}

	// create camera
	m_pCamera = std::make_shared<tfx::Camera2D>();
	m_pCamera->SetCamera({ 0.f, 0.f, 8.f }, { 0.f, 0.f, -1.f }, { 0.f, 1.f, 0.f });
	m_pCamera->SetDistPlane(0, -1000.f);

	m_pRenderer = std::make_shared<tfx::OpenGLRenderer>(m_pCamera);
	m_pRenderer->SetContext(m_pContext);
	m_pRenderer->SetClearColor(1.0f, 1.0f, 1.0f, 1.0f); // Default clear color is white

	UpdateView();

	return true;
}

void PCBView::DeleteContext()
{
	if (m_pContext == NULL)
		return;

	m_pContext->DeleteContext();

	m_pContext.reset();
	m_pContext = nullptr;
}

void PCBView::SetView(const int nWidth, const int nHeight)
{
	m_nWidth = nWidth;
	m_nHeight = nHeight;
}

void PCBView::Draw()
{
	//glPointSize(10.0);
	//glColor3f(0, 1, 0);
	//glBegin(GL_POINTS);
	//	glVertex2f(0.f, 0.f);
	//glEnd();

	//glBegin(GL_LINES);
	//	glVertex2f(-0.5f, -0.5f);
	//	glVertex2f(0.5f, 0.5f);
	//glEnd();

	//m_pContext->SwapBuffer();

	m_pRenderer->Render();
}

void PCBView::UpdateView()
{
	if (m_pCamera)
	{
		m_pCamera->SetView(m_nWidth, m_nHeight);
		m_pCamera->UpdateMatrix();
	}

	if (m_pRenderer)
		m_pRenderer->SetViewPort(0, 0, m_nWidth, m_nHeight);
}

void PCBView::OnMouseEnter(TFXEvent* event)
{
	int a = 10;
}

void PCBView::OnMouseMove(TFXMouseEvent* event)
{
	int a = 10;
}

void PCBView::OnMouseDown(TFXMouseEvent* event)
{
	int a = 10;
}

void PCBView::OnMouseUp(TFXMouseEvent* event)
{
	int a = 10;
}

void PCBView::OnMouseDoubleClick(TFXMouseEvent* event)
{
	int a = 10;
}

void PCBView::OnMouseWheel(TFXMouseEvent* event)
{
	int a = 10;
}

DeviceContextPtr PCBView::GetContext() const
{
	return m_pContext;
}


