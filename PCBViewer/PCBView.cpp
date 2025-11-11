#include "PCBView.h"

#include <random>
#include <string>
#include <iostream>
#include <chrono>

#include "graphics/camera/xcamera.h"
#include "graphics/rendering/xopenglctx.h"

#include "GLRenderer.h"
#include "PolyDrawObject.h"
#include "LineDrawObject.h"
#include "CircleDrawObject.h"
#include "RectDrawObject.h"
#include "TriangleDrawObject.h"
#include "Renderer.h"

#include "core/tfx_utils.h"
#include "core/tfx_type.h"


#undef min
#undef max


#include "freetype/ft2build.h"
#include FT_FREETYPE_H

#include "msdfgen/msdfgen.h"
#include "msdfgen/msdfgen-ext.h"


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

	//msdfgen::FreetypeHandle* ft = msdfgen::initializeFreetype();
	//if (!ft) {
	//	printf("Failed to init FreeType\n");
	//	return -1;
	//}

	//FT_Library oft;
	//if (FT_Init_FreeType(&oft)) {
	//	std::cerr << "Không thể khởi tạo FreeType Library" << std::endl;
	//	return -1;
	//}

	//FT_Face face;
	//if (FT_New_Face(oft, "F:\\cad_viewer\\PCBViewer\\JetBrainsMonoNL-Regular.ttf", 0, &face)) {
	//	std::cerr << "Không thể load font!" << std::endl;
	//	return -1;
	//}

	//msdfgen::FontHandle* font = msdfgen::adoptFreetypeFont(face);
	//if (!font) {
	//	printf("Failed to load font\n");
	//	//deinitializeFreetype(ft);
	//	return -1;
	//}
	//auto start = std::chrono::high_resolution_clock::now();

	//// Bắt đầu duyệt toàn bộ ký tự
	//FT_UInt glyphIndex;
	//FT_ULong charCode = FT_Get_First_Char(face, &glyphIndex);

	//int count = 0;
	//while (glyphIndex != 0) {
	//	count++;

	//	// In ra codepoint và glyph index
	//	std::cout << "Char: U+" << std::hex << charCode
	//		<< "  GlyphIndex: " << std::dec << glyphIndex << std::endl;

	//	// (tuỳ chọn) Load glyph và xuất MSDF:
	//	msdfgen::Shape shape;
	//	if (loadGlyph(shape, font, glyphIndex)) {
	//		shape.normalize();
	//		// ở đây bạn có thể gọi generateMSDF(...)
	//	}

	//	// Lấy ký tự kế tiếp
	//	charCode = FT_Get_Next_Char(face, charCode, &glyphIndex);
	//}
	//auto end = std::chrono::high_resolution_clock::now();

	//auto duration = std::chrono::duration_cast<std::chrono::milliseconds>(end - start).count();

	//std::cout << "Thời gian thực thi: " << duration << " ms\n";

	////msdfgen::Shape shape;
	////if (loadGlyph(shape, font, 0x00E1)) {
	////	shape.normalize();

	////	// Kích thước MSDF
	////	int width = 64, height = 64;
	////	double range = 2.0;
	////	double scale = 1.0;
	////	msdfgen::Vector2 translate(0.0, 0.0);

	////	msdfgen::Bitmap<float, 3> msdf(width, height);
	////	generateMSDF(msdf, shape, range, scale, translate);

	////	// Xuất ra file .png nếu bạn có stb_image_write.h
	////	// hoặc tự xử lý dữ liệu msdf(x,y)[channel]

	////	msdfgen::savePng(msdf, "glyph_A.png");

	////}

	//destroyFont(font);
	//deinitializeFreetype(ft);

	// create camera
	m_pCamera = std::make_shared<Camera2D>();
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
	pLine2->m_fThickness = 1.f;

	pLine2->Move({ 0, 300 });

	LineDrawObjectPtr pLine3 = m_lines->CreateLineDrawObject();

	pLine3->m_ptS = { 0, 100 };
	pLine3->m_ptE = { 100, 0 };
	pLine3->m_clColor = Col4(0.f, 0.f, 0.f, 1.f);
	pLine3->m_fThickness = 1.f;

	pLine3->Move({ 0, 200 });

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
	m_rects->SetObjectID(3);

	RectDrawObjectPtr pRect = m_rects->CreateRectDrawObject();

	pRect->m_ptX = 150.f;
	pRect->m_ptY = 100.f;
	pRect->m_fWidth = 150.f;
	pRect->m_fHeight = 100.f;
	pRect->m_fAngle = tfx::Deg2Rad(0.f);
	pRect->m_fThickness = 1.f;

	pRect->m_clThicknessColor = Col4(0.f, 0.f, 0.f, 1.f);
	pRect->m_clFillColor = Col4(0.2f, 0.5f, 0.f, 1.f);
	pRect->SetObjectID(4);

	//RectDrawObjectPtr pRect2 = m_rects->CreateRectDrawObject();

	//pRect2->m_ptX = 150.f;
	//pRect2->m_ptY = 100.f;
	//pRect2->m_fWidth = 150.f;
	//pRect2->m_fHeight = 100.f;
	//pRect2->m_fAngle = tfx::Deg2Rad(35.0);
	//pRect2->m_fThickness = 1.f;

	//pRect->m_clThicknessColor = Col4(0.f, 0.f, 0.f, 1.f);

	// ********************************************************************
	// Rectangles
	m_triangles = std::make_shared<TriangleDrawObjectList>();

	auto pTrig = m_triangles->CreateTriangleDrawObject();

	pTrig->m_pt2 = { 100, 100 };
	pTrig->m_pt1 = { 0, 100 };
	pTrig->m_pt3 = { 100, -60 };

	pTrig->m_fThickness = 1.f;
	pTrig->m_clColor = Col4(0.5f, 1.f, 0.f, 1.f);
	pTrig->m_clThicknessColor = Col4(0.f, 0.f, 0.f, 1.f);

	pTrig->Move({ -100.f, -100 });

	UpdateView();

	//vec e0 = p1 - p0, e1 = p2 - p1, e2 = p0 - p2;
	//vec2 v0 = p - p0, v1 = p - p1, v2 = p - p2;
	//vec2 pq0 = v0 - e0 * clamp(dot(v0, e0) / dot(e0, e0), 0.0, 1.0);
	//vec2 pq1 = v1 - e1 * clamp(dot(v1, e1) / dot(e1, e1), 0.0, 1.0);
	//vec2 pq2 = v2 - e2 * clamp(dot(v2, e2) / dot(e2, e2), 0.0, 1.0);
	//float s = sign(e0.x * e2.y - e0.y * e2.x);
	//vec2 d = min(min(vec2(dot(pq0, pq0), s * (v0.x * e0.y - v0.y * e0.x)),
	//	vec2(dot(pq1, pq1), s * (v1.x * e1.y - v1.y * e1.x))),
	//	vec2(dot(pq2, pq2), s * (v2.x * e2.y - v2.y * e2.x)));
	//return -sqrt(d.x) * sign(d.y);

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
	model.push_back(m_lines);
	model.push_back(m_circles);
	model.push_back(m_rects);
	model.push_back(m_triangles);

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

		auto pRect = m_rects->GetDrawObject(0);

		pRect->m_fAngle += 0.1f;

		m_rects->m_vecUpdateRects.push_back(pRect);

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


