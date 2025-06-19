#include "PolyObjectDrawer.h"
#include "PolyDrawObject.h"

PolyObjectDrawer::PolyObjectDrawer()
{
}

PolyObjectDrawer::~PolyObjectDrawer()
{
}

void PolyObjectDrawer::Remake()
{
	auto pPolyData = std::dynamic_pointer_cast<PolyDrawObject>(m_pModelManager->GetModel(m_nModelID));

	if (pPolyData == nullptr)
		return;

	UpdateVertexBuffer();
}

void PolyObjectDrawer::AddPolyData(PolyDrawObjectPtr polydata)
{
	size_t nVertexCnt = polydata->m_vecPoints.size();

	for (size_t i = 0; i < nVertexCnt; i++)
	{
		auto& vtxData = polydata->m_vecPoints[i];

		m_vecRenderData.push_back({ tfx::Vec3(vtxData.x, vtxData.y, 12), polydata->m_clColor, polydata->m_fThickness });

		unsigned int prev = (i + nVertexCnt - 1) % nVertexCnt;
		unsigned int cur1 = i;
		unsigned int cur2 = (i + 1) % nVertexCnt;
		unsigned int next = (i + 2) % nVertexCnt;

		m_vecIndices.push_back(prev);
		m_vecIndices.push_back(cur1);
		m_vecIndices.push_back(cur2);
		m_vecIndices.push_back(next);
	}

	m_bReloadBufferFlag = true;
	m_bReloadIndexFlags = true;
}