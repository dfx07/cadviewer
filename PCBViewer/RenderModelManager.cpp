#include "RenderModelManager.h"

int RenderModelManager::AddModel(const DrawObjectPtr pObj)
{
	int id = static_cast<int>(m_models.size());
	m_models[id] = pObj;
	return id;
}

DrawObjectPtr RenderModelManager::GetModel(const int nID)
{
	auto it = m_models.find(nID);
	if (it != m_models.end())
	{
		return it->second;
	}
	return nullptr;
}

void RenderModelManager::RemoveModel(const int nID)
{
	auto it = m_models.find(nID);
	if (it != m_models.end())
	{
		m_models.erase(it);
	}
}
