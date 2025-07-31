#pragma once

#include "DrawObject.h"
#include "PCBViewType.h"
#include "RenderDef.h"

#include <vector>
#include <cassert>

class PolyDrawObject : public DrawObject
{
public:
	PolyDrawObject();
	virtual ~PolyDrawObject() = default;

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

public:
	void Move(Vec2 offset)
	{
		for (auto& pt : m_vecPoints)
		{
			pt += offset;
		}
	}

public:
	std::vector<Vec2> m_vecPoints;	// Points defining the polygon
	Col4 m_clColor{ 0.f };			// Colors for each point
	float m_fThickness{ 1.0f };		// Thickness of the polygon lines
};

class PolyDrawObjectList : public DrawObject
{
public:
	PolyDrawObjectList() = default;
	virtual ~PolyDrawObjectList() = default;

public:
	void AddPolyDrawObject(const PolyDrawObjectPtr& poly);

	void RemovePolyDrawObject(const PolyDrawObjectPtr& poly);

	void Clear();

	PolyDrawObjectPtr CreatePolyDrawObject();

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

	RenderDataPtr Make(RenderDataBuilderPtr builder) override;
	bool Update(RenderDataPtr pData, RenderDataBuilderPtr builder) override;

public:
	std::vector<PolyDrawObjectPtr> m_vecPolys; // List of polygon draw objects
};
