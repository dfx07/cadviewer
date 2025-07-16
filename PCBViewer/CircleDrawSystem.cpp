﻿#include "CircleDrawSystem.h"

#include "graphics/rendering/OpenGL/glew.h"
#include "graphics/rendering/shader/xglshader.h"
#include "graphics/rendering/xopenglctx.h"


#include "GLRenderData.h"
#include "RenderContext.h"
#include "RenderComponent.h"


CircleDrawSystem::CircleDrawSystem()
{
}

CircleDrawSystem::~CircleDrawSystem()
{
}

void CircleDrawSystem::Draw(RenderDataPtr pRenderData, const DrawParams& params)
{
	if (!pRenderData)
		return;

	GLCircleRenderDataPtr pPolyData = std::dynamic_pointer_cast<GLCircleRenderData>(pRenderData);
	if (!pPolyData)
		return;

	auto pContext = params.context;
	auto pMaterial = params.material;
	auto pTransform = params.transform;

	if (!pRenderData || !pMaterial)
		return;

	auto pShader = pMaterial->GetShader();
	auto pBinder = pMaterial->GetBinder();

	if (!pShader->Use())
		return;

	pBinder->SetMat4("u_Proj", RENPTR(pContext->m_Proj));
	pBinder->SetMat4("u_View", RENPTR(pContext->m_View));
	pBinder->SetMat4("u_Model", RENPTR(Mat4(1.f)));
	pBinder->SetVec2("u_Viewport", RENPTR(pContext->m_ViewPort));
	pBinder->SetFloat("u_zZoom", pContext->m_Zoom);

	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glDisable(GL_MULTISAMPLE);
	glDepthMask(GL_FALSE);

	glBindVertexArray(pPolyData->m_nVao);
	glDrawElementsInstanced(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0, pPolyData->m_nInstances);

	glEnable(GL_MULTISAMPLE);
	glEnable(GL_DEPTH_TEST);

	pShader->UnUse();
}
