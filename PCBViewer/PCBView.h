#pragma once

#include "PCBViewDef.h"
#include <common/dllexports.h>
#include <modules/NotifyObject.h>
#include "graphics/rendering/xctx.h"
#include "graphics/rendering/xrendertype.h"


#include <Windows.h>

class PCBView : public NotifyObject
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
	virtual void SetView(const int nWidth, const int nHeight);
	virtual void Draw();

public:
	DeviceContextPtr GetContext() const;

protected:
	DeviceContextPtr		m_pContext = nullptr;
	tfx::TFXRendererPtr		m_pRenderer = nullptr;
	tfx::CameraPtr			m_pCamera = nullptr;
	
	HWND			m_hHandle;
	int				m_nWidth;
	int				m_nHeight;
};

