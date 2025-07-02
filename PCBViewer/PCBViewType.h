#pragma once
#include "PCBViewDef.h"

#include <memory>

class DrawObject;
typedef std::shared_ptr<DrawObject> DrawObjectPtr;

class RenderModelManager;
typedef std::shared_ptr<RenderModelManager> RenderModelManagerPtr;

class IRenderDataBuilder;
typedef std::shared_ptr<IRenderDataBuilder> RenderDataBuilderPtr;

class PolyDrawObject;
typedef std::shared_ptr<PolyDrawObject> PolyDrawObjectPtr;

class PolyDrawObjectList;
typedef std::shared_ptr<PolyDrawObjectList> PolyDrawObjectListPtr;

class PolyObjectDrawer;
typedef std::shared_ptr<PolyObjectDrawer> PolyObjectDrawerPtr;

class RenderData;
typedef std::shared_ptr<RenderData> RenderDataPtr;


#include "common/tfxtype.h"

typedef tfx::Vec2 Vec2;
typedef tfx::Vec3 Vec3;
typedef tfx::Vec4 Vec4;
typedef tfx::Vec4 Col4;
typedef tfx::Mat4 Mat4;


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