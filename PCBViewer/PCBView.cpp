#include "PCBView.h"
#include "graphics/camera/xcamera.h"
#include "GLRenderer.h"
#include "PolyDrawObject.h"
#include "LineDrawObject.h"
#include "CircleDrawObject.h"
#include "RectDrawObject.h"
#include "Renderer.h"

#include "common/tfxutil.h"

#include <random>
#include <graphics/rendering/xopenglctx.h>
#include <string>


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
	m_pCamera->SetCamera({ 0.f, 0.f, 10000.f }, { 0.f, 0.f, 1.f }, { 0.f, 1.f, 0.f });
	m_pCamera->SetDistPlane(0.f, 10000.f);

	//m_pRenderer = std::make_shared<tfx::OpenGLRenderer>(m_pCamera);
	m_pRenderer = std::make_shared<GLRenderer>(m_pCamera);
	m_pRenderer->SetContext(m_pContext);
	m_pRenderer->SetClearColor(1.0f, 1.0f, 1.0f, 1.0f); // Default clear color is white

	//m_pModelManager = std::make_shared<RenderModelManager>();

	m_polys = std::make_shared<PolyDrawObjectList>();


	auto poly2 = m_polys->CreatePolyDrawObject();

	poly2->m_clColor = Col4(0.f, 0.f, 0.f, 1.f);
	poly2->m_fThickness = 1.f;
	poly2->m_vecPoints.push_back({ -200.f, 200.f });
	poly2->m_vecPoints.push_back({ -150.f, 200.f });
	poly2->m_vecPoints.push_back({ -150.f, 100.f });
	poly2->m_vecPoints.push_back({ -200.f, 100.f });
	poly2->SetObjectID(1);

	//for (int i = 0; i < 20; i++)
	//{
	//	auto pNPoly = std::dynamic_pointer_cast<PolyDrawObject>(poly2->Clone());

	//	float x = (float)RandomInt(-100, 300);
	//	float y = (float)RandomInt(-100, 200);

	//	if (pNPoly != nullptr)
	//	{
	//		pNPoly->Move(Vec2(x, y));

	//		m_polys->AddPolyDrawObject(pNPoly);
	//	}
	//}

	auto poly = m_polys->CreatePolyDrawObject();

	poly->m_clColor = Col4(1.f, 0.f, 0.f, 1.f);
	poly->m_fThickness = 1.f;
	poly->m_vecPoints.push_back({ -100.f, 100.f });
	poly->m_vecPoints.push_back({ 50.f, 100.f });
	poly->m_vecPoints.push_back({ 100.f, -100.f });
	poly->m_vecPoints.push_back({ 150.f, -100.f });
	poly->m_vecPoints.push_back({ 150.f, -150.f });
	poly->m_vecPoints.push_back({ 120.f, -150.f });
	poly->m_vecPoints.push_back({ 250.f, -200.f });
	poly->m_vecPoints.push_back({ 100.f, -150.f });
	poly->m_vecPoints.push_back({ 90.f, -180.f });
	poly->m_vecPoints.push_back({ 95.f, -130.f });
	poly->m_vecPoints.push_back({ -100.f, -100.f });
	poly2->SetObjectID(2);

	//int nID = m_pModelManager->AddModel(m_polys);

	// ********************************************************************
	// Lines 
	m_lines = std::make_shared<LineDrawObjectList>();

	LineDrawObjectPtr pLine = m_lines->CreateLineDrawObject();

	pLine->m_ptS = { 60, 100 };
	pLine->m_ptE = { 100, -100 };
	pLine->m_clColor = Col4(0.f, 0.f, 0.f, 1.f);
	pLine->m_fThickness = 1.f;
	pLine->Move({ 0, 300 });

	LineDrawObjectPtr pLine2 = m_lines->CreateLineDrawObject();

	pLine2->m_ptS = { -100, 100 };
	pLine2->m_ptE = { -100, -50 };
	pLine2->m_clColor = Col4(0.f, 0.f, 0.f, 1.f);
	pLine2->m_fThickness = 2.f;

	pLine2->Move({ 0, 300 });

	// ********************************************************************
	// Circles 
	m_circles = std::make_shared<CircleDrawObjectList>();

	CircleDrawObjectPtr pCircle = m_circles->CreateCircleDrawObject();

	pCircle->m_ptCenter = { 150, 100 };
	pCircle->m_fRadius = 50.f;
	pCircle->m_fThickness = 1.f;
	pCircle->m_clThicknessColor = Col4(0.f, 0.f, 0.f, 1.f);
	pCircle->m_clFillColor = Col4(0.f, 1.f, 0.f, 1.f);


	CircleDrawObjectPtr pCircle2 = m_circles->CreateCircleDrawObject();

	pCircle2->m_ptCenter = { 192.24, 233.98 };
	pCircle2->m_fRadius = 2.f;
	pCircle2->m_fThickness = 1.f;
	pCircle2->m_clThicknessColor = Col4(0.f, 0.f, 0.f, 1.f);
	pCircle2->m_clFillColor = Col4(0.f, 1.f, 0.f, 1.f);

	CircleDrawObjectPtr pCircle3 = m_circles->CreateCircleDrawObject();

	pCircle3->m_ptCenter = { 315.12,147.94 };
	pCircle3->m_fRadius = 2.f;
	pCircle3->m_fThickness = 1.f;
	pCircle3->m_clThicknessColor = Col4(0.f, 0.f, 0.f, 1.f);
	pCircle3->m_clFillColor = Col4(0.f, 1.f, 0.f, 1.f);


	CircleDrawObjectPtr pCircle4 = m_circles->CreateCircleDrawObject();

	pCircle4->m_ptCenter = { 257.75, 66.02 };
	pCircle4->m_fRadius = 2.f;
	pCircle4->m_fThickness = 1.f;
	pCircle4->m_clThicknessColor = Col4(0.f, 0.f, 0.f, 1.f);
	pCircle4->m_clFillColor = Col4(0.f, 1.f, 0.f, 1.f);

	//for (int i = 0; i < 10000; i++)
	//{
	//	auto pNCircle = std::dynamic_pointer_cast<CircleDrawObject>(pCircle->Clone());

	//	float x = (float)RandomInt(-2000, 2000);
	//	float y = (float)RandomInt(-2000, 2000);

	//	if (pNCircle != nullptr)
	//	{
	//		pNCircle->Move(Vec2(x, y));

	//		m_circles->Add(pNCircle);
	//	}
	//}


	// ********************************************************************
	// Rectangles
	m_rects = std::make_shared<RectDrawObjectList>();
	RectDrawObjectPtr pRect = m_rects->CreateRectDrawObject();

	pRect->m_ptX = 150.f;
	pRect->m_ptY = 100.f;
	pRect->m_fWidth = 150.f;
	pRect->m_fHeight = 100.f;
	pRect->m_fAngle = tfx::Deg2Rad(35.0);
	pRect->m_fThickness = 1.f;

	pRect->m_clThicknessColor = Col4(0.f, 0.f, 0.f, 1.f);
	//pRect->m_clFillColor = Col4(1.f, 0.f, 0.f, 1.f);
	pRect->SetObjectID(3);

	//RectDrawObjectPtr pRect2 = m_rects->CreateRectDrawObject();

	//pRect2->m_ptX = 150.f;
	//pRect2->m_ptY = 100.f;
	//pRect2->m_fWidth = 150.f;
	//pRect2->m_fHeight = 100.f;
	//pRect2->m_fAngle = tfx::Deg2Rad(35.0);
	//pRect2->m_fThickness = 1.f;

	//pRect->m_clThicknessColor = Col4(0.f, 0.f, 0.f, 1.f);




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
	std::vector<DrawObjectPtr> model;

	//model.push_back(m_polys);
	//model.push_back(m_lines);
	model.push_back(m_circles);
	model.push_back(m_rects);

	m_pRenderer->Render(model);
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
		m_rects->MarkDirty(1);

		HandleMoveView(event->m_Pt);

		m_ptLastMouse = event->m_Pt;
	}


	tfx::Vec2 ptNewReal = m_pCamera->Screen2WorldPoint({ event->m_Pt.x, event->m_Pt.y, 0 });
	std::wstring msg = L"World move: ";

	msg += std::to_wstring(ptNewReal.x) + L", " + std::to_wstring(ptNewReal.y) + L"\n";

	OutputDebugString(msg.c_str());
}

void PCBView::OnMouseDown(TFXMouseEvent* event)
{
	//m_polyDrawer->Remake();

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

	m_pCamera->Move(tfx::Vec3(-vOffset.x, -vOffset.y, 0));
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


