#include "TriangleDrawObject.h"
#include "GLRenderDataBuilder.h"

TriangleDrawObject::TriangleDrawObject()
	: DrawObject(),
	m_clColor(0.f),
	m_clThicknessColor(0.f),
	m_fThickness(0.f)
{

}

DrawObjectPtr TriangleDrawObject::Clone()
{
	auto pNewObject = std::make_shared<TriangleDrawObject>();
	pNewObject->Copy(this);
	return pNewObject;
}

void TriangleDrawObject::Copy(DrawObject* pSource)
{
	auto pSrcObj = dynamic_cast<TriangleDrawObject*>(pSource);

	if (!pSrcObj)
	{
		assert(0);
		return;
	}

	m_pt1 = pSrcObj->m_pt1;
	m_pt2 = pSrcObj->m_pt2;
	m_pt3 = pSrcObj->m_pt3;

	m_clColor = pSrcObj->m_clColor;
	m_fThickness = pSrcObj->m_fThickness;
	m_clThicknessColor = pSrcObj->m_clThicknessColor;
}

RenderDataPtr TriangleDrawObject::DoMake(RenderDataBuilderPtr builder)
{
	return RenderDataPtr();
}

bool TriangleDrawObject::DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder)
{
	return false;
}

void TriangleDrawObjectList::Add(const TriangleDrawObjectPtr pTri)
{
	if (pTri)
	{
		m_vecTrigs.push_back(pTri);
	}
	else
	{
		assert(0 && "TriangleDrawObject is null");
	}
}

void TriangleDrawObjectList::Remove(const TriangleDrawObjectPtr pTri)
{
	auto it = std::remove(m_vecTrigs.begin(), m_vecTrigs.end(), pTri);
	if (it != m_vecTrigs.end())
	{
		m_vecTrigs.erase(it, m_vecTrigs.end());
	}
}

void TriangleDrawObjectList::Clear()
{
	m_vecTrigs.clear();
}

TriangleDrawObjectPtr TriangleDrawObjectList::CreateTriangleDrawObject()
{
	auto pTri = std::make_shared<TriangleDrawObject>();
	m_vecTrigs.push_back(pTri);
	return pTri;
}

DrawObjectPtr TriangleDrawObjectList::Clone()
{
	auto pNewObject = std::make_shared<TriangleDrawObjectList>();
	pNewObject->Copy(this);
	return pNewObject;
}

void TriangleDrawObjectList::Copy(DrawObject* pSource)
{

}

RenderDataPtr TriangleDrawObjectList::DoMake(RenderDataBuilderPtr builder)
{
	return builder->Make(this);
}

bool TriangleDrawObjectList::DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder)
{
	return builder->Update(pData, this);
}