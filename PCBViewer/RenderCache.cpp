#include "RenderCache.h"
#include "DrawObject.h"
#include "RenderCache.h"

#include "graphics/rendering/xrenderable.h"

RenderCache::RenderCache(RenderDataBuilderPtr pBuilder):
	m_builder(pBuilder)
{
	//m_cache = std::make_shared<RenderCache>(m_builder);
}

RenderCache::~RenderCache()
{

}

RenderDataPtr RenderCache::GetOrCreateRenderData(DrawObjectPtr pModel)
{
	auto it = m_cache.find(pModel);
	if (it != m_cache.end())
	{
		if (pModel->IsDirty())
		{
			if (pModel->Update(it->second, m_builder))
			{
				pModel->ClearDirty();
			}
		}

		return it->second;
	}
	else
	{
		auto pRenderData = pModel->Make(m_builder);
		m_cache[pModel] = pRenderData;

		return pRenderData;
	}
}

void RenderCache::Invalidate(DrawObjectPtr pModel)
{
	//auto it = m_cache.find(model);
	//if (it != m_cache.end())
	//{
	//	m_backend->DestroyRenderData(it->second);
	//	m_cache.erase(it);
	//}
}
