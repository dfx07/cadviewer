////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     tfx_type_traits.h
* @create   Oct 06, 2025
* @brief    Define template structure
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/

#ifndef TFX_TYPE_TRAITS_H
#define TFX_TYPE_TRAITS_H

#include "tfx_def.h"

#include <map>
#include <memory>
#include <type_traits>


__BEGIN_NAMESPACE__

template<typename> struct is_shared_ptr : std::false_type {};
template<typename U> struct is_shared_ptr<std::shared_ptr<U>> : std::true_type {};

template<typename> struct is_unique_ptr : std::false_type {};
template<typename U> struct is_unique_ptr<std::unique_ptr<U>> : std::true_type {};

template<typename> struct is_raw_ptr : std::false_type {};
template<typename U> struct is_raw_ptr<U*> : std::true_type {};




__NAMESPACE_SEC__(tsp)
	template<typename U> U* ToPtr(U& obj) { return &obj; }
	template<typename U> U* ToPtr(U* ptr) { return ptr; }
	template<typename U> U* ToPtr(const std::shared_ptr<U>& ptr) { return ptr.get(); }
	template<typename U> U* ToPtr(const std::unique_ptr<U>& ptr) { return ptr.get(); }
__END_NAMESPACE__

/*
	Template class manager
	Note : when using std::unique_ptr, you must pass it by std::move with lvalue
*/
template<typename K, typename T>
class _tfxManager
{
public:
	typedef T type;

public:
	bool Add(const K& key, const type& object)
	{
		return m_objects.emplace(key, object).second;
	}

	bool Add(const K& key, type&& object)
	{
		return m_objects.emplace(key, std::move(object)).second;
	}

	bool Remove(const K& key)
	{
		return m_objects.erase(key) > 0;
	}

	template<typename U = T>
	auto Get(const K& key) const -> std::enable_if_t<std::is_fundamental<U>::value, U>
	{
		auto it = m_objects.find(key);
		if (it != m_objects.end())
			return it->second;

		return T{};
	}

	template<typename U = T>
	auto Get(const K& key) const -> typename std::enable_if_t<
		std::is_same<U, std::shared_ptr<typename U::element_type>>::value, U>
	{
		auto it = m_objects.find(key);
		if (it != m_objects.end())
			return it->second;
		return U{};  // empty shared_ptr
	}

	template<typename U = T>
	auto Get(const K& key) const -> typename std::enable_if_t<
		std::is_same<U, std::unique_ptr<typename U::element_type>>::value, typename U::element_type*>
	{
		auto it = m_objects.find(key);
		if (it != m_objects.end())
			return tsp::ToPtr(it->second);
		return nullptr;
	}

	template<typename U = T>
	auto Get(const K& key) const
		-> typename std::enable_if_t<std::integral_constant<bool,
		(std::is_class<U>::value &&
			!is_shared_ptr<U>::value &&
			!is_unique_ptr<U>::value)>::value, U*>
	{
		auto it = m_objects.find(key);
		if (it != m_objects.end())
			return const_cast<U*>(tsp::ToPtr(it->second));
		return nullptr;
	}

	template<typename U = T>
	auto Get(const K& key) const
		-> typename std::enable_if_t<is_raw_ptr<U>::value, U>
	{
		auto it = m_objects.find(key);
		if (it != m_objects.end())
			return it->second;
		return nullptr;
	}

private:
	std::map<K, T> m_objects;
};

__END_NAMESPACE__


#endif // !TFX_TYPE_TRAITS_H
