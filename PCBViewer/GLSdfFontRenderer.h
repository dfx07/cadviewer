////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Render text using SDF (Signed Distance Field)
* @file  : GLSdfFontRenderer.h
* @create: Oct 06, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef GLSDFFONTRENDERER_H
#define GLSDFFONTRENDERER_H

#include "graphics/fonts/xfontrenderer.h"
#include "RenderDef.h"

class GLSdfFontRenderer : public IFontRender
{
public:
	GLSdfFontRenderer();
	virtual ~GLSdfFontRenderer() = default;

public:
	virtual bool Draw(const IFont* font, const std::string txt, float x, float y, Vec2 dir, const Col4& col);
};

#endif // !GLSDFFONTRENDERER_H
