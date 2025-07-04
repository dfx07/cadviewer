#pragma once
#include "PCBViewDef.h"

#include <memory>

class DrawObject;
typedef std::shared_ptr<DrawObject> DrawObjectPtr;

class RenderModelManager;
typedef std::shared_ptr<RenderModelManager> RenderModelManagerPtr;

class PolyDrawObject;
typedef std::shared_ptr<PolyDrawObject> PolyDrawObjectPtr;

class PolyDrawObjectList;
typedef std::shared_ptr<PolyDrawObjectList> PolyDrawObjectListPtr;
