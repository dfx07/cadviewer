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
#ifndef XPOLYRENDER_H
#define XPOLYRENDER_H

#include "graphics/rendering/xrenderable.h"
#include "graphics/rendering/xrendertype.h"

__BEGIN_NAMESPACE__

class PolyRender : public ObjectRenderable
{
public:
	PolyRender();
	~PolyRender();

protected:
	bool CreateShader();


public:
	virtual void Draw();

	virtual bool BindShader();

	virtual void UnbindShader();

	virtual void Update(float deltaTime);

protected:
	ShaderProgramPtr m_pProgram = nullptr;
	IShaderDataBinderPtr m_pBinder = nullptr;

public:
	std::vector<float> m_vecRenderData;
};


__END_NAMESPACE__

#endif // !XPOLYRENDER_H