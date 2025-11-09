#pragma once

#include "PCBViewDef.h"
#include "PCBViewType.h"

#include <modules/NotifyObject.h>
#include "graphics/rendering/xctx.h"
#include "graphics/rendering/xrendertype.h"
#include "RenderDef.h"

#include "ViewBehavior.h"

#include <Windows.h>

enum EPCBViewState
{
	none = 0,
	move = 1 << 0,
	zoom = 1 << 1,
};

class PCBView : public NotifyObject,
	public ITFXMouseInteractiveView,
	public ITFXKeyboardInteractiveView
{
public:
	PCBView();
	virtual ~PCBView();

public:
	virtual bool Create(HWND hWnd, const int nWidth, const int nHeight);
	virtual void Destroy();
	virtual bool CreateContext(ContextConfig ctx_conf);

protected:
	virtual void DeleteContext();

public:
	virtual void Draw();

public:
	void UpdateView();

public:
	virtual void OnPaint();
	virtual void OnSizeChanged(const int nWidth, const int nHeight);

	virtual void OnMouseEnter(TFXEvent* event);
	virtual void OnMouseMove(TFXMouseEvent* event);
	virtual void OnMouseDown(TFXMouseEvent* event);
	virtual void OnMouseUp(TFXMouseEvent* event);
	virtual void OnMouseDoubleClick(TFXMouseEvent* event);
	virtual void OnMouseWheel(TFXMouseEvent* event);
	virtual void OnMouseDragDrop(TFXMouseEvent* event);

	virtual void OnKeyDown(TFXKeyEvent* event);
	virtual void OnKeyUp(TFXKeyEvent* event);

public:
	bool HandleMoveView(TFX_DevicePt ptNew);

	void OnReceiveCtrlKeyState(const int keyFlags);
	bool IsCtrlKeyPressed(int key) const;

public:
	DeviceContextPtr GetContext() const;

protected:
	DeviceContextPtr m_pContext = nullptr;
	RendererPtr		 m_pRenderer = nullptr;
	CameraPtr		 m_pCamera = nullptr;
	
	HWND			m_hHandle;
	int				m_nWidth;
	int				m_nHeight;
	int				m_nCtrlKeyFlags = 0;

	EPCBViewState	m_ePCBViewState = EPCBViewState::none;
	TFX_DevicePt	m_ptMouseLastClick = { 0,0 };
	TFX_DevicePt	m_ptLastMouse = { 0,0 };

	PolyDrawObjectListPtr m_polys = nullptr;
	LineDrawObjectListPtr m_lines = nullptr;
	CircleDrawObjectListPtr m_circles = nullptr;
	RectDrawObjectListPtr m_rects = nullptr;
	TriangleDrawObjectListPtr m_triangles = nullptr;


	//RenderModelManagerPtr m_pModelManager = nullptr;
};

