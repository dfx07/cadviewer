#include "TextDrawSystem.h"

#include "graphics/rendering/OpenGL/glew.h"
#include "graphics/rendering/shader/xglshader.h"
#include "graphics/rendering/xopenglctx.h"


#include "GLRenderData.h"
#include "RenderContext.h"
#include "RenderComponent.h"


SDFTextDrawSystem::SDFTextDrawSystem()
{
}

SDFTextDrawSystem::~SDFTextDrawSystem()
{
}

void SDFTextDrawSystem::Draw(RenderDataPtr data, const DrawParams& params)
{
	auto pRectData = std::dynamic_pointer_cast<GLSDFTextRenderDataPtr::element_type>(data);
	if (!pRectData)
		return;

	// TODO :
}

BitmapTextDrawSystem::BitmapTextDrawSystem()
{
}

BitmapTextDrawSystem::~BitmapTextDrawSystem()
{
}

void BitmapTextDrawSystem::Draw(RenderDataPtr data, const DrawParams& params)
{
	auto pRectData = std::dynamic_pointer_cast<GLBitmapTextRenderDataPtr::element_type>(data);
	if (!pRectData)
		return;

	// TODO :
}

TextDrawSystem::TextDrawSystem()
{
	m_pTextDrawSystemRegistry = std::make_shared<DrawSystemRegistry>();

	m_pTextDrawSystemRegistry->RegisterSystem<GLSDFTextRenderData>(std::make_shared<SDFTextDrawSystem>());
	m_pTextDrawSystemRegistry->RegisterSystem<GLBitmapTextRenderData>(std::make_shared<BitmapTextDrawSystem>());
}

TextDrawSystem::~TextDrawSystem()
{
}

void TextDrawSystem::Draw(RenderDataPtr pRenderData, const DrawParams& params)
{
	auto pRectData = std::dynamic_pointer_cast<GLTextRenderDataPtr::element_type>(pRenderData);
	if (!pRectData)
		return;

	m_pTextDrawSystemRegistry->Draw(pRectData->m_pSDFRenderData, params);
	m_pTextDrawSystemRegistry->Draw(pRectData->m_pBitmapRenderData, params);
}
