#include "GLRenderEngine.h"
#include "graphics/rendering/OpenGL/glew.h"

#include "GLRenderData.h"
#include "RenderComponent.h"

#include "graphics/rendering/shader/xglshader.h"
#include "common/tfxtype.h"


GLRenderEngine::GLRenderEngine()
	: RenderEngine()
{
}

GLRenderEngine::~GLRenderEngine()
{
}

void GLRenderEngine::DrawRenderData(RenderDataPtr pRenderData, RenderContextPtr pContext,
	MaterialComponentPtr pMaterial, TransformComponentPtr pTranform)
{
	if (!pRenderData || !pMaterial)
		return;

	GLPolyRenderDataPtr pPolyData = std::dynamic_pointer_cast<GLPolyRenderData>(pRenderData);
	if (!pPolyData)
		return;

	auto pShader = pMaterial->GetShader();
	auto pBinder = pMaterial->GetBinder();

	if (!pShader->Use())
		return;

	pBinder->SetMat4("u_Proj", tfx::ValuePtr(pContext->m_Proj));
	pBinder->SetMat4("u_View", tfx::ValuePtr(pContext->m_View));
	pBinder->SetMat4("u_Model", tfx::ValuePtr(Mat4(1.f)));
	pBinder->SetVec2("u_Viewport", tfx::ValuePtr(pContext->m_ViewPort));
	pBinder->SetFloat("u_zZoom", pContext->m_Zoom);

	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glDisable(GL_MULTISAMPLE);
	glDepthMask(GL_FALSE);

	glBindVertexArray(pPolyData->m_nVao);
	glDrawElements(GL_LINES_ADJACENCY, pPolyData->m_vecRenderData.size() * 4, GL_UNSIGNED_INT, 0);

	glEnable(GL_MULTISAMPLE);
	glEnable(GL_DEPTH_TEST);

	pShader->UnUse();
}
