#include "PolyDrawObject.h"
#include "GLRenderDataBuilder.h"

PolyDrawObject::PolyDrawObject()
	: DrawObject()
{
}

DrawObjectPtr PolyDrawObject::Clone()
{
	auto pNewObject = std::make_shared<PolyDrawObject>();
	pNewObject->Copy(this);
	return pNewObject;
}

void PolyDrawObject::Copy(DrawObject* pSource)
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

void PolyDrawObjectList::AddPolyDrawObject(const PolyDrawObjectPtr& poly)
{
	if (poly)
	{
		m_vecPolys.push_back(poly);
	}
	else
	{
		assert(0 && "PolyDrawObject is null");
	}
}

void PolyDrawObjectList::RemovePolyDrawObject(const PolyDrawObjectPtr& poly)
{
	auto it = std::remove(m_vecPolys.begin(), m_vecPolys.end(), poly);
	if (it != m_vecPolys.end())
	{
		m_vecPolys.erase(it, m_vecPolys.end());
	}
}

void PolyDrawObjectList::Clear()
{
	m_vecPolys.clear();
}

PolyDrawObjectPtr PolyDrawObjectList::CreatePolyDrawObject()
{
	auto poly = std::make_shared<PolyDrawObject>();
	m_vecPolys.push_back(poly);
	return poly;
}

DrawObjectPtr PolyDrawObjectList::Clone()
{
	auto pNewObject = std::make_shared<PolyDrawObjectList>();
	pNewObject->Copy(this);
	return pNewObject;
}

void PolyDrawObjectList::Copy(DrawObject* pSource)
{
}

RenderDataPtr PolyDrawObjectList::Make(RenderDataBuilderPtr builder)
{
	if (!builder)
		return nullptr;

	return builder->Make(this);
}

bool PolyDrawObjectList::Update(RenderDataPtr pData, RenderDataBuilderPtr builder)
{
	if (!builder || pData)
		return false;

	return builder->Update(pData, this);
}