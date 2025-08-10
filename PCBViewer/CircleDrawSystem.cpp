#include "CircleDrawSystem.h"

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

	GLCircleRenderDataPtr pCircleData = std::dynamic_pointer_cast<GLCircleRenderData>(pRenderData);
	if (!pCircleData)
		return;

	auto pContext = params.context;
	auto pMaterial = params.material;
	auto pTransform = params.transform;

	if (!pRenderData || !pMaterial)
		return;

	// Draw fill
	{
		auto pShader = pMaterial->GetShader("circle_f");
		auto pBinder = pMaterial->GetBinder("circle_f");

		if (pShader->Use())
		{
			pBinder->SetMat4("u_Proj", RENPTR(pContext->m_Proj));
			pBinder->SetMat4("u_View", RENPTR(pContext->m_View));
			pBinder->SetMat4("u_Model", RENPTR(Mat4(1.f)));
			pBinder->SetVec2("u_Viewport", RENPTR(pContext->m_ViewPort));
			pBinder->SetFloat("u_zZoom", pContext->m_Zoom);

			glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
			glDisable(GL_MULTISAMPLE);
			glDepthMask(GL_FALSE);

			glBindVertexArray(pCircleData->m_nVao);
			glDrawElementsInstanced(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0, pCircleData->m_nInstances);

			glEnable(GL_MULTISAMPLE);
			glEnable(GL_DEPTH_TEST);
		}
		pShader->UnUse();
	}

	// Draw border
	//{
	//	auto pShader = pMaterial->GetShader("circle_b");
	//	auto pBinder = pMaterial->GetBinder("circle_b");

	//	if (pShader->Use())
	//	{
	//		pBinder->SetMat4("u_Proj", RENPTR(pContext->m_Proj));
	//		pBinder->SetMat4("u_View", RENPTR(pContext->m_View));
	//		pBinder->SetMat4("u_Model", RENPTR(Mat4(1.f)));
	//		pBinder->SetVec2("u_Viewport", RENPTR(pContext->m_ViewPort));
	//		pBinder->SetFloat("u_zZoom", pContext->m_Zoom);

	//		glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	//		glDisable(GL_MULTISAMPLE);
	//		glDepthMask(GL_FALSE);

	//		glBindVertexArray(pCircleData->m_nBorderVao);
	//		glDrawElementsInstanced(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0, pCircleData->m_nInstances);

	//		glEnable(GL_MULTISAMPLE);
	//		glEnable(GL_DEPTH_TEST);
	//	}
	//	pShader->UnUse();
	//}
}
