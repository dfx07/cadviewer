#include "FontManager.h"

//bool FontManager::Add(const std::string& name, FontPtr& font)
//{
//	return m_objects.insert({ name, font }).second;
//}


bool FontManager::Remove(const std::string& name)
{
	auto itF = m_objects.find(name);
	if (itF == m_objects.end())
		return true;

	itF->second->Release();

	m_objects.erase(itF);

	return true;
}
