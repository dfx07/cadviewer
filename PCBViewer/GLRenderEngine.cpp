#include "GLRenderEngine.h"
#include "graphics/rendering/OpenGL/glew.h"

#include "GLRenderData.h"


GLRenderEngine::GLRenderEngine()
	: RenderEngine()
{
}

GLRenderEngine::~GLRenderEngine()
{
}

void GLRenderEngine::DrawRenderData(RenderDataPtr pRenderData, const Mat4& transform)
{
	if (!pRenderData)
		return;

	GLPolyRenderDataPtr pPolyData = std::dynamic_pointer_cast<GLPolyRenderData>(pRenderData);
	if (!pPolyData)
		return;

	if (!BindShader())
		return;

	if (m_vecRenderData.empty())
		return;

	m_pBinder->SetMat4("u_Proj", tfx::ValuePtr(proj));
	m_pBinder->SetMat4("u_View", tfx::ValuePtr(view));
	m_pBinder->SetMat4("u_Model", tfx::ValuePtr(m_matModel));
	m_pBinder->SetVec2("u_Viewport", tfx::ValuePtr(viewport));
	m_pBinder->SetFloat("u_zZoom", zoom);

	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glDisable(GL_MULTISAMPLE);
	glDepthMask(GL_FALSE);

	glBindVertexArray(pPolyData->m_nVao);
	glDrawElements(GL_LINES_ADJACENCY, pPolyData->m_vecRenderData.size() * 4, GL_UNSIGNED_INT, 0);

	glEnable(GL_MULTISAMPLE);
	glEnable(GL_DEPTH_TEST);

	UnbindShader();
}
