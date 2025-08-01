﻿////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Interface define object rendering
* @file  : xglpolyrender.h
* @create: June 10, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XGLPOLYRENDER_H
#define XGLPOLYRENDER_H

#include "graphics/rendering/xrenderable.h"
#include "graphics/rendering/xrendertype.h"
#include "graphics/rendering/shape/xshapedata.h"

__BEGIN_NAMESPACE__


//struct PolyVertexData
//{
//public:
//	Vec3 position;
//	Vec4 color;
//	float thickness;
//	int polygonID;
//};
//
//class GLPolyRender : public ObjectRenderable
//{
//public:
//	GLPolyRender();
//	~GLPolyRender();
//
//public:
//	virtual bool CreateShader();
//	virtual bool CreateBuffers();
//	virtual void UpdateVertexBuffer();
//	virtual void ReleaseBuffer();
//
//public:
//	virtual void Draw(const Mat4& view, const Mat4& proj, const Vec2& viewport, const float& zoom = 1.f);
//	virtual bool BindShader();
//	virtual void UnbindShader();
//	virtual void Remake();
//
//	virtual void Update(float deltaTime);
//
//public:
//	std::vector<PolyVertexData> m_vecRenderData;
//	std::vector<unsigned int> m_vecIndices;
//
//	unsigned int m_nVao = 0;
//	unsigned int m_nVbo = 0;
//	unsigned int m_nEbo = 0;
//
//	bool m_bReloadBufferFlag = false;
//	bool m_bReloadIndexFlags = false;
//};


__END_NAMESPACE__

#endif // !XPOLYRENDER_H