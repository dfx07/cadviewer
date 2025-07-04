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
	MaterialComponent() :
		m_pShader(nullptr),
		m_pBinder(nullptr)
	{

	}

	MaterialComponent(tfx::ShaderProgramPtr shader,
		tfx::ShaderDataBinderPtr binder = nullptr) :
		m_pShader(shader),
		m_pBinder(binder)
	{

	}

public:

	tfx::ShaderProgramPtr GetShader()
	{
		return m_pShader;
	}

	tfx::ShaderDataBinderPtr GetBinder()
	{
		return m_pBinder;
	}

protected:

	tfx::ShaderProgramPtr m_pShader;
	tfx::ShaderDataBinderPtr m_pBinder;
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