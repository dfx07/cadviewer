#include "LineDrawObject.h"
#include "GLRenderDataBuilder.h"

LineDrawObject::LineDrawObject()
	: DrawObject(),
	m_clColor(0.f),
	m_fThickness(0.f)
{

}

DrawObjectPtr LineDrawObject::Clone()
{
	auto pNewObject = std::make_shared<LineDrawObject>();
	pNewObject->Copy(this);
	return pNewObject;
}

void LineDrawObject::Copy(DrawObject* pSource)
{
	auto pSrcObj = dynamic_cast<LineDrawObject*>(pSource);

	if (!pSrcObj)
	{
		assert(0);
		return;
	}

	m_ptS = pSrcObj->m_ptS;
	m_ptE = pSrcObj->m_ptE;

	m_clColor = pSrcObj->m_clColor;
	m_fThickness = pSrcObj->m_fThickness;
}

RenderDataPtr LineDrawObject::DoMake(RenderDataBuilderPtr builder)
{
	return RenderDataPtr();
}

bool LineDrawObject::DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder)
{
	return false;
}

void LineDrawObjectList::Add(const LineDrawObjectPtr pLine)
{
	if (pLine)
	{
		m_vecLines.push_back(pLine);
	}
	else
	{
		assert(0 && "LineDrawObject is null");
	}
}

void LineDrawObjectList::Remove(const LineDrawObjectPtr pLine)
{
	auto it = std::remove(m_vecLines.begin(), m_vecLines.end(), pLine);
	if (it != m_vecLines.end())
	{
		m_vecLines.erase(it, m_vecLines.end());
	}
}

void LineDrawObjectList::Clear()
{
	m_vecLines.clear();
}

LineDrawObjectPtr LineDrawObjectList::CreateLineDrawObject()
{
	auto poly = std::make_shared<LineDrawObject>();
	m_vecLines.push_back(poly);
	return poly;
}

DrawObjectPtr LineDrawObjectList::Clone()
{
	auto pNewObject = std::make_shared<LineDrawObjectList>();
	pNewObject->Copy(this);
	return pNewObject;
}

void LineDrawObjectList::Copy(DrawObject* pSource)
{

}

RenderDataPtr LineDrawObjectList::DoMake(RenderDataBuilderPtr builder)
{
	return builder->Make(this);
}

bool LineDrawObjectList::DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder)
{
	return builder->Update(pData, this);
}