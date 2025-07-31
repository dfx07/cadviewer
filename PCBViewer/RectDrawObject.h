#pragma once

#include "DrawObject.h"
#include "PCBViewType.h"
#include "RenderDef.h"

#include <vector>
#include <cassert>

class RectDrawObject : public DrawObject
{
public:
	RectDrawObject();
	virtual ~RectDrawObject() = default;

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

public:
	void Move(Vec2 offset)
	{
		m_ptX += offset.x;
		m_ptY += offset.x;
	}

public:
	float	m_ptX{ 0.f }; // Left X coordinate
	float	m_ptY{ 0.f }; // Top Y coordinate
	float	m_fWidth{ 100.f }; // Width of the rectangle
	float	m_fHeight{ 100.f }; // Height of the rectangle
	float	m_fThickness{ 1.0f };
	Col4	m_clThicknessColor{ 0.f };
	Col4	m_clFillColor{ 0.f };

	float	m_fAngle{ 0.f }; // angle radian
};

class RectDrawObjectList : public DrawObject
{
public:
	RectDrawObjectList() = default;
	virtual ~RectDrawObjectList() = default;

public:
	void Add(const RectDrawObjectPtr& poly);

	void Remove(const RectDrawObjectPtr& poly);

	void Clear();

	RectDrawObjectPtr CreateRectDrawObject();

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

	RenderDataPtr Make(RenderDataBuilderPtr builder) override;
	bool Update(RenderDataPtr pData, RenderDataBuilderPtr builder) override;

public:
	std::vector<RectDrawObjectPtr> m_vecRects; // List of polygon draw objects
};