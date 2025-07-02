#pragma once

#include <memory>

class DrawObject;
typedef std::shared_ptr<DrawObject> DrawObjectPtr;

class RenderData;
typedef std::shared_ptr<RenderData> RenderDataPtr;

class IRenderDataBuilder;
typedef std::shared_ptr<IRenderDataBuilder> RenderDataBuilderPtr;

class DrawObject
{
public:
	DrawObject();
	virtual ~DrawObject();

public:
	virtual DrawObjectPtr Clone() = 0;
	virtual void Copy(DrawObject* pSource) = 0;

public:
	int GetObjectID() const
	{
		return m_nObjectID;
	}

	void SetObjectID(const int nID)
	{
		m_nObjectID = nID;
	}

public:
	virtual RenderDataPtr Make(RenderDataBuilderPtr builder) const;

protected:
	int m_nObjectID{ -1 };

};

