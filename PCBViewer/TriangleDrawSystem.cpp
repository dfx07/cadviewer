#include "TriangleDrawSystem.h"

#include "graphics/rendering/OpenGL/glew.h"
#include "graphics/rendering/shader/xglshader.h"
#include "graphics/rendering/xopenglctx.h"


#include "GLRenderData.h"
#include "RenderContext.h"
#include "RenderComponent.h"


TriangleDrawSystem::TriangleDrawSystem()
{
}

TriangleDrawSystem::~TriangleDrawSystem()
{
}

void TriangleDrawSystem::Draw(RenderDataPtr pRenderData, const DrawParams& params)
{
	if (!pRenderData)
		return;

	GLTriangleRenderDataPtr pTriData = std::dynamic_pointer_cast<GLTriangleRenderDataPtr::element_type>(pRenderData);
	if (!pTriData)
		return;

	auto pContext = params.context;
	auto pMaterial = params.material;
	auto pTransform = params.transform;

	if (!pRenderData || !pMaterial)
		return;

	// Fill
	{
		auto pShader = pMaterial->GetShader("trig_f");
		auto pBinder = pMaterial->GetBinder("trig_f");

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

		glBindVertexArray(pTriData->m_nVao);
		glDrawArraysInstanced(GL_TRIANGLES, 0, 3, pTriData->m_nInstances);

		glEnable(GL_MULTISAMPLE);
		glEnable(GL_DEPTH_TEST);

		pShader->UnUse();
	}

	// Border
	{
		auto pShader = pMaterial->GetShader("trig_b");
		auto pBinder = pMaterial->GetBinder("trig_b");

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

		glBindVertexArray(pTriData->m_nVao);
		glDrawArraysInstanced(GL_TRIANGLES, 0, 3, pTriData->m_nInstances);

		glEnable(GL_MULTISAMPLE);
		glEnable(GL_DEPTH_TEST);

		pShader->UnUse();
	}
}
