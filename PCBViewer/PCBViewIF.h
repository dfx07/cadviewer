////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2021-2025 thuong.nv <thuong.nv.mta@gmail.com>
* MIT software Licencs, see the accompanying
* http://www.opensource.org/licenses/mit-license.php
* For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************
* @file     PCBViewIF.h
* @create   Jan 4, 2025
* @brief    PCBview interface (P/Invoke)
***********************************************************************************/

#pragma once

#include <Common/apicom.h>
#include "PCBViewDef.h"

C_STYLE
{
	LIBRARY_API IntPtr CreatePCBView(IntPtr pHandle, _INT nWidth, _INT nHeight);
	LIBRARY_API void DestroyPCBView(IntPtr pPCB);

	LIBRARY_API _INT CreateContext(IntPtr pHandle, ContextConfig _ctxConfig);
	LIBRARY_API void Clear(IntPtr pPCB);

	// Handler
	LIBRARY_API void OnPaint(IntPtr pPCB);
	LIBRARY_API void OnSizeChanged(IntPtr pPCB, _INT nWidth, _INT nHeight);

	// Event handle
	LIBRARY_API void OnMouseEnter(IntPtr pPCB);
	LIBRARY_API void OnMouseMove(IntPtr pPCB, _INT x, _INT y);
	LIBRARY_API void OnMouseDown(IntPtr pPCB, _INT x, _INT y, _INT button);
	LIBRARY_API void OnMouseUp(IntPtr pPCB, _INT x, _INT y, _INT button);
	LIBRARY_API void OnMouseDoubleClick(IntPtr pPCB, _INT x, _INT y, _INT button);
	LIBRARY_API void OnMouseWheel(IntPtr pPCB, _INT x, _INT y, _FLOAT deltal);
	LIBRARY_API void OnMouseDragDrop(IntPtr pPCB, _INT x, _INT y, _INT button, _INT state);

	LIBRARY_API void OnKeyDown(IntPtr pPCB, _INT key, _INT flag);
	LIBRARY_API void OnKeyUp(IntPtr pPCB, _INT key, _INT flag);

	LIBRARY_API void OnSelect(IntPtr pPCB, _INT x, _INT y, _INT x1, _INT y1);

	LIBRARY_API void ProcessKeyState(IntPtr pPCB, _INT keyFlags);
}
