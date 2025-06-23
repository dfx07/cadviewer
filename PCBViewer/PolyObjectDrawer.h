#pragma once
#include "DrawObject.h"
#include "RenderModelManager.h"
#include "graphics/rendering/shape/xglpolyrender.h"

class PolyObjectDrawer : public tfx::GLPolyRender
{
public:
	PolyObjectDrawer(RenderModelManagerPtr pModelManager, int nID);
	virtual ~PolyObjectDrawer();


protected:
	void AddPolyData(PolyDrawObjectPtr polydata);
	void AddListPolyData(PolyDrawObjectListPtr polydataList);

public:
	void Clear();
	virtual void Remake() override;

protected:
	RenderModelManagerPtr m_pModelManager;
	int m_nNextPolygonID = 0; // ID for the next polygon to be added
};
