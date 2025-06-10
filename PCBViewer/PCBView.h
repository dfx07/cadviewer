#pragma once

#include <common/dllexports.h>
#include <graphics/rendering/xopenglctx.h>
#include <modules/NotifyObject.h>
#include <graphics/rendering/xopenglrenderer.h>
#include "PCBViewDef.h"

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
	virtual bool IsAlreadyContext() const;
	virtual void MakeContext() const;

public:
	virtual void SetView(const int nWidth, const int nHeight);
	virtual void Clear();
	virtual void Draw();

public:
	DeviceContext* GetContext() const;

protected:
	DeviceContext*	m_pContext = nullptr;
	tfx::OpenGLRenderer* m_Renderer = nullptr;
	
	HWND			m_hHandle;
	int				m_nWidth;
	int				m_nHeight;
};

