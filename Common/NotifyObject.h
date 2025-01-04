#pragma once
#include "dllexports.h"
#include "apidef.h"

typedef void (*CallBackFunctionNotifyUI)(const char* msg, int lParam, int wParam);

class NotifyObject
{
public:
	virtual void OnNotifyUI(const char* msg, int lParam, int wParam)
	{
		if (!m_pCallbackUI)
			return;

		m_pCallbackUI(msg, lParam, wParam);
	}

public:
	virtual void SetCallBackUI(CallBackFunctionNotifyUI _func)
	{
		m_pCallbackUI = _func;
	}

protected:
	CallBackFunctionNotifyUI m_pCallbackUI = nullptr;
};

C_STYLE LIBRARY_API_INLINE void SetCallbackFunctionNotifyUI(IntPtr _pBase, CallBackFunctionNotifyUI _pCall)
{
	NotifyObject* pNotifyObject = reinterpret_cast<NotifyObject*>(_pBase);

	NULL_RETURN(pNotifyObject);

	pNotifyObject->SetCallBackUI(_pCall);
	return;
}