#include "PolyDrawObject.h"
#include "GLRenderDataBuilder.h"

PolyDrawObject::PolyDrawObject()
	: DrawObject()
{
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

RenderDataPtr PolyDrawObjectList::Make(RenderDataBuilderPtr builder)
{
	if (!builder)
		return nullptr;

	return builder->Make(this);
}
