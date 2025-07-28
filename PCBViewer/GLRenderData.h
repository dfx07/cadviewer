////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Render Data for OpenGL
* @file  : GLRenderData.h
* @create: July 03, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include <vector>
#include <memory>

#include "RenderDef.h"
#include "RenderData.h"


/***********************************************************************************/
// Poly Draw Object Data

struct PolyVertexData
{
	Vec3 position;
	Vec4 color;
	float thickness;
	int polygonID;
};

class GLPolyRenderData : public RenderData
{
public:
	enum Flags
	{
		UpdateVertex = 1 << 0,
		UpdateIndex = 1 << 1,
	};

public:
	GLPolyRenderData();
	virtual ~GLPolyRenderData();

public:
	virtual void UpdateVertexBuffer();
	virtual void ReleaseBuffer();

public:
	virtual bool Create();
	virtual void Update();
	virtual void Release();

public:
	unsigned int m_nVao = 0;
	unsigned int m_nVbo = 0;
	unsigned int m_nEbo = 0;

	std::vector<PolyVertexData> m_vecRenderData;
	std::vector<unsigned int> m_vecIndices;
};

/*********************************************************************************/
// Line
/*********************************************************************************/


struct LineVertexData
{
	Vec3 position;
	Vec4 color;
	float thickness;
};

class GLLineRenderData : public RenderData
{
public:
	enum Flags
	{
		UpdateVertex = 1 << 0,
		UpdateIndex = 1 << 1,
	};

public:
	GLLineRenderData();
	virtual ~GLLineRenderData();

public:
	virtual bool Create();
	virtual void Update();
	virtual void Release();

public:
	unsigned int m_nVao = 0;
	unsigned int m_nVbo = 0;

	std::vector<LineVertexData> m_vecRenderData;
};

/*********************************************************************************/
// Circle
/*********************************************************************************/

struct CircleVertexData
{
	Vec3 center;
	float radius;
	float thickness;
	Vec4 thickness_color;
	Vec4 fill_color;
};

class GLCircleRenderData : public RenderData
{
public:


public:
	enum Flags
	{
		UpdateVertex = 1 << 0,
		UpdateIndex = 1 << 1,
	};

public:
	GLCircleRenderData();
	virtual ~GLCircleRenderData();

public:
	virtual bool Create();
	virtual void Update();
	virtual void Release();

public:
	unsigned int m_nVao = 0;
	unsigned int m_nVbo = 0;
	unsigned int m_nQuadVbo = 0;
	unsigned int m_nEbo = 0;

	std::vector<CircleVertexData> m_vecRenderData;
	int m_nInstances = 0;
};

/*********************************************************************************/
// Rectangle
/*********************************************************************************/
struct RectVertexData
{
	Vec3 position;		// world
	float angle;		// radian
	Vec2 size;
	float thickness;
	Vec4 thickness_color;
	Vec4 fill_color;
};

class GLRectRenderData : public RenderData
{
public:
	enum Flags
	{
		UpdateVertex = 1 << 0,
		UpdateIndex = 1 << 1,
	};
public:
	GLRectRenderData();
	virtual ~GLRectRenderData();
public:
	virtual bool Create();
	virtual void Update();
	virtual void Release();

public:
	unsigned int m_nVao = 0;
	unsigned int m_nVbo = 0;
	unsigned int m_nEbo = 0;

	unsigned int m_nVboRender = 0;

	std::vector<RectVertexData> m_vecRenderData;
	int m_nInstances = 0;
};