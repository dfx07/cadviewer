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
	m_nDirtyFlags |= nFlags;
}

void DrawObject::ClearDirty()
{
	m_nDirtyFlags = 0;
}

bool DrawObject::IsDirty(int nFlags) const
{
	if (nFlags == 0)
		return (m_nDirtyFlags != 0);

	return (m_nDirtyFlags & nFlags) == nFlags;
}

RenderDataPtr DrawObject::Make(RenderDataBuilderPtr builder)
{
	if (!builder)
		return nullptr;

	return this->DoMake(builder);
}

bool DrawObject::Update(RenderDataPtr pData, RenderDataBuilderPtr builder)
{
	if (!pData || !builder)
		return false;

	return this->DoUpdate(pData, builder);
}
