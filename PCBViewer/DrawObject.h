#pragma once

#include <memory>
#include <typeindex>
#include <unordered_map>

#include "RenderDef.h"
#include "PCBViewType.h"
#include "RenderComponent.h"


class DrawObject;
typedef std::shared_ptr<DrawObject> DrawObjectPtr;

class RenderData;
typedef std::shared_ptr<RenderData> RenderDataPtr;

class RenderDataBuilder;
typedef std::shared_ptr<RenderDataBuilder> RenderDataBuilderPtr;


class DrawObject
{
public:
	DrawObject();
	virtual ~DrawObject();

public:
	virtual DrawObjectPtr Clone() = 0;
	virtual void Copy(DrawObject* pSource) = 0;

public:
	int GetObjectID() const
	{
		return m_nObjectID;
	}

	void SetObjectID(const int nID)
	{
		m_nObjectID = nID;
	}

public:
	void* GetModel() const { return m_pModel; }

	template<typename T>
	std::shared_ptr<T> GetComponent();

	template<typename T>
	void AddComponent(std::shared_ptr<T> comp);

public:
	virtual RenderDataPtr Make(RenderDataBuilderPtr builder);

protected:
	int m_nObjectID{ -1 };
	void* m_pModel{ nullptr };
	std::unordered_map<std::type_index, std::shared_ptr<Component>> m_components;
};

template<typename T>
inline std::shared_ptr<T> DrawObject::GetComponent()
{
	static_assert(std::is_base_of<Component, T>::value, "T must be derived from Component");

	auto it = m_components.find(typeid(T));
	if (it != m_components.end())
	{
		return std::dynamic_pointer_cast<T>(it->second);
	}
	return nullptr;
}

template<typename T>
inline void DrawObject::AddComponent(std::shared_ptr<T> comp)
{
	static_assert(std::is_base_of<Component, T>::value, "T must be derived from Component");
	m_components[typeid(T)] = comp;
}
