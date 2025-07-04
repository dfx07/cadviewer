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

__BEGIN_NAMESPACE__

class TFXObjectRenderable
{
public:
	TFXObjectRenderable(const Mat4& modelMatrix = Mat4(1.0f))
		: m_matModel(modelMatrix), m_nModelID(0)
	{ }

	virtual ~TFXObjectRenderable() { }

	void SetModelMatrix(const Mat4& modelMatrix)
	{
		m_matModel = modelMatrix;
	}

	const Mat4& GetModelMatrix() const
	{
		return m_matModel;
	}

public:
	//virtual TFXRenderDataPtr Make(TFXRenderDataBuilderPtr builder) const = 0;
	virtual void Update(float deltaTime) {}

public:
	int m_nModelID = 0;
	Mat4 m_matModel;
};

__END_NAMESPACE__

#endif // !XRENDERABLE_H