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

#include "common/typext.h"

#include <memory>
#include <vector>

class Camera;
typedef std::shared_ptr<Camera> CameraPtr;

class Camera2D;
typedef std::shared_ptr<Camera2D> Camera2DPtr;

class Camera3D;
typedef std::shared_ptr<Camera3D> Camera3DPtr;

_interface IShaderProgram;
typedef std::shared_ptr<IShaderProgram> ShaderProgramPtr;

_interface IShaderDataBinder;
typedef std::shared_ptr<IShaderDataBinder> ShaderDataBinderPtr;

_interface IFont;
typedef std::shared_ptr<IFont> FontPtr;

_interface IFontRender;
typedef std::shared_ptr<IFontRender> FontRenderPtr;

#endif // !XRENDERTYPE_H