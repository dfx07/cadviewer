#pragma once

#include <apicom.h>
#include "PCBViewDef.h"

extern "C" LIBRARY_API void   SetCallbackFunctionNotifyUI(IntPtr _pBase, CallBackFunctionNotifyUI _pCall);
extern "C" LIBRARY_API IntPtr CreatePCBView(IntPtr pHandle);
extern "C" LIBRARY_API int    CreateContext(IntPtr pHandle, ContextConfig _ctxConfig);
extern "C" LIBRARY_API void   DestroyPCBView(IntPtr pPCB);
extern "C" LIBRARY_API void   DrawLine(IntPtr pPCB, int a);
extern "C" LIBRARY_API void   Draw(IntPtr pPCB);
extern "C" LIBRARY_API void   SetView(IntPtr pPCB, int nWidth, int nHeight);
extern "C" LIBRARY_API void   Clear(IntPtr pPCB);