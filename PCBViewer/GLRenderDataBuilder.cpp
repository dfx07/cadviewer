#include "GLRenderDataBuilder.h"

#include "DrawObject.h"
#include "RenderComponent.h"

#include "graphics/rendering/shader/xglshader.h"
#include "graphics/rendering/OpenGL/glew.h"

#include "PolyDrawObject.h"
#include "LineDrawObject.h"
#include "CircleDrawObject.h"
#include "RectDrawObject.h"
#include "TriangleDrawObject.h"
#include "TextDrawObject.h"
#include "RenderUtil.h"
#include "Font.h"
#include "FontAtlasMSDFGen.h"
#include "FontManager.h"

#include "RenderResourceManager.h"


GLRenderDataBuilder::GLRenderDataBuilder(RenderResourceManagerPtr pFontAtlasManager/* = nullptr*/) :
	RenderDataBuilder(),
	m_pRenderResourceManger(pFontAtlasManager)
{
}

GLRenderDataBuilder::~GLRenderDataBuilder()
{
}

RectVertexData GLRenderDataBuilder::Build(RectDrawObject* pRectObj)
{
	float z = NextZ();

	Vec2 szRect = { pRectObj->m_fWidth, pRectObj->m_fHeight };
	Point2 ptCenter = Vec2(pRectObj->m_ptX, pRectObj->m_ptY) + szRect / 2.f;

	return RectVertexData({
		Vec3(ptCenter.x, ptCenter.y, z),
		pRectObj->m_fAngle,
		szRect,
		pRectObj->m_fThickness,
		pRectObj->m_clThicknessColor,
		pRectObj->m_clFillColor
	});
}

float GLRenderDataBuilder::NextZ()
{
	float z = m_fCurrentZ;
	m_fCurrentZ += m_fZStep;
	return z;
}

void GLRenderDataBuilder::SetFontAtlasManager(RenderResourceManagerPtr pResourceManager)
{
	m_pRenderResourceManger = pResourceManager;
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
	pData->SetFlags(0);

	auto pShader = std::make_shared<GLShaderProgram>();

	std::unordered_map<ShaderStage, std::string> shaderSrc;
	shaderSrc[ShaderStage::Vertex]   = "shaders/shape/poly.vert";
	shaderSrc[ShaderStage::Fragment] = "shaders/shape/poly.frag"; 
	shaderSrc[ShaderStage::Geometry] = "shaders/shape/poly.geom";

	if (!pShader->LoadShaders(shaderSrc))
	{
		assert(0);
	}

	auto pBinder = std::make_shared<GLShaderDataBinder>(pShader->GetProgramID());

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
	//for (auto& line : pDrawObject->m_vecLines)
	//{
	//	Point2& ptStart = line->m_ptS;
	//	Point2& ptEnd = line->m_ptE;
	//	float z = NextZ();

	//	pData->m_vecRenderData.push_back({ Vec3(ptStart.x, ptStart.y, z), line->m_clColor, line->m_fThickness});
	//	pData->m_vecRenderData.push_back({ Vec3(ptEnd.x, ptEnd.y, z), line->m_clColor, line->m_fThickness});
	//}

	for (auto& line : pDrawObject->m_vecLines)
	{
		Point2& ptStart = line->m_ptS;
		Point2& ptEnd = line->m_ptE;
		float z = NextZ();

		pData->m_vecRenderData.push_back(
			{ 
				Vec3(ptStart.x, ptStart.y, z),
				Vec3(ptEnd.x , ptEnd.y, z),
				Vec4(line->m_clColor),
				Vec4(line->m_clColor),
				line->m_fThickness 
			}
		);

		pData->m_nInstances++;
	}

	pData->Create();
	pData->SetFlags(0);

	auto pShader = std::make_shared<GLShaderProgram>();

	std::unordered_map<ShaderStage, std::string> shaderSrc;
	shaderSrc[ShaderStage::Vertex]   = "shaders/shape/line.vert";
	shaderSrc[ShaderStage::Fragment] = "shaders/shape/line.frag";
	//shaderSrc[ShaderStage::Geometry] = "shaders/shape/line.geom";

	if (!pShader->LoadShaders(shaderSrc))
	{
		assert(0);
	}

	auto pBinder = std::make_shared<GLShaderDataBinder>(pShader->GetProgramID());

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
	pData->SetFlags(0);

	auto pMaterial = std::make_shared<MaterialComponent>();

	// Load fill shader
	{
		auto pShader = std::make_shared<GLShaderProgram>();

		std::unordered_map<ShaderStage, std::string> shaderSrc;
		shaderSrc[ShaderStage::Vertex] = "shaders/shape/circle_f.vert";
		shaderSrc[ShaderStage::Fragment] = "shaders/shape/circle_f.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<GLShaderDataBinder>(pShader->GetProgramID());

		// Add the material component to the draw object
		pMaterial->Add("circle_f", pShader, pBinder);
	}

	// Load border shader
	{
		auto pShader = std::make_shared<GLShaderProgram>();

		std::unordered_map<ShaderStage, std::string> shaderSrc;
		shaderSrc[ShaderStage::Vertex] = "shaders/shape/circle_b.vert";
		shaderSrc[ShaderStage::Fragment] = "shaders/shape/circle_b.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<GLShaderDataBinder>(pShader->GetProgramID());

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

	for (auto pRect : pDrawObject->m_vecRects)
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

		auto rectData = Build(pRect.get());

		pData->m_mapOffset.insert(std::make_pair(pRect->GetObjectID(), nSizeOffset));

		pData->m_nInstances++;
	}

	pData->Create();
	pData->SetFlags(0);

	auto pMaterial = std::make_shared<MaterialComponent>();

	// Load fill shader
	{
		auto pShader = std::make_shared<GLShaderProgram>();

		std::unordered_map<ShaderStage, std::string> shaderSrc;
		shaderSrc[ShaderStage::Vertex] = "shaders/shape/rect_f.vert";
		shaderSrc[ShaderStage::Fragment] = "shaders/shape/rect_f.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<GLShaderDataBinder>(pShader->GetProgramID());

		// Add the material component to the draw object
		pMaterial->Add("rect_f", pShader, pBinder);
	}

	// Load border shader
	{
		auto pShader = std::make_shared<GLShaderProgram>();

		std::unordered_map<ShaderStage, std::string> shaderSrc;
		shaderSrc[ShaderStage::Vertex] = "shaders/shape/rect_b.vert";
		shaderSrc[ShaderStage::Fragment] = "shaders/shape/rect_b.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<GLShaderDataBinder>(pShader->GetProgramID());

		// Add the material component to the draw object
		pMaterial->Add("rect_b", pShader, pBinder);
	}

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

bool GLRenderDataBuilder::Update(RenderDataPtr pRenderData, RectDrawObjectList* pDrawObject)
{
	auto pRectRenderData = std::dynamic_pointer_cast<GLRectRenderDataPtr::element_type>(pRenderData);
	NULL_RETURN(pRectRenderData, false);

	const std::vector<RectDrawObjectPtr>& vecUpdateRects = pDrawObject->GetUpdateList();
	auto& mapOffset = pRectRenderData->m_mapOffset;

	size_t szUpdateObjCnt = 0;

	// Update the render data for set of update objects.
	for (auto pRectObj : vecUpdateRects)
	{
		if (!pRectObj)
			continue;

		ObjectID id = pRectObj->GetObjectID();

		auto itOffsetFound = mapOffset.find(id);

		if (itOffsetFound == mapOffset.end())
			continue;

		size_t szOffset = itOffsetFound->second;
		auto rectData = Build(pRectObj.get());

		pRectRenderData->UpdateData(szOffset, rectData);

		szUpdateObjCnt++;
	}

	if (szUpdateObjCnt > 0)
		pRectRenderData->SetFlags(GLRectRenderData::Flags::UpdateAllData);

	pRectRenderData->Update();

	pDrawObject->ClearUpdateList();

	return true;
}

RenderDataPtr GLRenderDataBuilder::Make(TriangleDrawObjectList* pDrawObject)
{
	auto pData = std::make_shared<GLTriangleRenderData>();

	for (auto pTrig : pDrawObject->m_vecTrigs)
	{
		float z = NextZ();

		size_t nSizeOffset = pData->m_vecRenderData.size();

		pData->m_vecRenderData.push_back({
			Vec3(pTrig->m_pt1.x, pTrig->m_pt1.y, z),
			Vec3(pTrig->m_pt2.x, pTrig->m_pt2.y, z),
			Vec3(pTrig->m_pt3.x, pTrig->m_pt3.y, z),
			pTrig->m_fThickness,
			pTrig->m_clThicknessColor,
			pTrig->m_clColor
		});

		pData->m_mapOffset.insert(std::make_pair(pTrig->GetObjectID(), nSizeOffset));

		pData->m_nInstances++;
	}

	pData->Create();
	pData->SetFlags(0);

	auto pMaterial = std::make_shared<MaterialComponent>();

	// Load fill shader
	{
		auto pShader = std::make_shared<GLShaderProgram>();

		std::unordered_map<ShaderStage, std::string> shaderSrc;
		shaderSrc[ShaderStage::Vertex] = "shaders/shape/trig_f.vert";
		shaderSrc[ShaderStage::Fragment] = "shaders/shape/trig_f.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<GLShaderDataBinder>(pShader->GetProgramID());

		// Add the material component to the draw object
		pMaterial->Add("trig_f", pShader, pBinder);
	}

	// Load border shader
	{
		auto pShader = std::make_shared<GLShaderProgram>();

		std::unordered_map<ShaderStage, std::string> shaderSrc;
		shaderSrc[ShaderStage::Vertex] = "shaders/shape/trig_b.vert";
		shaderSrc[ShaderStage::Fragment] = "shaders/shape/trig_b.frag";

		if (!pShader->LoadShaders(shaderSrc))
			assert(0);

		auto pBinder = std::make_shared<GLShaderDataBinder>(pShader->GetProgramID());

		// Add the material component to the draw object
		pMaterial->Add("trig_b", pShader, pBinder);
	}

	pDrawObject->AddComponent(pMaterial);

	return pData;
}

bool GLRenderDataBuilder::Update(RenderDataPtr pRenderData, TriangleDrawObjectList* pDrawObject)
{
	return false;
}

RenderDataPtr GLRenderDataBuilder::Make(TextDrawObjectList* pDrawObject)
{
	auto pData = std::make_shared<GLTextRenderData>();

	auto pFontAtlasMana = m_pRenderResourceManger->GetFontAtlasMana();

	for (auto pTextObj : pDrawObject->m_vecTexts)
	{
		auto pFont = pTextObj->m_font;

		std::string strKey = pFont->GetGUID();

		if (pTextObj->m_eRenderType == ETextRenderType::SDF)
		{
			auto pSdfRenderData = pData->m_pSDFRenderData;

			auto pAtlasPtr = pFontAtlasMana->Get(strKey);

			if (pAtlasPtr == nullptr)
			{
				auto pNewAtlasPtr = std::make_shared<FontAtlasMSDFGen>();

				if (pNewAtlasPtr->BuildFromFont(pFont.get(), 12))
				{
					pFontAtlasMana->Add(pTextObj->m_font->GetGUID(), pNewAtlasPtr);
					pAtlasPtr = pNewAtlasPtr;
				}
				else
				{
					return nullptr;
				}
			}

			float fZ = NextZ();

			float fNextPosX = pTextObj->m_pt.x;
			float fPosY = pTextObj->m_pt.y;

			CharGlyphDataList glyphLists;

			for (auto& ch : pTextObj->m_data)
			{
				if (auto pGlyphBase = pAtlasPtr->GetGlyph(ch))
				{
					CharGlyphData glyphChar;

					glyphChar.char_code = ch;
					glyphChar.color = pTextObj->m_clColor;
					glyphChar.dir = Vec2(1, 0);

					Point2 ptGlyphDraw;
					ptGlyphDraw.x = fNextPosX + pGlyphBase->bearingX;
					ptGlyphDraw.y = fPosY + pGlyphBase->bearingY;

					glyphChar.pos = Vec3(ptGlyphDraw, fZ);

					fNextPosX += pGlyphBase->advanceX;
					glyphLists.push_back(glyphChar);
				}
			}
			pSdfRenderData->Add(pAtlasPtr, glyphLists);
		}
		else if (pTextObj->m_eRenderType == ETextRenderType::Bitmap)
		{
			// TODO : 
		}
	}

	return pData;
}

bool GLRenderDataBuilder::Update(RenderDataPtr pRenderData, TextDrawObjectList* pDrawObject)
{
	return false;
}

RenderDataPtr GLRenderDataBuilder::MakeTextBitmap(TextDrawObject* pDrawObject)
{
	return RenderDataPtr();
}

bool GLRenderDataBuilder::UpdateTextBitmap(RenderDataPtr pRenderData, TextDrawObject* pDrawObject)
{
	return false;
}

RenderDataPtr GLRenderDataBuilder::MakeTextSdf(TextDrawObject* pDrawObject)
{
	return RenderDataPtr();
}

bool GLRenderDataBuilder::UpdateTextSdf(RenderDataPtr pRenderData, TextDrawObject* pDrawObject)
{
	return false;
}

