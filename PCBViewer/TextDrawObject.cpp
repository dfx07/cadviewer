#include "TextDrawObject.h"
#include "GLRenderDataBuilder.h"

TextDrawObject::TextDrawObject()
	: DrawObject()
{
}

DrawObjectPtr TextDrawObject::Clone()
{
	auto pNewObject = std::make_shared<TextDrawObject>();
	pNewObject->Copy(this);
	return pNewObject;
}

void TextDrawObject::Copy(DrawObject* pSource)
{
	auto pSrcObj = dynamic_cast<TextDrawObject*>(pSource);

	if (!pSrcObj)
	{
		assert(0);
		return;
	}
	m_data = pSrcObj->m_data;
	m_clColor = pSrcObj->m_clColor;
	m_fAngle = pSrcObj->m_fAngle;
	m_pt = pSrcObj->m_pt;
}

void TextDrawObject::Move(const Vec2& offset)
{
	m_pt += offset;
}

RenderDataPtr TextDrawObject::DoMake(RenderDataBuilderPtr builder)
{
	return RenderDataPtr();
}

bool TextDrawObject::DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder)
{
	return false;
}

void TextDrawObjectList::AddTextDrawObject(const TextDrawObjectPtr& poly)
{
	if (poly)
	{
		m_vecTexts.push_back(poly);
	}
	else
	{
		assert(0 && "TextDrawObject is null");
	}
}

void TextDrawObjectList::RemoveTextDrawObject(const TextDrawObjectPtr& poly)
{
	auto it = std::remove(m_vecTexts.begin(), m_vecTexts.end(), poly);
	if (it != m_vecTexts.end())
	{
		m_vecTexts.erase(it, m_vecTexts.end());
	}
}

void TextDrawObjectList::Clear()
{
	m_vecTexts.clear();
}

TextDrawObjectPtr TextDrawObjectList::CreateTextDrawObject()
{
	auto poly = std::make_shared<TextDrawObject>();
	m_vecTexts.push_back(poly);
	return poly;
}

DrawObjectPtr TextDrawObjectList::Clone()
{
	auto pNewObject = std::make_shared<TextDrawObjectList>();
	pNewObject->Copy(this);
	return pNewObject;
}

void TextDrawObjectList::Copy(DrawObject* pSource)
{

}

RenderDataPtr TextDrawObjectList::DoMake(RenderDataBuilderPtr builder)
{
	return builder->Make(this);
}

bool TextDrawObjectList::DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder)
{
	return builder->Update(pData, this);
}