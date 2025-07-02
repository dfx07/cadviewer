#include "GLRenderDataBuilder.h"

#include "PolyDrawObject.h"

#include "graphics/rendering/OpenGL/glew.h"


GLPolyRenderData::GLPolyRenderData()
{

}

GLPolyRenderData::~GLPolyRenderData()
{

}

RenderDataPtr GLRenderDataBuilder::Make(const PolyDrawObjectList* model)
{
	GLPolyRenderDataPtr pData = std::make_shared<GLPolyRenderData>();

	for (auto& poly : model->m_vecPolys)
	{
		size_t nVertexCnt = poly->m_vecPoints.size();

		size_t nStartIndex = pData->m_vecRenderData.size();

		for (size_t i = 0; i < nVertexCnt; i++)
		{
			auto& vtxData = poly->m_vecPoints[i];

			//float z = float(m_nNextPolygonID);
			float z = 10;

			pData->m_vecRenderData.push_back({ Vec3(vtxData.x, vtxData.y, z), poly->m_clColor, poly->m_fThickness, m_nNextPolygonID });

			unsigned int prev = nStartIndex + (i + nVertexCnt - 1) % nVertexCnt;
			unsigned int cur1 = nStartIndex + i;
			unsigned int cur2 = nStartIndex + (i + 1) % nVertexCnt;
			unsigned int next = nStartIndex + (i + 2) % nVertexCnt;

			pData->m_vecIndices.push_back(prev);
			pData->m_vecIndices.push_back(cur1);
			pData->m_vecIndices.push_back(cur2);
			pData->m_vecIndices.push_back(next);
		}
	}

	pData->m_nVao ;

	return pData;
}

