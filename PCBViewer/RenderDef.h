////////////////////////////////////////////////////////////////////////////////////
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

#include "core/tfx_math.h"
#include "graphics/rendering/xrendertype.h"
#include "graphics/camera/xcamera.h"

#include <memory>

typedef tfx::Vec2 Vec2;
typedef tfx::Vec3 Vec3;
typedef tfx::Vec4 Vec4;
typedef tfx::Vec2 Point2;
typedef tfx::Vec3 Point3;
typedef tfx::Vec3 Col3;
typedef tfx::Vec4 Col4;
typedef tfx::Mat4 Mat4;
typedef tfx::Mat3 Mat3;

template<typename T>
auto RENPTR(const T& obj) -> decltype(tfx::ValuePtr(obj)) {
	return tfx::ValuePtr(obj);
};

enum ETextRenderType
{
	Bitmap,
	SDF,
};

struct ViewPort
{
	int x;
	int y;
	int width;
	int height;
};

typedef tfx::_tfxCamera Camera;
typedef std::shared_ptr<Camera> CameraPtr;

typedef tfx::_tfxCamera2D Camera2D;
typedef std::shared_ptr<Camera2D> Camera2DPtr;

typedef tfx::_tfxCamera3D Camera3D;
typedef std::shared_ptr<Camera3D> Camera3DPtr;

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

class GLLineRenderData;
typedef std::shared_ptr<GLLineRenderData> GLLineRenderDataPtr;

class GLCircleRenderData;
typedef std::shared_ptr<GLCircleRenderData> GLCircleRenderDataPtr;

class GLRectRenderData;
typedef std::shared_ptr<GLRectRenderData> GLRectRenderDataPtr;

class GLTriangleRenderData;
typedef std::shared_ptr<GLTriangleRenderData> GLTriangleRenderDataPtr;

class GLTextRenderData;
typedef std::shared_ptr<GLTextRenderData> GLTextRenderDataPtr;

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

class DrawSystem;
typedef std::shared_ptr<DrawSystem> DrawSystemPtr;

class PolyDrawSystem;
typedef std::shared_ptr<PolyDrawSystem> PolyDrawSystemPtr;

class DrawSystemRegistry;
typedef std::shared_ptr<DrawSystemRegistry> DrawSystemRegistryPtr;

#endif // !RENDERDEF_H
