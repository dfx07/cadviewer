#pragma once
#include "DrawObject.h"
#include "RenderModelManager.h"
#include "graphics/rendering/shape/xglpolyrender.h"

class PolyObjectDrawer : public tfx::GLPolyRender
{
public:
	PolyObjectDrawer();
	virtual ~PolyObjectDrawer();


public:
	void AddPolyData(PolyDrawObjectPtr polydata);

public:
	virtual void Remake() override;

protected:
	RenderModelManagerPtr m_pModelManager;
};
