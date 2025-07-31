#pragma once

#include "DrawObject.h"
#include "PCBViewType.h"
#include "RenderDef.h"

#include <vector>
#include <cassert>

class CircleDrawObject : public DrawObject
{
public:
	CircleDrawObject();
	virtual ~CircleDrawObject() = default;

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

public:
	void Move(Vec2 offset)
	{
		m_ptCenter += offset;
	}

public:
	Point2	m_ptCenter{ 0.f };
	float	m_fThickness{ 1.0f };
	float	m_fRadius{ 10.0f };
	Col4	m_clThicknessColor{ 0.f };
	Col4	m_clFillColor{ 0.f };
};

class CircleDrawObjectList : public DrawObject
{
public:
	CircleDrawObjectList() = default;
	virtual ~CircleDrawObjectList() = default;

public:
	void Add(const CircleDrawObjectPtr& poly);

	void Remove(const CircleDrawObjectPtr& poly);

	void Clear();

	CircleDrawObjectPtr CreateCircleDrawObject();

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

	RenderDataPtr Make(RenderDataBuilderPtr builder) override;
	bool Update(RenderDataPtr pData, RenderDataBuilderPtr builder) override;

public:
	std::vector<CircleDrawObjectPtr> m_vecCircles; // List of polygon draw objects
};