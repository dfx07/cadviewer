#include "PCBView.h"
#include "graphics/camera/xcamera.h"
#include "graphics/rendering/xopenglrenderer.h"


#include "PolyObjectDrawer.h"
#include <random>


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

int RandomInt(int min, int max)
{
	static std::mt19937 rng(std::random_device{}());
	std::uniform_int_distribution<int> dist(min, max);
	return dist(rng);
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
	m_pCamera->SetCamera({ 0.f, 0.f, 200.f }, { 0.f, 0.f, 1.f }, { 0.f, 1.f, 0.f });
	m_pCamera->SetDistPlane(0.f, 1000.f);

	m_pRenderer = std::make_shared<tfx::OpenGLRenderer>(m_pCamera);
	m_pRenderer->SetContext(m_pContext);
	m_pRenderer->SetClearColor(1.0f, 1.0f, 1.0f, 1.0f); // Default clear color is white

	m_pModelManager = std::make_shared<RenderModelManager>();

	m_polys = std::make_shared<PolyDrawObjectList>();
	auto poly = m_polys->CreatePolyDrawObject();

	poly->m_clColor = Col4(1.f, 0.f, 0.f, 1.f);
	poly->m_fThickness = 2.f;
	poly->m_vecPoints.push_back({ -100.f, 100.f });
	poly->m_vecPoints.push_back({ 50.f, 100.f });
	poly->m_vecPoints.push_back({ 100.f, -100.f });
	poly->m_vecPoints.push_back({ 150.f, -100.f });
	poly->m_vecPoints.push_back({ 150.f, -150.f });
	poly->m_vecPoints.push_back({ 120.f, -150.f });
	poly->m_vecPoints.push_back({ 180.f, -200.f });
	poly->m_vecPoints.push_back({ -100.f, -100.f });

	auto poly2 = m_polys->CreatePolyDrawObject();

	poly2->m_clColor = Col4(0.f, 0.f, 0.f, 1.f);
	poly2->m_fThickness = 1.f;
	poly2->m_vecPoints.push_back({ -200.f, 200.f });
	poly2->m_vecPoints.push_back({ -150.f, 200.f });
	poly2->m_vecPoints.push_back({ -150.f, 100.f });
	poly2->m_vecPoints.push_back({ -200.f, 100.f });

	for (int i = 0; i < 10; i++)
	{
		auto pNPoly = std::dynamic_pointer_cast<PolyDrawObject>(poly2->Clone());

		float x = RandomInt(100, 200);
		float y = RandomInt(-300, 100);

		if (pNPoly != nullptr)
		{
			pNPoly->Move(Vec2(x, y));

			m_polys->AddPolyDrawObject(pNPoly);
		}
	}


	int nID = m_pModelManager->AddModel(m_polys);

	m_polyDrawer = std::make_shared<PolyObjectDrawer>(m_pModelManager, nID);
	m_polyDrawer->CreateShader();
	m_polyDrawer->CreateBuffers();

	m_pRenderer->AddObjectRenderable(m_polyDrawer);

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

void PCBView::Draw()
{
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

void PCBView::OnPaint()
{
	Draw();
}

void PCBView::OnSizeChanged(const int nWidth, const int nHeight)
{
	m_nWidth = nWidth;
	m_nHeight = nHeight;

	UpdateView();
}

void PCBView::OnMouseEnter(TFXEvent* event)
{
	int a = 10;
}

void PCBView::OnMouseMove(TFXMouseEvent* event)
{
	if (m_ePCBViewState == EPCBViewState::move)
	{
		HandleMoveView(event->m_Pt);

		m_ptLastMouse = event->m_Pt;
	}
}

void PCBView::OnMouseDown(TFXMouseEvent* event)
{
	m_polyDrawer->Remake();

	if (m_ePCBViewState == EPCBViewState::none)
	{
		if (event->m_Button == TFXMouseButton::MID)
		{
			m_ePCBViewState = EPCBViewState::move;
			m_ptMouseLastClick = event->m_Pt;
			m_ptLastMouse = event->m_Pt;
		}
	}
}

void PCBView::OnMouseUp(TFXMouseEvent* event)
{
	if (m_ePCBViewState == EPCBViewState::move)
	{
		if (event->m_Button == TFXMouseButton::MID)
		{
			m_ePCBViewState = EPCBViewState::none;
		}
	}
}

void PCBView::OnMouseDoubleClick(TFXMouseEvent* event)
{
	int a = 10;
}

void PCBView::OnMouseWheel(TFXMouseEvent* event)
{
	float zDelta = event->m_Delta;

	m_pCamera->ZoomTo({ event->m_Pt.x, event->m_Pt.y, 0 }, zDelta);
	m_pCamera->UpdateMatrix();

	OnPaint();
}

void PCBView::OnMouseDragDrop(TFXMouseEvent* event)
{
	int a = 10;
}

void PCBView::OnKeyDown(TFXKeyEvent* event)
{
	int a = 10;
}

void PCBView::OnKeyUp(TFXKeyEvent* event)
{
	int a = 10;
}

bool PCBView::HandleMoveView(TFX_DevicePt ptNew)
{
	tfx::Vec2 ptNewReal = m_pCamera->Screen2WorldPoint({ ptNew.x, ptNew.y, 0 });
	tfx::Vec2 ptOldReal = m_pCamera->Screen2WorldPoint({ m_ptLastMouse.x, m_ptLastMouse.y, 0 });

	tfx::Vec2 vOffset = ptNewReal - ptOldReal;

	m_pCamera->Move(tfx::Vec3(vOffset.x, vOffset.y, 0));
	m_pCamera->UpdateMatrix();

	OnPaint();

	return true;
}

/*
* This function is called to receive the state of control keys (Ctrl, Alt, Shift).
 * The keyFlags parameter can be a combination of MK_CONTROL, MK_SHIFT, and MK_ALT.
 */
void PCBView::OnReceiveCtrlKeyState(const int keyFlags)
{
	m_nCtrlKeyFlags = keyFlags;
}

bool PCBView::IsCtrlKeyPressed(int key) const
{
	if (m_pCallbackUI != nullptr)
	{
		m_pCallbackUI("GetCtrlKeyPressed", 0, key);
	}

	return m_nCtrlKeyFlags & key;
}

DeviceContextPtr PCBView::GetContext() const
{
	return m_pContext;
}


