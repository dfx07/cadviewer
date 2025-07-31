#include "DrawObject.h"
#include "GLRenderDataBuilder.h"


DrawObject::DrawObject()
{
}

DrawObject::~DrawObject()
{
}

void DrawObject::MarkDirty(int nFlags)
{
	m_nDirtyFlags &= nFlags;
}

void DrawObject::ClearDirty()
{
	m_nDirtyFlags = 0;
}

bool DrawObject::IsDirty(int nFlags) const
{
	return (m_nDirtyFlags & nFlags) == nFlags;
}

RenderDataPtr DrawObject::Make(RenderDataBuilderPtr builder)
{
	return nullptr;
}

bool DrawObject::Update(RenderDataPtr pData, RenderDataBuilderPtr builder)
{
	return false;
}
