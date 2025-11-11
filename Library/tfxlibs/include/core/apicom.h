#include "macros.h"
#include "apidef.h"
#include "../Modules/NotifyObject.h"

C_STYLE LIBRARY_API_INLINE void SetCallbackFunctionNotifyUI(IntPtr _pBase, CallBackFunctionNotifyUI _pCall)
{
	NotifyObject* pNotifyObject = reinterpret_cast<NotifyObject*>(_pBase);

	NULL_RETURN(pNotifyObject);

	pNotifyObject->SetCallBackUI(_pCall);
	return;
}

