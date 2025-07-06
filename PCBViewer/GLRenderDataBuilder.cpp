#include "GLRenderDataBuilder.h"

#include "DrawObject.h"
#include "GLRenderData.h"
#include "RenderComponent.h"

#include "graphics/rendering/shader/xglshader.h"
#include "graphics/rendering/OpenGL/glew.h"


#include "PolyDrawObject.h"
#include "LineDrawObject.h"


float GLRenderDataBuilder::NextZ()
{
	float z = m_fCurrentZ;
	m_fCurrentZ += m_fZStep;
	return z;
}

RenderDataPtr GLRenderDataBuilder::Make(PolyDrawObjectList* pDrawObject)
{
	GLPolyRenderDataPtr pData = std::make_shared<GLPolyRenderData>();

	for (auto& poly : pDrawObject->m_vecPolys)
	{
		size_t nVertexCnt = poly->m_vecPoints.size();

		size_t nStartIndex = pData->m_vecRenderData.size();

		float z = NextZ();

		for (size_t i = 0; i < nVertexCnt; i++)
		{
			auto& vtxData = poly->m_vecPoints[i];

			pData->m_vecRenderData.push_back({ Vec3(vtxData.x, vtxData.y, z), poly->m_clColor, poly->m_fThickness, poly->GetObjectID()});

			size_t prev = nStartIndex + (i + nVertexCnt - 1) % nVertexCnt;
			size_t cur1 = nStartIndex + i;
			size_t cur2 = nStartIndex + (i + 1) % nVertexCnt;
			size_t next = nStartIndex + (i + 2) % nVertexCnt;

			pData->m_vecIndices.push_back(static_cast<unsigned int>(prev));
			pData->m_vecIndices.push_back(static_cast<unsigned int>(cur1));
			pData->m_vecIndices.push_back(static_cast<unsigned int>(cur2));
			pData->m_vecIndices.push_back(static_cast<unsigned int>(next));
		}
	}

	pData->Create();
	pData->UpdateVertexBuffer();
	pData->SetUpdateFlags(0);

	auto pShader = std::make_shared<tfx::GLShaderProgram>();

	std::unordered_map<tfx::ShaderStage, std::string> shaderSrc;
	shaderSrc[tfx::ShaderStage::Vertex]   = "shaders/shape/poly.vert";
	shaderSrc[tfx::ShaderStage::Fragment] = "shaders/shape/poly.frag"; 
	shaderSrc[tfx::ShaderStage::Geometry] = "shaders/shape/poly.geom";

	if (!pShader->LoadShaders(shaderSrc))
	{
		assert(0);
	}

	auto pBinder = std::make_shared<tfx::GLShaderDataBinder>(pShader->GetProgramID());

	auto pMaterial = std::make_shared<MaterialComponent>(pShader, pBinder);

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

RenderDataPtr GLRenderDataBuilder::Make(LineDrawObjectList* pDrawObject)
{
	GLineRenderDataPtr pData = std::make_shared<GLineRenderData>();

	// TODO : implement create buffer

	auto pShader = std::make_shared<tfx::GLShaderProgram>();

	std::unordered_map<tfx::ShaderStage, std::string> shaderSrc;
	shaderSrc[tfx::ShaderStage::Vertex]   = "shaders/shape/line.vert";
	shaderSrc[tfx::ShaderStage::Fragment] = "shaders/shape/line.frag";
	shaderSrc[tfx::ShaderStage::Geometry] = "shaders/shape/line.geom";

	if (!pShader->LoadShaders(shaderSrc))
	{
		assert(0);
	}

	auto pBinder = std::make_shared<tfx::GLShaderDataBinder>(pShader->GetProgramID());

	auto pMaterial = std::make_shared<MaterialComponent>(pShader, pBinder);

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

