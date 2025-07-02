////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Interface define object rendering
* @file  : xrenderable.h
* @create: June 10, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XRENDERABLE_H
#define XRENDERABLE_H

#include <memory>
#include "common/tfxtype.h"
#include "graphics/rendering/xrendertype.h"

__BEGIN_NAMESPACE__

class ObjectRenderable
{
public:
	ObjectRenderable(const Mat4& modelMatrix = Mat4(1.0f))
		: m_matModel(modelMatrix), m_nModelID(0)
	{ }

	virtual ~ObjectRenderable() { }

	void SetModelMatrix(const Mat4& modelMatrix)
	{
		m_matModel = modelMatrix;
	}

	const Mat4& GetModelMatrix() const
	{
		return m_matModel;
	}

public:
	virtual void Draw(const Mat4& view, const Mat4& proj, const Vec2& viewport, const float& zoom = 1.f) = 0;
	//virtual bool BindShader() = 0;
	//virtual void UnbindShader() = 0;
	virtual void Remake() = 0;

	// Optional
	virtual void Update(float deltaTime) {}

public:
	int m_nModelID = 0;
	Mat4 m_matModel;
	ShaderProgramPtr m_pProgram = nullptr;
	IShaderDataBinderPtr m_pBinder = nullptr;
};

__END_NAMESPACE__

#endif // !XRENDERABLE_H