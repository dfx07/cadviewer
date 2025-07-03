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