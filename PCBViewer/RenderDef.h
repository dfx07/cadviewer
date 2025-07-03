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

class GLPolyRenderData;
typedef std::shared_ptr<GLPolyRenderData> GLPolyRenderDataPtr;


#endif // !RENDERDEF_H
