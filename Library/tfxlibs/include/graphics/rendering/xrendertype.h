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

#include "core/tfx_math.h"

#include <memory>
#include <vector>
#include <string>

_interface IShaderProgram;
typedef std::shared_ptr<IShaderProgram> ShaderProgramPtr;

_interface IShaderDataBinder;
typedef std::shared_ptr<IShaderDataBinder> ShaderDataBinderPtr;


__BEGIN_NAMESPACE__

_interface _tfxIFontRender;

template<typename _TFont_>
_interface _tfxIFontAtlas;

_interface _tfxIFont;

class _tfxFontManager;

typedef Vec3 Col3;
typedef Vec4 Col4;

__END_NAMESPACE__

#endif // !XRENDERTYPE_H