#pragma once

#include "DrawObject.h"
#include "PCBViewType.h"

#include <vector>

class PolyDrawObject : public DrawObject
{
public:
	PolyDrawObject();
	virtual ~PolyDrawObject() = default;

public:
	virtual void Remake()
	{
		// Implement the logic to remake the poly draw object
	}

public:
	virtual DrawObjectPtr Clone() override
	{
		auto pNewObject = std::make_shared<PolyDrawObject>();
		pNewObject->Copy(this);

		return pNewObject;
	}

	virtual void Copy(DrawObject* pSource) override
	{
		if (!pSource)
		{
			assert(0);
			return;
		}

		auto pSrcObj = dynamic_cast<PolyDrawObject*>(pSource);

		if (!pSrcObj)
		{
			assert(0);
			return;
		}

		m_vecPoints.clear();

		if (!pSrcObj->m_vecPoints.empty())
		{
			m_vecPoints.reserve(pSrcObj->m_vecPoints.size());
			m_vecPoints.insert(m_vecPoints.end(),
							pSrcObj->m_vecPoints.begin(),
							pSrcObj->m_vecPoints.end());
		}

		m_vecPoints = pSrcObj->m_vecPoints;
		m_clColor = pSrcObj->m_clColor;
		m_fThickness = pSrcObj->m_fThickness;
	}

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
	Col4 m_clColor;					// Colors for each point
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
	virtual DrawObjectPtr Clone() override
	{

		return nullptr;
	}

	virtual void Copy(DrawObject* pSource) override
	{

	}

	RenderDataPtr Make(RenderDataBuilderPtr builder) const override;

public:
	std::vector<PolyDrawObjectPtr> m_vecPolys; // List of polygon draw objects
};