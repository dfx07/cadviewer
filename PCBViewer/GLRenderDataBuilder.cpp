#include "GLRenderDataBuilder.h"

#include "DrawObject.h"
#include "GLRenderData.h"
#include "RenderComponent.h"

#include "graphics/rendering/shader/xglshader.h"
#include "graphics/rendering/OpenGL/glew.h"


#include "PolyDrawObject.h"
#include "LineDrawObject.h"
#include "CircleDrawObject.h"
#include "RectDrawObject.h"


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
	GLLineRenderDataPtr pData = std::make_shared<GLLineRenderData>();

	// TODO : implement create buffer
	for (auto& line : pDrawObject->m_vecLines)
	{
		Point2& ptStart = line->m_ptS;
		Point2& ptEnd = line->m_ptE;
		float z = NextZ();

		pData->m_vecRenderData.push_back({ Vec3(ptStart.x, ptStart.y, z), line->m_clColor, line->m_fThickness});
		pData->m_vecRenderData.push_back({ Vec3(ptEnd.x, ptEnd.y, z), line->m_clColor, line->m_fThickness});
	}

	pData->Create();
	pData->SetUpdateFlags(0);

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

RenderDataPtr GLRenderDataBuilder::Make(CircleDrawObjectList* pDrawObject)
{
	GLCircleRenderDataPtr pData = std::make_shared<GLCircleRenderData>();

	// TODO : implement create buffer
	for (auto& pCircle : pDrawObject->m_vecCircles)
	{
		Point2& ptCenter = pCircle->m_ptCenter;
		float radius = pCircle->m_fRadius;
		float thickness = pCircle->m_fThickness;
		Vec4 thicknessColor = pCircle->m_clThicknessColor;
		Vec4 fillColor = pCircle->m_clFillColor;

		float z = NextZ();

		pData->m_vecRenderData.push_back({ 
			Vec3(ptCenter.x, ptCenter.y, z),
			radius,
			thickness,
			thicknessColor,
			fillColor,
		});

		pData->m_nInstances++;
	}

	pData->Create();
	pData->SetUpdateFlags(0);

	auto pShader = std::make_shared<tfx::GLShaderProgram>();

	std::unordered_map<tfx::ShaderStage, std::string> shaderSrc;
	shaderSrc[tfx::ShaderStage::Vertex]   = "shaders/shape/circle.vert";
	shaderSrc[tfx::ShaderStage::Fragment] = "shaders/shape/circle.frag";
	//shaderSrc[tfx::ShaderStage::Geometry] = "shaders/shape/circle.geom";

	if (!pShader->LoadShaders(shaderSrc))
	{
		assert(0);
	}

	auto pBinder = std::make_shared<tfx::GLShaderDataBinder>(pShader->GetProgramID());

	auto pMaterial = std::make_shared<MaterialComponent>(pShader, pBinder);

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

RenderDataPtr GLRenderDataBuilder::Make(RectDrawObjectList* pDrawObject)
{
	GLRectRenderDataPtr pData = std::make_shared<GLRectRenderData>();

	// TODO : implement create buffer
	for (auto& pRect : pDrawObject->m_vecRects)
	{
		Vec2 sz = { pRect->m_fWidth + pRect->m_fThickness, pRect->m_fHeight + pRect->m_fThickness};
		Point2 pt = Vec2(pRect->m_ptY, pRect->m_ptY) + Vec2(pRect->m_fWidth, pRect->m_fHeight) / 2.f;
		float thickness = pRect->m_fThickness;
		Vec4 thicknessColor = pRect->m_clThicknessColor;
		Vec4 fillColor = pRect->m_clFillColor;
		float angle = pRect->m_fAngle;

		float z = NextZ();

		pData->m_vecRenderData.push_back({Vec3(pt.x, pt.y, z), angle, sz, thickness, thicknessColor, fillColor});

		pData->m_nInstances++;
	}

	pData->Create();
	pData->SetUpdateFlags(0);

	auto pShader = std::make_shared<tfx::GLShaderProgram>();

	std::unordered_map<tfx::ShaderStage, std::string> shaderSrc;
	shaderSrc[tfx::ShaderStage::Vertex]  = "shaders/shape/rect.vert";
	shaderSrc[tfx::ShaderStage::Fragment] = "shaders/shape/rect.frag";

	if (!pShader->LoadShaders(shaderSrc))
	{
		assert(0);
	}

	auto pBinder = std::make_shared<tfx::GLShaderDataBinder>(pShader->GetProgramID());

	auto pMaterial = std::make_shared<MaterialComponent>(pShader, pBinder);

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

