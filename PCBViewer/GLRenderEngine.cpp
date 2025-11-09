#include "GLRenderEngine.h"
#include "graphics/rendering/OpenGL/glew.h"

#include "GLRenderData.h"
#include "RenderComponent.h"

#include "graphics/rendering/shader/xglshader.h"
#include "common/tfx_type.h"

#include "DrawSystem.h"
#include "PolyDrawSystem.h"
#include "LineDrawSystem.h"
#include "CircleDrawSystem.h"
#include "RectDrawSystem.h"
#include "TriangleDrawSystem.h"
#include "TextDrawSystem.h"


GLRenderEngine::GLRenderEngine()
	: RenderEngine()
{

	m_pDrawSystemRegistry = std::make_shared<DrawSystemRegistry>();

	// TODO : add if you add a new draw object
	m_pDrawSystemRegistry->RegisterSystem<GLPolyRenderData>(std::make_shared<PolyDrawSystem>());
	m_pDrawSystemRegistry->RegisterSystem<GLLineRenderData>(std::make_shared<LineDrawSystem>());
	m_pDrawSystemRegistry->RegisterSystem<GLCircleRenderData>(std::make_shared<CircleDrawSystem>());
	m_pDrawSystemRegistry->RegisterSystem<GLRectRenderData>(std::make_shared<RectDrawSystem>());
	m_pDrawSystemRegistry->RegisterSystem<GLTriangleRenderData>(std::make_shared<TriangleDrawSystem>());
	m_pDrawSystemRegistry->RegisterSystem<GLTextRenderData>(std::make_shared<TextDrawSystem>());
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
