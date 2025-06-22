#include "graphics/rendering/shader/xglshader.h"
#include "graphics/rendering/OpenGL/glew.h"

#include <iostream>
#include <cstdarg>

#include <map>

__BEGIN_NAMESPACE__

#define MAX_LOG 1024

const char strStatus[][6] {
	"WARN",
	"ERR",
	"INFO",
};

std::map<int, std::string> mapShaderNames
{
	{GL_VERTEX_SHADER, "GL_VERTEX_SHADER"},
	{GL_FRAGMENT_SHADER, "GL_FRAGMENT_SHADER"},
	{GL_GEOMETRY_SHADER, "GL_GEOMETRY_SHADER"}
};

GLShaderLogger Logger("");

template<typename Ch>
typename std::enable_if_t<std::is_same_v<Ch, char>, FILE*>
OpenFile(const Ch* path)
{
	return _fsopen(path, "rb", _SH_DENYRD);
}

template<typename Ch>
typename std::enable_if_t<std::is_same_v<Ch, wchar_t>, FILE*>
OpenFile(const Ch* path)
{
	return _wfsopen(path, L"rb", _SH_DENYRD);
}

template<typename Ch>
static int ReadContentFile(const Ch* path, void** data)
{
	if (data) *data = NULL;
	long int nbytes = 0;

	FILE* file = OpenFile<Ch>(path);

	if (!file)
		return nbytes;

	fseek(file, 0L, SEEK_END);
	nbytes = static_cast<long int>(ftell(file));
	fseek(file, 0L, SEEK_SET);

	if (nbytes < 0)
	{
		fclose(file);
		return 0;
	}

	// 2 null value (char and wide char)
	nbytes = nbytes + 2;

	auto pData = new unsigned char[nbytes];
	memset(pData, 0, nbytes);
	nbytes = (long int)fread_s(pData, nbytes, sizeof(char), nbytes, file);
	fclose(file);

	if (data)
		*data = pData;
	else
		delete[] pData;

	return nbytes;
}


static std::string GetShaderLog(const GLuint nShader)
{
	std::string strErr = "";
	int nLen = 0;
	int chwt = 0;

	glGetShaderiv(nShader, GL_INFO_LOG_LENGTH, &nLen);
	if (nLen > 0)
	{
		char* pstrLog = new char[static_cast<size_t>(nLen) + 1]();
		glGetShaderInfoLog(nShader, nLen, &chwt, pstrLog);
		strErr = pstrLog;

		delete[] pstrLog;
	}
	return strErr;
}

static std::string GetProgramLog(const GLuint nProgram)
{
	std::string strErr = "";
	int nLen = 0;
	int chwt = 0;

	glGetProgramiv(nProgram, GL_INFO_LOG_LENGTH, &nLen);
	if (nLen > 0)
	{
		char* pstrLog = new char[static_cast<size_t>(nLen) + 1]();
		glGetProgramInfoLog(nProgram, nLen, &chwt, pstrLog);
		strErr = pstrLog;

		delete[] pstrLog;
	}
	return strErr;
}

static bool IsErrorShader(GLuint nShader)
{
	GLint isCompiled = 0;
	glGetShaderiv(nShader, GL_COMPILE_STATUS, &isCompiled);
	return (isCompiled == GL_FALSE);
}

static bool IsErrorProgram(GLint nProgram)
{
	GLint isLinked = 0;
	glGetProgramiv(nProgram, GL_LINK_STATUS, &isLinked);
	return (isLinked == GL_FALSE);
}

static GLuint CreateShader(GLenum type, const std::string& source)
{
	if (source.empty())
		return 0;

	GLuint nShader = 0; 

	const char* pFileData = nullptr;

	int nLength = ReadContentFile<char>(source.c_str(), (void**)&pFileData);

	if (nLength <= 0)
		Logger.WriteLog(ShaderLogStatus::ERR, "[System] Can't open file : %s", source.c_str());

	nShader = glCreateShader(type);
	glShaderSource(nShader, 1, &pFileData, NULL);
	glCompileShader(nShader);

	if (IsErrorShader(nShader))
	{
		std::string strErr = GetShaderLog(nShader);
		Logger.WriteLog(ShaderLogStatus::ERR, "~~~~~~~~~~~~~~~~~[SHADER-ERR(%s)]~~~~~~~~~~~~~~~~~\n", mapShaderNames[static_cast<int>(type)].c_str());
		Logger.WriteLog(ShaderLogStatus::ERR, strErr, true);
		Logger.WriteLog(ShaderLogStatus::ERR, "^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n");

		glDeleteShader(nShader);

		nShader = 0;
	}

	delete[] pFileData;

	return nShader;
}

static GLuint CreateProgram(std::vector<GLuint> vecShaders)
{
	GLuint nProgramID = glCreateProgram();

	for (GLuint shader : vecShaders)
	{
		glAttachShader(nProgramID, shader);
	}

	glLinkProgram(nProgramID);

	if (IsErrorProgram(nProgramID))
	{
		std::string strErr = GetProgramLog(nProgramID);

		Logger.WriteLog(ShaderLogStatus::ERR, "~~~~~~~~~~~~~~~~~~~~[PROGRAM-ERR]~~~~~~~~~~~~~~~~~~~~\n");
		Logger.WriteLog(ShaderLogStatus::ERR, strErr, true);
		Logger.WriteLog(ShaderLogStatus::ERR, "^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n");

		return 0;
	}

	return nProgramID;
}

GLShaderLogger::GLShaderLogger(const std::string& path)
{
	m_strLogPath = path;
}

void GLShaderLogger::WriteLog(ShaderLogStatus status, std::string& msg, bool bIngoreStatus/* = false*/)
{
	char msgLogData[MAX_LOG] = { 0 };

	if (!bIngoreStatus)
	{
		snprintf(&msgLogData[0], MAX_LOG, "[OPENGL][%s] : \n %s", strStatus[static_cast<int>(status)],
			msg.c_str());
	}
	else
	{
		snprintf(&msgLogData[0], MAX_LOG, "%s", msg.c_str());
	}

	std::cerr << msgLogData << std::endl;
}

void GLShaderLogger::WriteLog(ShaderLogStatus status, const char* format, ...)
{
	const int nBufferSize = MAX_LOG;

	std::string strBuffer;
	strBuffer.resize(nBufferSize);

	va_list args;
	va_start(args, format);
	vsnprintf(&strBuffer[0], nBufferSize, format, args);
	va_end(args);

	WriteLog(status, strBuffer);
}

GLShaderProgram::GLShaderProgram()
	:m_nProgramID(0)
{

}

GLShaderProgram::~GLShaderProgram()
{
	DeleteProgram();
}

bool GLShaderProgram::LoadShaders(const std::unordered_map<ShaderStage, std::string>& sources)
{
	std::vector<GLuint> vecShaders;

	// Load and create shader
	for (const auto& pair : sources)
	{
		ShaderStage stage = pair.first;
		const std::string& strPath = pair.second;

		GLenum glType;

		switch (stage)
		{
			case ShaderStage::Vertex:
				glType = GL_VERTEX_SHADER;
				break;
			case ShaderStage::Fragment:
				glType = GL_FRAGMENT_SHADER;
				break;
			case ShaderStage::Geometry:
				glType = GL_GEOMETRY_SHADER;
				break;
			default:
				continue;
		}

		GLuint shader = CreateShader(glType, strPath);

		if (shader)
			vecShaders.push_back(shader);
	}

	// Create program
	int nProgram = CreateProgram(vecShaders);

	// delete all shader bebause they don't used more.
	for (GLuint shader : vecShaders)
		glDeleteShader(shader);

	m_nProgramID = nProgram;

	return (m_nProgramID > 0);
}

bool GLShaderProgram::Use() const
{
	glUseProgram(m_nProgramID);

	return true;
}

void GLShaderProgram::UnUse() const
{
	glUseProgram(0);
}

unsigned int GLShaderProgram::GetProgramID() const
{
	return m_nProgramID;
}

void GLShaderProgram::DeleteProgram()
{
	if (m_nProgramID)
		glDeleteProgram(m_nProgramID);
}

GLShaderDataBinder::GLShaderDataBinder(unsigned int nProg)
	: m_nProgramID(nProg)
{
}

GLShaderDataBinder::~GLShaderDataBinder()
{
}

void GLShaderDataBinder::SetFloat(const std::string& name, float value)
{
	GLint loc = glGetUniformLocation(m_nProgramID, name.c_str());
	glUniform1f(loc, value);
}

void GLShaderDataBinder::SetInt(const std::string& name, int value)
{
	GLint loc = glGetUniformLocation(m_nProgramID, name.c_str());
	glUniform1i(loc, value);
}

void GLShaderDataBinder::SetVec2(const std::string& name, const float* vec2)
{
	GLint loc = glGetUniformLocation(m_nProgramID, name.c_str());
	glUniform2fv(loc, 1, vec2);
}

void GLShaderDataBinder::SetVec3(const std::string& name, const float* vec3)
{
	GLint loc = glGetUniformLocation(m_nProgramID, name.c_str());
	glUniform3fv(loc, 1, vec3);
}

void GLShaderDataBinder::SetMat4(const std::string& name, const float* mat4)
{
	GLint loc = glGetUniformLocation(m_nProgramID, name.c_str());
	glUniformMatrix4fv(loc, 1, GL_FALSE, mat4);
}

__END_NAMESPACE__


