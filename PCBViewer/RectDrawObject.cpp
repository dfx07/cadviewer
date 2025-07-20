#include "RectDrawObject.h"
#include "GLRenderDataBuilder.h"

RectDrawObject::RectDrawObject()
	: DrawObject()
{

}

DrawObjectPtr RectDrawObject::Clone()
{
	auto pNewObject = std::make_shared<RectDrawObject>();
	pNewObject->Copy(this);
	return pNewObject;
}

void RectDrawObject::Copy(DrawObject* pSource)
{
	if (!pSource)
	{
		assert(0);
		return;
	}

	auto pSrcObj = dynamic_cast<RectDrawObject*>(pSource);

	if (!pSrcObj)
	{
		assert(0);
		return;
	}

	m_ptX				= pSrcObj->m_ptX;
	m_ptY				= pSrcObj->m_ptY;
	m_fWidth			= pSrcObj->m_fWidth;
	m_fHeight			= pSrcObj->m_fHeight;
	m_fThickness		= pSrcObj->m_fThickness;
	m_clThicknessColor	= pSrcObj->m_clThicknessColor;
	m_clFillColor		= pSrcObj->m_clFillColor;
}

void RectDrawObjectList::Add(const RectDrawObjectPtr& poly)
{
	if (poly)
	{
		m_vecRects.push_back(poly);
	}
	else
	{
		assert(0 && "RectDrawObject is null");
	}
}

void RectDrawObjectList::Remove(const RectDrawObjectPtr& poly)
{
	auto it = std::remove(m_vecRects.begin(), m_vecRects.end(), poly);
	if (it != m_vecRects.end())
	{
		m_vecRects.erase(it, m_vecRects.end());
	}
}

void RectDrawObjectList::Clear()
{
	m_vecRects.clear();
}

RectDrawObjectPtr RectDrawObjectList::CreateRectDrawObject()
{
	auto poly = std::make_shared<RectDrawObject>();
	m_vecRects.push_back(poly);
	return poly;
}

DrawObjectPtr RectDrawObjectList::Clone()
{
	auto pNewObject = std::make_shared<RectDrawObjectList>();
	pNewObject->Copy(this);
	return pNewObject;
}

void RectDrawObjectList::Copy(DrawObject* pSource)
{

}

RenderDataPtr RectDrawObjectList::Make(RenderDataBuilderPtr builder)
{
	if (!builder)
		return nullptr;

	return builder->Make(this);
}
