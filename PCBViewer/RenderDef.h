﻿////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Define type and object for rendering
* @file  : RenderDef.h
* @create: July 02, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef RENDERDEF_H
#define RENDERDEF_H

#include "common/tfxtype.h"

#include <memory>

typedef tfx::Vec2 Vec2;
typedef tfx::Vec3 Vec3;
typedef tfx::Vec4 Vec4;
typedef tfx::Vec4 Col4;
typedef tfx::Mat4 Mat4;

struct ViewPort
{
	int x;
	int y;
	int width;
	int height;
};

class RenderData;
typedef std::shared_ptr<RenderData> RenderDataPtr;

class RenderEngine;
typedef std::shared_ptr<RenderEngine> RenderEnginePtr;

class RenderCache;
typedef std::shared_ptr<RenderCache> RenderCachePtr;

class RenderContext;
typedef std::shared_ptr<RenderContext> RenderContextPtr;

class Renderer;
typedef std::shared_ptr<Renderer> RendererPtr;

class RenderDataBuilder;
typedef std::shared_ptr<RenderDataBuilder> RenderDataBuilderPtr;

class GLPolyRenderData;
typedef std::shared_ptr<GLPolyRenderData> GLPolyRenderDataPtr;

template <typename T>
struct SharedPtrAddrHash {
	std::size_t operator()(const std::shared_ptr<T>& ptr) const {
		return std::hash<T*>()(ptr.get());
	}
};

template <typename T>
struct SharedPtrAddrEqual {
	bool operator()(const std::shared_ptr<T>& lhs, const std::shared_ptr<T>& rhs) const {
		return lhs.get() == rhs.get();
	}
};

#include <unordered_map>

template <typename T, typename V>
using unordered_map_shared_ptr = std::unordered_map<
	std::shared_ptr<T>,
	V,
	SharedPtrAddrHash<T>,
	SharedPtrAddrEqual<T>
>;

class Component;
typedef std::shared_ptr<Component> ComponentPtr;

class MaterialComponent;
typedef std::shared_ptr<MaterialComponent> MaterialComponentPtr;

class TransformComponent;
typedef std::shared_ptr<TransformComponent> TransformComponentPtr;

#endif // !RENDERDEF_H
