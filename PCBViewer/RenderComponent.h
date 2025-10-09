////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Define render component
* @file  : RenderComponent.h
* @create: July 04, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "graphics/rendering/xrendertype.h"
#include "RenderDef.h"

#include <string>
#include <unordered_map>

class Component
{
public:
	Component()
	{

	}

	virtual ~Component()
	{

	}
};

class MaterialComponent : public Component
{
public:
	MaterialComponent() : Component()
	{

	}

public:
	void Add(const char* strName, ShaderProgramPtr shader, ShaderDataBinderPtr binder)
	{
		if (strName == nullptr)
			return;

		if (shader != nullptr)
		{
			m_pShaders[strName] = shader;
		}

		if (binder != nullptr)
		{
			m_pBinders[strName] = binder;
		}
	}

public:
	ShaderProgramPtr GetShader(const char* strName = nullptr)
	{
		if (strName == nullptr)
		{
			if (m_pShaders.empty())
			{
				return nullptr;
			}

			return m_pShaders.begin()->second;
		}

		auto itShaderPtr = m_pShaders.find(strName);

		if (itShaderPtr != m_pShaders.end())
		{
			return itShaderPtr->second;
		}

		return nullptr;
	}

	ShaderDataBinderPtr GetBinder(const char* strName = nullptr)
	{
		if (strName == nullptr)
		{
			if (m_pBinders.empty())
			{
				return nullptr;
			}

			return m_pBinders.begin()->second;
		}

		auto itBinderPtr = m_pBinders.find(strName);
		if (itBinderPtr != m_pBinders.end())
		{
			return itBinderPtr->second;
		}
		return nullptr;
	}

protected:
	std::unordered_map<std::string, ShaderProgramPtr> m_pShaders;
	std::unordered_map<std::string, ShaderDataBinderPtr> m_pBinders;
};

class TransformComponent : public Component
{
public:
	TransformComponent()
	{

	}
protected:
	Mat4 m_MatModel{ 1.f };
};