////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Interface define font for rendering
* @file  : xfontrenderer.h
* @create: oct 06, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XFONTRENDERER_H
#define XFONTRENDERER_H

#include <string>

#include "graphics/rendering/xrendertype.h"
#include "core/tfx_math.h"

__BEGIN_NAMESPACE__

_interface IFontRender
{
public:
	virtual ~IFontRender() = default;
public:
	virtual bool Draw(const IFont * font, const std::string txt, float x, float y, Vec2 dir, const Col4& col) = 0;
};

__END_NAMESPACE__

#endif // !XFONTRENDERER_H