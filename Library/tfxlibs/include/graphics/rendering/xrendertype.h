////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Define type object for rendering
* @file  : xrendertype.h
* @create: June 10, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XRENDERTYPE_H
#define XRENDERTYPE_H

#include "common/tfxdef.h"

#include <memory>
#include <vector>

__BEGIN_NAMESPACE__

class TFXRenderer;
typedef std::shared_ptr<TFXRenderer> TFXRendererPtr;

class TFXObjectRenderable;
typedef std::shared_ptr<TFXObjectRenderable> TFXObjectRenderablePtr;
typedef std::vector<TFXObjectRenderablePtr> TFXObjectRenderablePtrList;

class TFXRenderData;
typedef std::shared_ptr<TFXRenderData> TFXRenderDataPtr;

class TFXRenderDataBuilder;
typedef std::shared_ptr<TFXRenderDataBuilder> TFXRenderDataBuilderPtr;

class Camera;
typedef std::shared_ptr<Camera> CameraPtr;

class Camera2D;
typedef std::shared_ptr<Camera2D> Camera2DPtr;

class Camera3D;
typedef std::shared_ptr<Camera3D> Camera3DPtr;

interface IShaderProgram;
typedef std::shared_ptr<IShaderProgram> ShaderProgramPtr;

interface IShaderDataBinder;
typedef std::shared_ptr<IShaderDataBinder> ShaderDataBinderPtr;

__END_NAMESPACE__

#endif // !XRENDERTYPE_H