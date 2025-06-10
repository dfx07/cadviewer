////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Define object for opengl rendering
* @file  : xopenglrender.h
* @create: June 10, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XOPENGLRENDER_H
#define XOPENGLRENDER_H

#include "graphics/rendering/xtfxrender.h"
#include "OpenGL/glew.h"

__BEGIN_NAMESPACE__

class OpenGLRenderer : public TFXRenderer
{
public:
	OpenGLRenderer()
	{

	}

	~OpenGLRenderer()
	{

	}

	virtual void SetViewPort(const int x, const int y, const int width, const int height)
	{
		glViewport(x, y, width, height);
	}

	virtual void SetClearColor(const float r, const float g, const float b, const float a)
	{
		m_v4ClearColor = Vec4(r, g, b, a);
	}

	virtual void Init()
	{

	}

	virtual void Render()
	{
		if (!m_pContext->IsValid())
			return;

		if (!m_pContext->MakeCurrentContext())
			return;

		glClearColor(m_v4ClearColor.r, m_v4ClearColor.g, m_v4ClearColor.b, m_v4ClearColor.a);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		for (auto pObjRenderable : m_ObjectRenders)
		{
			pObjRenderable->Draw();
		}

		m_pContext->SwapBuffer();
	}

	virtual void Update(float deltaTime)
	{

	}

protected:
	Vec4	m_v4ClearColor;
};

__END_NAMESPACE__

#endif // !XOPENGLRENDER_H