////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Define interface for drawer object
* @file  : DrawSystem.h
* @create: July 06, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "RenderDef.h"
#include <unordered_map>

#include "RenderData.h"

struct DrawParams
{
	RenderContextPtr context;
	MaterialComponentPtr material;
	TransformComponentPtr transform;
};

class DrawSystem
{
public:
	virtual ~DrawSystem() = default;
	virtual void Draw(RenderDataPtr data, const DrawParams& params) = 0;
};

class DrawSystemRegistry {
public:
	template<typename T>
	void RegisterSystem(std::shared_ptr<DrawSystem> system) {
		m_map[typeid(T).hash_code()] = system;
	}

	void Draw(RenderDataPtr data, const DrawParams& params) {
		auto it = m_map.find(typeid(*data).hash_code());
		if (it != m_map.end())
			it->second->Draw(data, params);
	}

private:
	std::unordered_map<size_t, std::shared_ptr<DrawSystem>> m_map;
};