#pragma once

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