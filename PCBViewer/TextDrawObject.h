#pragma once

#include "DrawObject.h"
#include "PCBViewType.h"
#include "RenderDef.h"

#include "ResourceDef.h"

#include <vector>

class TextDrawObject : public DrawObject
{
public:
	TextDrawObject();
	virtual ~TextDrawObject() = default;

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

	virtual RenderDataPtr DoMake(RenderDataBuilderPtr builder) override;
	virtual bool DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder) override;

public:
	void Move(const Vec2& offset); // move

public:
	Point2 m_pt;
	Col4 m_clColor{ 0.f };	// Colors
	float m_fAngle{ 0 };
	std::string m_data;

	ETextRenderType m_eRenderType{ ETextRenderType::SDF };
	std::string m_strFontKey{};
};

class TextDrawObjectList : public DrawObject
{
public:
	TextDrawObjectList() = default;
	virtual ~TextDrawObjectList() = default;

public:
	void AddTextDrawObject(const TextDrawObjectPtr& poly);

	void RemoveTextDrawObject(const TextDrawObjectPtr& poly);

	void Clear();

	TextDrawObjectPtr CreateTextDrawObject();

public:
	virtual DrawObjectPtr Clone() override;
	virtual void Copy(DrawObject* pSource) override;

	virtual RenderDataPtr DoMake(RenderDataBuilderPtr builder) override;
	virtual bool DoUpdate(RenderDataPtr pData, RenderDataBuilderPtr builder) override;

public:
	std::vector<TextDrawObjectPtr> m_vecTexts; // List of text draw objects
};
