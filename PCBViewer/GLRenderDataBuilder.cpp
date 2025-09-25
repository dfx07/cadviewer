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
#include "RenderUtil.h"


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

			pData->m_vecRenderData.push_back({ Vec3(vtxData.x, vtxData.y, z), poly->m_clColor
				, poly->m_fThickness, (int)poly->GetObjectID()});

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

	auto pMaterial = std::make_shared<MaterialComponent>();

	// Add the material component to the draw object
	pMaterial->Add("poly", pShader, pBinder);

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

bool GLRenderDataBuilder::Update(RenderDataPtr pRenderData, PolyDrawObjectList* pDrawObject)
{
	return false;
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

	auto pMaterial = std::make_shared<MaterialComponent>();

	// Add the material component to the draw object
	pMaterial->Add("line", pShader, pBinder);

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

bool GLRenderDataBuilder::Update(RenderDataPtr pRenderData, LineDrawObjectList* pDrawObject)
{
	return false;
}

RenderDataPtr GLRenderDataBuilder::Make(CircleDrawObjectList* pDrawObject)
{
	GLCircleRenderDataPtr pData = std::make_shared<GLCircleRenderData>();

	// TODO : implement create buffer
	for (auto& pCircle : pDrawObject->m_vecCircles)
	{
		Point2& ptCenter = pCircle->m_ptCenter;
		float z = NextZ();

		pData->m_vecFillRenderData.push_back({ 
			Vec3(ptCenter.x, ptCenter.y, z),
			pCircle->m_fRadius,
			pCircle->m_clFillColor,
		});


		z = NextZ();

		// Create border data
		pData->m_vecBorderRenderData.push_back({
			Vec3(ptCenter.x, ptCenter.y, z),
			pCircle->m_fRadius,
			pCircle->m_fThickness,
			pCircle->m_clThicknessColor,
		});

		pData->m_nInstances++;
	}

	pData->Create();
	pData->SetUpdateFlags(0);

	auto pMaterial = std::make_shared<MaterialComponent>();

	// Load fill shader
	{
		auto pShader = std::make_shared<tfx::GLShaderProgram>();

		std::unordered_map<tfx::ShaderStage, std::string> shaderSrc;
		shaderSrc[tfx::ShaderStage::Vertex] = "shaders/shape/circle_f.vert";
		shaderSrc[tfx::ShaderStage::Fragment] = "shaders/shape/circle_f.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<tfx::GLShaderDataBinder>(pShader->GetProgramID());

		// Add the material component to the draw object
		pMaterial->Add("circle_f", pShader, pBinder);
	}

	// Load border shader
	{
		auto pShader = std::make_shared<tfx::GLShaderProgram>();

		std::unordered_map<tfx::ShaderStage, std::string> shaderSrc;
		shaderSrc[tfx::ShaderStage::Vertex] = "shaders/shape/circle_b.vert";
		shaderSrc[tfx::ShaderStage::Fragment] = "shaders/shape/circle_b.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<tfx::GLShaderDataBinder>(pShader->GetProgramID());

		// Add the material component to the draw object
		pMaterial->Add("circle_b", pShader, pBinder);
	}

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

bool GLRenderDataBuilder::Update(RenderDataPtr pRenderData, CircleDrawObjectList* pDrawObject)
{
	return false;
}

RenderDataPtr GLRenderDataBuilder::Make(RectDrawObjectList* pDrawObject)
{
	GLRectRenderDataPtr pData = std::make_shared<GLRectRenderData>();

	Point2 ptVertices[4] = {
		{ -0.5f, -0.5f },
		{  0.5f, -0.5f },
		{  0.5f,  0.5f },
		{ -0.5f,  0.5f }
	};

	for (auto& pRect : pDrawObject->m_vecRects)
	{
		Vec2 szRect = { pRect->m_fWidth, pRect->m_fHeight };
		Point2 ptCenter = Vec2(pRect->m_ptX, pRect->m_ptY) + szRect / 2.f;

		float z = NextZ();

		size_t nSizeOffset = pData->m_vecRenderData.size();

		pData->m_vecRenderData.push_back({
			Vec3(ptCenter.x, ptCenter.y, z),
			pRect->m_fAngle,
			szRect,
			pRect->m_fThickness,
			pRect->m_clThicknessColor,
			pRect->m_clFillColor
		});

		pData->m_mapOffset.insert(std::make_pair(pRect->GetObjectID(), nSizeOffset));

		pData->m_nInstances++;
	}

	pData->Create();
	pData->SetUpdateFlags(0);

	auto pMaterial = std::make_shared<MaterialComponent>();

	// Load fill shader
	{
		auto pShader = std::make_shared<tfx::GLShaderProgram>();

		std::unordered_map<tfx::ShaderStage, std::string> shaderSrc;
		shaderSrc[tfx::ShaderStage::Vertex] = "shaders/shape/rect_f.vert";
		shaderSrc[tfx::ShaderStage::Fragment] = "shaders/shape/rect_f.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<tfx::GLShaderDataBinder>(pShader->GetProgramID());

		// Add the material component to the draw object
		pMaterial->Add("rect_f", pShader, pBinder);
	}

	// Load border shader
	{
		auto pShader = std::make_shared<tfx::GLShaderProgram>();

		std::unordered_map<tfx::ShaderStage, std::string> shaderSrc;
		shaderSrc[tfx::ShaderStage::Vertex] = "shaders/shape/rect_b.vert";
		shaderSrc[tfx::ShaderStage::Fragment] = "shaders/shape/rect_b.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<tfx::GLShaderDataBinder>(pShader->GetProgramID());

		// Add the material component to the draw object
		pMaterial->Add("rect_b", pShader, pBinder);
	}

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

bool GLRenderDataBuilder::Update(RenderDataPtr pRenderData, RectDrawObjectList* pDrawObject)
{
	//pDrawObject->Update(pRenderData, this);
	return false;
}

