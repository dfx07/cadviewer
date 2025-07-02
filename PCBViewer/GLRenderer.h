////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Contain render data cache
* @file  : RenderCache
* @create: July 02, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef GLRENDERER_H
#define XOPENGLRENDER_H

#include "OpenGL/glew.h"
#include "graphics/rendering/xrendertype.h"
#include "graphics/rendering/xtfxrender.h"

__BEGIN_NAMESPACE__

class OpenGLRenderer : public TFXRenderer
{
public:
	OpenGLRenderer();

	OpenGLRenderer(CameraPtr pCamera);

	~OpenGLRenderer();

	virtual void SetViewPort(const int x, const int y, const int width, const int height);

	virtual void SetClearColor(const float r, const float g, const float b, const float a);

	virtual void Init();

	virtual void Render();

	virtual void Update(float deltaTime);

protected:
	Vec4	m_v4ClearColor;
};

__END_NAMESPACE__

#endif // !XOPENGLRENDER_H
