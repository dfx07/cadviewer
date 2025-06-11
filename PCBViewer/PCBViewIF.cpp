#include "PCBViewIF.h"
#include "PCBView.h"

#include "modules/inputdevice.h"
#include "WInputDeviceMap.h"

//\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

LIBRARY_API IntPtr CreatePCBView(IntPtr pHandle, _INT nWidth, _INT nHeight)
{
	PCBView* pPCBView = new PCBView();
	NULL_RETURN(pPCBView, NULL);

	if (!pPCBView->Create((HWND)pHandle, nWidth, nHeight))
	{
		delete pPCBView;
		return nullptr;
	}

	return pPCBView;
}

LIBRARY_API void DestroyPCBView(IntPtr pPCB)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	pView->Destroy();
	delete pView;
	return;
}

LIBRARY_API _INT CreateContext(IntPtr pHandle, ContextConfig _ctxConfig)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pHandle);
	NULL_RETURN(pView, FALSE);

	if (pView->CreateContext(_ctxConfig))
		return TRUE;

	return FALSE;
}

LIBRARY_API void DrawLine(IntPtr pPCB, _INT a)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	pView->OnNotifyUI("DrawLine", a, 3);
}

LIBRARY_API void Draw(IntPtr pPCB)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	pView->Draw();
}

LIBRARY_API void SetView(IntPtr pPCB, _INT nWidth, _INT nHeight)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	pView->SetView(nWidth, nHeight);
}

LIBRARY_API void Clear(IntPtr pPCB)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	//pView->Clear();
}

LIBRARY_API void OnMouseEnter(IntPtr pPCB)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	pView->OnMouseEnter(nullptr);

	return;
}

LIBRARY_API void OnMouseMove(IntPtr pPCB, _INT x, _INT y)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	TFXMouseEvent mouseEvent;

	mouseEvent.m_Pt = TFX_DevicePt(x, y);
	mouseEvent.m_Button = TFXMouseButton::UNKNOWN;
	mouseEvent.m_State = TFXMouseState::UNKNOW;

	pView->OnMouseMove(&mouseEvent);

	return;
}

LIBRARY_API void OnMouseDown(IntPtr pPCB, _INT x, _INT y, _INT button)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	TFXMouseEvent mouseEvent;

	mouseEvent.m_Pt = TFX_DevicePt(x, y);
	mouseEvent.m_Button = WInputDeviceMap::MapWinBtn2TFXBtn(button);
	mouseEvent.m_State = TFXMouseState::DOWN;

	pView->OnMouseDown(&mouseEvent);

	return;
}

LIBRARY_API void OnMouseUp(IntPtr pPCB, _INT x, _INT y, _INT button)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	TFXMouseEvent mouseEvent;

	mouseEvent.m_Pt = TFX_DevicePt(x, y);
	mouseEvent.m_Button = WInputDeviceMap::MapWinBtn2TFXBtn(button);
	mouseEvent.m_State = TFXMouseState::UP;

	pView->OnMouseUp(&mouseEvent);

	return;
}

LIBRARY_API void OnMouseDoubleClick(IntPtr pPCB, _INT x, _INT y, _INT button)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	TFXMouseEvent mouseEvent;

	mouseEvent.m_Pt = TFX_DevicePt(x, y);
	mouseEvent.m_Button = WInputDeviceMap::MapWinBtn2TFXBtn(button);
	mouseEvent.m_State = TFXMouseState::DBLCLICK;

	pView->OnMouseDoubleClick(&mouseEvent);

	return;
}

LIBRARY_API void OnMouseWheel(IntPtr pPCB, _INT x, _INT y, _FLOAT deltal)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);
	NULL_RETURN(pView);

	TFXMouseEvent mouseEvent;

	mouseEvent.m_Pt = TFX_DevicePt(x, y);
	mouseEvent.m_Button = TFXMouseButton::UNKNOWN;
	mouseEvent.m_Delta = deltal;

	pView->OnMouseWheel(&mouseEvent);
}


