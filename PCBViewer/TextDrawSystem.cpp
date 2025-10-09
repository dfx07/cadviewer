#include "TextDrawSystem.h"

#include "graphics/rendering/OpenGL/glew.h"
#include "graphics/rendering/shader/xglshader.h"
#include "graphics/rendering/xopenglctx.h"


#include "GLRenderData.h"
#include "RenderContext.h"
#include "RenderComponent.h"


TextDrawSystem::TextDrawSystem()
{
}

TextDrawSystem::~TextDrawSystem()
{
}

void TextDrawSystem::Draw(RenderDataPtr pRenderData, const DrawParams& params)
{
	auto pRectData = std::dynamic_pointer_cast<GLTextRenderDataPtr::element_type>(pRenderData);
	if (!pRectData)
		return;


}
