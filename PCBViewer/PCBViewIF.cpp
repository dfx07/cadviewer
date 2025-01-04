#include "PCBViewIF.h"
#include "PCBView.h"

//\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

LIBRARY_API IntPtr CreatePCBView(IntPtr pHandle)
{
	PCBView* pPCBView = new PCBView();
	NULL_RETURN(pPCBView, NULL);

	if (!pPCBView->Create((HWND)pHandle, 100, 100))
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

LIBRARY_API Int CreateContext(IntPtr pHandle, ContextConfig _ctxConfig)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pHandle);
	NULL_RETURN(pView, FALSE);

	if (pView->CreateContext(_ctxConfig))
		return TRUE;

	return FALSE;
}

LIBRARY_API void DrawLine(IntPtr pPCB, Int a)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);

	if (pView == NULL)
		return;

	pView->OnNotifyUI("DrawLine", a, 3);
}

LIBRARY_API void Draw(IntPtr pPCB)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);

	NULL_RETURN(pView);

	pView->Draw();
}

LIBRARY_API void SetView(IntPtr pPCB, Int nWidth, Int nHeight)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);

	NULL_RETURN(pView);

	pView->SetView(nWidth, nHeight);
}

LIBRARY_API void Clear(IntPtr pPCB)
{
	PCBView* pView = reinterpret_cast<PCBView*>(pPCB);

	NULL_RETURN(pView);

	pView->Clear();
}


