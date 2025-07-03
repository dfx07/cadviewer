#include "GLRenderDataBuilder.h"

#include "PolyDrawObject.h"

#include "graphics/rendering/OpenGL/glew.h"


float GLRenderDataBuilder::NextZ()
{
	float z = m_fCurrentZ;
	m_fCurrentZ += m_fZStep;
	return z;
}

RenderDataPtr GLRenderDataBuilder::Make(const PolyDrawObjectList* model)
{
	GLPolyRenderDataPtr pData = std::make_shared<GLPolyRenderData>();

	for (auto& poly : model->m_vecPolys)
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

	pData->CreateBuffers();
	pData->UpdateVertexBuffer();

	return pData;
}

