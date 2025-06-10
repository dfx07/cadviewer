////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Interface define object for rendering
* @file  : xtfxrenderer.h
* @create: June 10, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XTFXRENDERER_H
#define XTFXRENDERER_H

#include <vector>

#include "common/tfxtype.h"
#include "graphics/camera/xcamera.h"

#include "graphics/rendering/xrenderable.h"

__BEGIN_NAMESPACE__

class TFXRenderer
{
public:
	TFXRenderer(Camera* pCamera)
		: m_pCamera(pCamera)
	{

	}

	virtual ~TFXRenderer()
	{

	}

	virtual void SetViewPort(const int x, const int y, const int width, const int height) = 0;
	virtual void SetClearColor(const float r, const float g, const float b, const float a) = 0;
	virtual void Init() = 0;
	virtual void Render() = 0;
	virtual void Update(float deltaTime) = 0;

	virtual void AddObjectRenderable(ObjectRenderable* pObject)
	{
		m_ObjectRenders.push_back(pObject);
	}

	virtual void ReserveObject(const size_t szObj)
	{
		m_ObjectRenders.reserve(szObj);
	}

protected:
	Camera* m_pCamera = nullptr;
	std::vector<ObjectRenderable*> m_ObjectRenders;
};

__END_NAMESPACE__

#endif // !XCTX_H