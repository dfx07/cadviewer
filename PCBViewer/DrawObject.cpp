#include "DrawObject.h"
#include "GLRenderDataBuilder.h"


DrawObject::DrawObject()
{
}

DrawObject::~DrawObject()
{
}

RenderDataPtr DrawObject::Make(RenderDataBuilderPtr builder) const
{
	return nullptr;
}
