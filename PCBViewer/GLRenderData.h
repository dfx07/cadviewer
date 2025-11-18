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
#include <unordered_map>

#include "RenderDef.h"
#include "RenderData.h"


/***********************************************************************************/
// Poly Draw Object Data
/***********************************************************************************/
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


/***********************************************************************************/
// Line
/***********************************************************************************/
struct LineVertexData
{
	Vec3 pos_s;
	Vec3 pos_e;
	Vec4 color_s;
	Vec4 color_e;
	float thickness;
};

class GLLineRenderData : public RenderData
{
public:
	enum Flags
	{
		UpdateVertex = 1 << 0,
		UpdateData = 1 << 1,
	};

public:
	GLLineRenderData();
	virtual ~GLLineRenderData();

protected:
	bool CreateBuffersAndVAO();
	bool BuildRenderData();

public:
	virtual bool Create();
	virtual void Update();
	virtual void Release();

public:
	unsigned int m_nVao = 0;
	unsigned int m_nVbo = 0;

	unsigned int m_nVboInst = 0;
	unsigned int m_nEboInst = 0;

	static float s_quadVertices[8];
	static unsigned int s_quadIndices[8];

	std::vector<LineVertexData> m_vecRenderData;
	int m_nInstances = 0;
};


/***********************************************************************************/
// Circle
/***********************************************************************************/
struct CircleFillVertexData
{
	Vec3 center;
	float radius;
	Vec4 color;
};

struct CircleBorderVertexData
{
	Vec3 center;
	float radius;
	float thickness;
	Vec4 color;
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

protected:
	bool CreateBorderRender();
	bool CreateFillRender();

	void ReleaseBorderRender();
	void ReleaseFillRender();

	void RemoveData();

public:
	virtual bool Create();
	virtual void Update();
	virtual void Release();

public:
	unsigned int m_nVao = 0;
	unsigned int m_nVbo = 0;
	unsigned int m_nIncVbo = 0; // Quad
	unsigned int m_nEbo = 0;

	unsigned int m_nBorderVao = 0;
	unsigned int m_nBorderVbo = 0;

	std::vector<CircleFillVertexData> m_vecFillRenderData;
	std::vector<CircleBorderVertexData> m_vecBorderRenderData;
	int m_nInstances = 0;
};


/***********************************************************************************/
// Rectangle
/***********************************************************************************/
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
		UpdateVertex  = 1 << 0,
		UpdateAllData = 1 << 1,
		UpdateElmData = 1 << 2
	};
public:
	GLRectRenderData();
	virtual ~GLRectRenderData();

protected:
	bool CreateBuffersAndVAO();
	bool BuildRenderData();

	void ReleaseBorderRender();
	void ReleaseFillRender();

	void RemoveData();

public:
	virtual bool Create();
	virtual void Update();
	virtual void Release();

public:
	bool UpdateData(size_t offset, const RectVertexData& data);
	bool AddData(const RectVertexData& data);

public:
	unsigned int m_nVao = 0;
	unsigned int m_nVbo = 0;
	unsigned int m_nEbo = 0;
	unsigned int m_nVboInst = 0;
	unsigned int m_nEboInst = 0;

	unsigned int m_nBorderVao = 0;
	unsigned int m_nBorderVbo = 0;
	unsigned int m_nBorderEbo = 0;

	static float s_quadVertices[8];
	static unsigned int s_quadIndices[8];

	std::vector<RectVertexData> m_vecRenderData;
	std::unordered_map<size_t, size_t> m_mapOffset;		// map object id and render data offset.
	size_t m_nInstances = 0;
};


/***********************************************************************************/
// Triangle
/***********************************************************************************/
struct TriangleVertexData
{
	Vec3 pos1;
	Vec3 pos2;
	Vec3 pos3;
	float thickness;
	Vec4 thickness_color;
	Vec4 fill_color;
};

class GLTriangleRenderData : public RenderData
{
public:
	enum Flags
	{
		UpdateVertex  = 1 << 0,
		UpdateAllData = 1 << 1,
		UpdateElmData = 1 << 2
	};

public:
	GLTriangleRenderData();
	virtual ~GLTriangleRenderData();

protected:
	bool CreateBuffersAndVAO();
	bool BuildRenderData();

public:
	bool UpdateData(size_t offset, const TriangleVertexData& data);
	bool AddData(const TriangleVertexData& data);

public:
	virtual bool Create();
	virtual void Update();
	virtual void Release();

public:
	unsigned int m_nVao = 0;
	unsigned int m_nVbo = 0;

	unsigned int m_nVboInst = 0;
	unsigned int m_nEboInst = 0;

	static float s_trigVertices[6];

	std::vector<TriangleVertexData> m_vecRenderData;
	std::unordered_map<size_t, size_t> m_mapOffset;		// map object id and render data offset.
	int m_nInstances = 0;
};

/***********************************************************************************/
// Triangle
/***********************************************************************************/

#include "graphics/fonts/xfontatlas.h"
#include <map>
#include "RenderDef.h"

struct CharGlyphData
{
	Vec3 pos;
	Vec4 color;
	Vec2 dir;
	uint32_t char_code;
};

typedef std::vector<CharGlyphData> CharGlyphDataList;

class GLBitmapTextRenderData : public RenderData
{
public:
	GLBitmapTextRenderData();
	virtual ~GLBitmapTextRenderData();

public:
	virtual bool Create() override;

public:
	bool Add(FontAtlasPtr fontAtl, CharGlyphDataList& charList);

public:
	int m_nInstances = 0;
};

class GLSDFTextRenderData : public RenderData
{
public:
	GLSDFTextRenderData();
	virtual ~GLSDFTextRenderData();

public:
	virtual bool Create() override;

public:
	bool Add(FontAtlasPtr fontAtl, CharGlyphDataList& charList);

public:
	std::map<FontAtlasPtr, CharGlyphDataList> m_mapSdfRenderData;
	std::map<FontAtlasPtr, unsigned int> m_mapFontAtlSSBOGlyph;

	int m_nInstances = 0;
};

class GLTextRenderData : public RenderData
{
public:
	enum Flags
	{
		UpdateVertex = 1 << 0,
		UpdateAllData = 1 << 1,
		UpdateElmData = 1 << 2
	};

	/*
		Type of render : Bitmap, SDF, Outline complie data in here and push it to the draw system
		Here, we determine the type of text rendering and call the corresponding render function.
	*/
public:
	GLTextRenderData();
	virtual ~GLTextRenderData();

	GLSDFTextRenderDataPtr m_pSDFRenderData;
	GLBitmapTextRenderDataPtr m_pBitmapRenderData;
};

