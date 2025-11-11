////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Define shader
* @file  : xshader.h
* @create: June 18, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XSHADER_H
#define XSHADER_H

#include <unordered_map>
#include <string>
#include "core/macros.h"

enum class ShaderStage {
	Vertex,
	Fragment,
	Geometry,
	Compute
};

enum class ShaderLogStatus
{
	WARN = 0,
	ERR  = 1,
	INFO = 2,
};

_interface IShaderLogger
{
public:
	virtual void WriteLog(ShaderLogStatus status, std::string& msg, bool bIngoreStatus = false) = 0;
};

_interface IShaderProgram
{
public:
	virtual ~IShaderProgram() = default;

	virtual bool LoadShaders(const std::unordered_map<ShaderStage, std::string>&sources) = 0;
	virtual bool Use() const = 0;
	virtual void UnUse() const = 0;
};

_interface IShaderDataBinder
{
public:
	virtual ~IShaderDataBinder() = default;

	virtual void SetFloat(const std::string& name, float value) = 0;
	virtual void SetInt(const std::string& name, int value) = 0;
	virtual void SetVec2(const std::string& name, const float* vec2) = 0;
	virtual void SetVec3(const std::string& name, const float* vec3) = 0;
	virtual void SetMat4(const std::string& name, const float* mat4) = 0;
};

#endif // !XSHADER_H