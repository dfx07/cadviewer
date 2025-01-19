#include "PCBView.h"


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

	if (IsAlreadyContext())
		DeleteContext();

	DeviceContextConfig config;
	config.UseOpenGLExtension(ctx_conf.m_bUseContextExt ? true : false);
	config.SetAntiliasingLevel(static_cast<int>(ctx_conf.m_nAntialiasingLevel));

	m_pContext = new OpenGLDeviceContext(config);

	if (!m_pContext->CreateContext(m_hHandle))
	{
		delete m_pContext;
		m_pContext = NULL;
		return false;
	}

	return true;
}

void PCBView::DeleteContext()
{
	if (m_pContext == NULL)
		return;

	m_pContext->DeleteContext();

	delete m_pContext;
	m_pContext = NULL;
}

bool PCBView::IsAlreadyContext() const
{
	return (m_pContext != nullptr && m_pContext->IsValid());
}

void PCBView::MakeContext() const
{
	m_pContext->MakeCurrentContext();
}

void PCBView::SetView(const int nWidth, const int nHeight)
{
	m_nWidth = nWidth;
	m_nHeight = nHeight;

	MakeContext();

	if (IsAlreadyContext())
	{
		glViewport(0, 0, m_nWidth, m_nHeight);
		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();

		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();
	}
}

void PCBView::Clear()
{
	glClearColor(1.0, 0.0, 1.0, 1.0);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
}

void PCBView::Draw()
{
	MakeContext();

	glPointSize(10.0);
	glColor3f(0, 1, 0);
	glBegin(GL_POINTS);
		glVertex2f(0.f, 0.f);
	glEnd();

	m_pContext->SwapBuffer();
}

DeviceContext* PCBView::GetContext() const
{
	return m_pContext;
}
