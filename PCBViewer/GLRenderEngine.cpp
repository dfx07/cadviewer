#include "GLRenderEngine.h"
#include "graphics/rendering/OpenGL/glew.h"

#include "GLRenderData.h"
#include "RenderComponent.h"

#include "graphics/rendering/shader/xglshader.h"
#include "common/tfxtype.h"

#include "DrawSystem.h"
#include "PolyDrawSystem.h"


GLRenderEngine::GLRenderEngine()
	: RenderEngine()
{

	m_pDrawSystemRegistry = std::make_shared<DrawSystemRegistry>();

	// TODO : add if you add a new draw object
	m_pDrawSystemRegistry->RegisterSystem<GLPolyRenderData>(std::make_shared<PolyDrawSystem>());
}

GLRenderEngine::~GLRenderEngine()
{
}

void GLRenderEngine::DrawRenderData(RenderDataPtr pRenderData, RenderContextPtr pContext,
	MaterialComponentPtr pMaterial, TransformComponentPtr pTranform)
{
	if (!pRenderData || !pMaterial)
		return;

	if (!m_pDrawSystemRegistry)
		return;

	DrawParams params;

	params.context = pContext;
	params.material = pMaterial;
	params.transform = pTranform;

	m_pDrawSystemRegistry->Draw(pRenderData, params);
}
