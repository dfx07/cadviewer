#pragma once

#include "DrawObject.h"
#include "PCBViewType.h"
#include "RenderDef.h"

#include <vector>
#include <cassert>

class TriangleDrawObject : public DrawObject
{
public:
	TriangleDrawObject();
	virtual ~TriangleDrawObject() = default;

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

	virtual RenderDataPtr DoMake(RenderDataBuilderPtr builder) override;
	virtual bool DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder) override;

public:
	void Move(Vec2 offset)
	{
		m_pt1 += offset;
		m_pt2 += offset;
		m_pt3 += offset;
	}

public:
	Point2	m_pt1{ 0.f };
	Point2	m_pt2{ 0.f };
	Point2	m_pt3{ 0.f };
	Col4	m_clColor{ 0.f };
	float	m_fThickness{ 1.0f };
	Col4	m_clThicknessColor{ 1.0f };
};

class TriangleDrawObjectList : public DrawObject
{
public:
	TriangleDrawObjectList() = default;
	virtual ~TriangleDrawObjectList() = default;

public:
	void Add(const TriangleDrawObjectPtr pTri);

	void Remove(const TriangleDrawObjectPtr pTri);

	void Clear();

	TriangleDrawObjectPtr CreateTriangleDrawObject();

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

	virtual RenderDataPtr DoMake(RenderDataBuilderPtr builder) override;
	virtual bool DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder) override;

public:
	std::vector<TriangleDrawObjectPtr> m_vecTrigs; // List of polygon draw objects
};