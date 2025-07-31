#pragma once

#include "DrawObject.h"
#include "PCBViewType.h"
#include "RenderDef.h"

#include <vector>
#include <cassert>

class LineDrawObject : public DrawObject
{
public:
	LineDrawObject();
	virtual ~LineDrawObject() = default;

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

public:
	void Move(Vec2 offset)
	{
		m_ptS += offset;
		m_ptE += offset;
	}

public:
	Point2	m_ptS{ 0.f };
	Point2	m_ptE{ 0.f };
	Col4	m_clColor{ 0.f };
	float	m_fThickness{ 1.0f };
};

class LineDrawObjectList : public DrawObject
{
public:
	LineDrawObjectList() = default;
	virtual ~LineDrawObjectList() = default;

public:
	void Add(const LineDrawObjectPtr& poly);

	void Remove(const LineDrawObjectPtr& poly);

	void Clear();

	LineDrawObjectPtr CreateLineDrawObject();

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

	RenderDataPtr Make(RenderDataBuilderPtr builder) override;
	bool Update(RenderDataPtr pData, RenderDataBuilderPtr builder) override;

public:
	std::vector<LineDrawObjectPtr> m_vecLines; // List of polygon draw objects
};