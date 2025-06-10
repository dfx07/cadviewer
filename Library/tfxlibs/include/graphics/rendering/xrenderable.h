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

#include "common/tfxtype.h"

__BEGIN_NAMESPACE__

class ObjectRenderable
{
public:
	ObjectRenderable(const Vec4& modelMatrix = Vec4(1.0f))
		: m_matModelMatrix(modelMatrix)
	{

	}

	virtual ~ObjectRenderable()
	{

	}

	virtual void Draw() = 0;

	void SetModelMatrix(const Vec4& modelMatrix)
	{
		m_matModelMatrix = modelMatrix;
	}

	const Vec4& GetModelMatrix() const
	{
		return m_matModelMatrix;
	}

protected:
	Vec4 m_matModelMatrix;
};

__END_NAMESPACE__

#endif // !XCTX_H