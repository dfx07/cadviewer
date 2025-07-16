#include "CircleDrawObject.h"
#include "GLRenderDataBuilder.h"

CircleDrawObject::CircleDrawObject()
	: DrawObject()
{

}

DrawObjectPtr CircleDrawObject::Clone()
{
	auto pNewObject = std::make_shared<CircleDrawObject>();
	pNewObject->Copy(this);
	return pNewObject;
}

void CircleDrawObject::Copy(DrawObject* pSource)
{
	if (!pSource)
	{
		assert(0);
		return;
	}

	auto pSrcObj = dynamic_cast<CircleDrawObject*>(pSource);

	if (!pSrcObj)
	{
		assert(0);
		return;
	}

	m_ptCenter = pSrcObj->m_ptCenter;
	m_fRadius = pSrcObj->m_fRadius;
	m_fThickness = pSrcObj->m_fThickness;
	m_clFillColor = pSrcObj->m_clFillColor;
	m_clThicknessColor = pSrcObj->m_clThicknessColor;
}

void CircleDrawObjectList::Add(const CircleDrawObjectPtr& poly)
{
	if (poly)
	{
		m_vecCircles.push_back(poly);
	}
	else
	{
		assert(0 && "CircleDrawObject is null");
	}
}

void CircleDrawObjectList::Remove(const CircleDrawObjectPtr& poly)
{
	auto it = std::remove(m_vecCircles.begin(), m_vecCircles.end(), poly);
	if (it != m_vecCircles.end())
	{
		m_vecCircles.erase(it, m_vecCircles.end());
	}
}

void CircleDrawObjectList::Clear()
{
	m_vecCircles.clear();
}

CircleDrawObjectPtr CircleDrawObjectList::CreateCircleDrawObject()
{
	auto poly = std::make_shared<CircleDrawObject>();
	m_vecCircles.push_back(poly);
	return poly;
}

DrawObjectPtr CircleDrawObjectList::Clone()
{
	auto pNewObject = std::make_shared<CircleDrawObjectList>();
	pNewObject->Copy(this);
	return pNewObject;
}

void CircleDrawObjectList::Copy(DrawObject* pSource)
{

}

RenderDataPtr CircleDrawObjectList::Make(RenderDataBuilderPtr builder)
{
	if (!builder)
		return nullptr;

	return builder->Make(this);
}
