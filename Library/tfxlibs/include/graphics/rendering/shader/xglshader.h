////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Define opengl shader
* @file  : xglshader.h
* @create: June 18, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XGLSHADER_H
#define XGLSHADER_H

#include "common/tfx_def.h"
#include "xshader.h"
#include <string>


class GLShaderLogger : public IShaderLogger
{
public:
	GLShaderLogger(const std::string& path = "");

public:
	virtual void WriteLog(ShaderLogStatus status, std::string& msg, bool bIngoreStatus = false);
	virtual void WriteLog(ShaderLogStatus status, const char* format, ...);

public:
	std::string m_strLogPath = "";
};


/******************************************************************************
* Class : GLShaderProgram
******************************************************************************/
class GLShaderProgram : public IShaderProgram
{
public:
	GLShaderProgram();

	virtual ~GLShaderProgram();

	virtual bool LoadShaders(const std::unordered_map<ShaderStage, std::string>& sources);
	virtual bool Use() const;
	virtual void UnUse() const;

public:
	unsigned int GetProgramID() const;

private:
	virtual void DeleteProgram();

private:
	unsigned int m_nProgramID;
};

/******************************************************************************
* Class : GLShaderDataBinder
******************************************************************************/
class GLShaderDataBinder : public IShaderDataBinder
{
public:
	GLShaderDataBinder(unsigned int nProg);
	~GLShaderDataBinder();

public:
	virtual void SetFloat(const std::string& name, float value);
	virtual void SetInt(const std::string& name, int value);
	virtual void SetVec2(const std::string& name, const float* vec2);
	virtual void SetVec3(const std::string& name, const float* vec3);
	virtual void SetMat4(const std::string& name, const float* mat4);

protected:
	unsigned int m_nProgramID;
};


#endif // !XGLSHADER_H