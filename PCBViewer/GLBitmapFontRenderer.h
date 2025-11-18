////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Render text using SDF (Signed Distance Field)
* @file  : GLBitmapFontRenderer.h
* @create: Oct 06, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef GLBITMAPFONTRENDERER_H
#define GLBITMAPFONTRENDERER_H

#include "graphics/fonts/xfontrenderer.h"
#include "RenderDef.h"
#include "ResourceDef.h"

class GLBitmapFontRenderer : public IFontRender
{
public:
	GLBitmapFontRenderer();
	~GLBitmapFontRenderer();

public:
	virtual bool Draw(const IFont* font, const std::string txt, float x, float y, Vec2 dir, const Col4& col);
};

#endif // !GLBITMAPFONTRENDERER_H
