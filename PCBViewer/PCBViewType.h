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

class LineDrawObject;
typedef std::shared_ptr<LineDrawObject> LineDrawObjectPtr;

class LineDrawObjectList;
typedef std::shared_ptr<LineDrawObjectList> LineDrawObjectListPtr;

class CircleDrawObject;
typedef std::shared_ptr<CircleDrawObject> CircleDrawObjectPtr;

class CircleDrawObjectList;
typedef std::shared_ptr<CircleDrawObjectList> CircleDrawObjectListPtr;

class RectDrawObject;
typedef std::shared_ptr<RectDrawObject> RectDrawObjectPtr;

class RectDrawObjectList;
typedef std::shared_ptr<RectDrawObjectList> RectDrawObjectListPtr;

class TriangleDrawObject;
typedef std::shared_ptr<TriangleDrawObject> TriangleDrawObjectPtr;

class TriangleDrawObjectList;
typedef std::shared_ptr<TriangleDrawObjectList> TriangleDrawObjectListPtr;

