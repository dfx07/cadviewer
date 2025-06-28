#include "PolyObjectDrawer.h"
#include "PolyDrawObject.h"

PolyObjectDrawer::PolyObjectDrawer(RenderModelManagerPtr pModelManager, int nID):
	m_pModelManager(pModelManager)
{
	m_nModelID = nID;
}

PolyObjectDrawer::~PolyObjectDrawer()
{

}

void PolyObjectDrawer::Clear()
{
	m_vecRenderData.clear();
	m_vecIndices.clear();
	m_nNextPolygonID = 0;
}

void PolyObjectDrawer::Remake()
{
	if (m_pModelManager == nullptr)
		return;

	auto pPolyData = std::dynamic_pointer_cast<PolyDrawObjectList>(m_pModelManager->GetModel(m_nModelID));

	if (pPolyData == nullptr || !m_vecRenderData.empty())
		return;

	Clear();

	AddListPolyData(pPolyData);

	UpdateVertexBuffer();
}

void PolyObjectDrawer::AddPolyData(PolyDrawObjectPtr polydata)
{
	size_t nVertexCnt = polydata->m_vecPoints.size();

	size_t nStartIndex = m_vecRenderData.size();

	for (size_t i = 0; i < nVertexCnt; i++)
	{
		auto& vtxData = polydata->m_vecPoints[i];

		float z = m_nNextPolygonID + float(i) / nVertexCnt + 20;

		m_vecRenderData.push_back({ tfx::Vec3(vtxData.x, vtxData.y, z), polydata->m_clColor, polydata->m_fThickness, m_nNextPolygonID });

		unsigned int prev = nStartIndex + (i + nVertexCnt - 1) % nVertexCnt;
		unsigned int cur1 = nStartIndex + i;
		unsigned int cur2 = nStartIndex + (i + 1) % nVertexCnt;
		unsigned int next = nStartIndex + (i + 2) % nVertexCnt;

		m_vecIndices.push_back(prev);
		m_vecIndices.push_back(cur1);
		m_vecIndices.push_back(cur2);
		m_vecIndices.push_back(next);
	}

	m_nNextPolygonID++;

	m_bReloadBufferFlag = true;
	m_bReloadIndexFlags = true;
}

void PolyObjectDrawer::AddListPolyData(PolyDrawObjectListPtr polydataList)
{
	if (polydataList == nullptr)
		return;

	for (auto& poly : polydataList->m_vecPolys)
	{
		AddPolyData(poly);
	}
}
