#include "GLRenderCache.h"
#include "DrawObject.h"

RenderCache::RenderCache(RenderDataBuilderPtr pBuilder):
	m_builder(pBuilder)
{

}

RenderCache::~RenderCache()
{

}

RenderDataPtr RenderCache::GetOrCreateRenderData(DrawObjectPtr model)
{
	auto it = m_cache.find(model);
	if (it != m_cache.end())
		return it->second;

	auto pRenderData = model->Make(m_builder);
	m_cache[model] = pRenderData;


	return pRenderData;
}

void RenderCache::Invalidate(DrawObjectPtr model)
{
	//auto it = m_cache.find(model);
	//if (it != m_cache.end())
	//{
	//	m_backend->DestroyRenderData(it->second);
	//	m_cache.erase(it);
	//}
}
