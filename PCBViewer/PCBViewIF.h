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

#include <apicom.h>
#include "PCBViewDef.h"

C_STYLE
{
	LIBRARY_API IntPtr CreatePCBView(IntPtr pHandle);
	LIBRARY_API void   DestroyPCBView(IntPtr pPCB);

	LIBRARY_API Int    CreateContext(IntPtr pHandle, ContextConfig _ctxConfig);
	LIBRARY_API void   DrawLine(IntPtr pPCB, Int a);
	LIBRARY_API void   Draw(IntPtr pPCB);
	LIBRARY_API void   SetView(IntPtr pPCB, Int nWidth, Int nHeight);
	LIBRARY_API void   Clear(IntPtr pPCB);
}
