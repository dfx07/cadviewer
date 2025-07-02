#pragma once
#include "PCBViewType.h"
#include "RenderModelManager.h"
#include "graphics/rendering/xrenderable.h"

class PolyObjectDrawer : public tfx::ObjectRenderable
{
public:
	PolyObjectDrawer(RenderModelManagerPtr pModelManager, int nID);
	virtual ~PolyObjectDrawer();

protected:
	void AddPolyData(PolyDrawObjectPtr polydata);
	void AddListPolyData(PolyDrawObjectListPtr polydataList);

public:
	virtual void Draw(const Mat4& view, const Mat4& proj, const Vec2& viewport, const float& zoom = 1.f);
	virtual void Remake() override;

public:
	void Clear();

protected:
	RenderModelManagerPtr m_pModelManager;
	int m_nNextPolygonID = 0; // ID for the next polygon to be added
};
