#include "GLRenderData.h"
#include "graphics/rendering/OpenGL/glew.h"

GLPolyRenderData::GLPolyRenderData()
{
}

GLPolyRenderData::~GLPolyRenderData()
{
	ReleaseBuffer();
}

void GLPolyRenderData::UpdateVertexBuffer()
{

}

void GLPolyRenderData::ReleaseBuffer()
{
	if (m_nVao != 0)
	{
		glDeleteVertexArrays(1, &m_nVao);
		m_nVao = 0;
	}

	if (m_nVbo != 0)
	{
		glDeleteBuffers(1, &m_nVbo);
		m_nVbo = 0;
	}

	if (m_nEbo != 0)
	{
		glDeleteBuffers(1, &m_nEbo);
		m_nEbo = 0;
	}
}

bool GLPolyRenderData::Create()
{
	ReleaseBuffer();

	glGenVertexArrays(1, &m_nVao);
	glBindVertexArray(m_nVao);

	glGenBuffers(1, &m_nVbo);
	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	glBufferData(GL_ARRAY_BUFFER, m_vecRenderData.size() * sizeof(PolyVertexData), m_vecRenderData.data(), GL_STATIC_DRAW);

	glGenBuffers(1, &m_nEbo);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEbo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, m_vecIndices.size() * sizeof(GLuint), m_vecIndices.data(), GL_STATIC_DRAW);

	// Layout
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(PolyVertexData), (void*)offsetof(PolyVertexData, position));
	glEnableVertexAttribArray(0);

	glVertexAttribPointer(1, 4, GL_FLOAT, GL_FALSE, sizeof(PolyVertexData), (void*)offsetof(PolyVertexData, color));
	glEnableVertexAttribArray(1);

	glVertexAttribPointer(2, 1, GL_FLOAT, GL_FALSE, sizeof(PolyVertexData), (void*)offsetof(PolyVertexData, thickness));
	glEnableVertexAttribArray(2);

	glVertexAttribPointer(3, 1, GL_INT, GL_FALSE, sizeof(PolyVertexData), (void*)offsetof(PolyVertexData, polygonID));
	glEnableVertexAttribArray(3);

	glBindVertexArray(0);

	return true;
}

void GLPolyRenderData::Update()
{
	if (m_nVao == 0 || m_nEbo == 0 || m_nVbo == 0)
	{
		if (Create() == false)
			return;
	}

	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	if (m_nUpdateFlags & Flags::UpdateVertex)
	{
		// Dung lượng thay đổi → cấp phát lại
		glBufferData(GL_ARRAY_BUFFER, m_vecRenderData.size() * sizeof(PolyVertexData), m_vecRenderData.data(), GL_DYNAMIC_DRAW);
	}
	else
	{
		// Kích thước không đổi → update nhanh
		glBufferSubData(GL_ARRAY_BUFFER, 0, m_vecRenderData.size() * sizeof(PolyVertexData), m_vecRenderData.data());
	}

	glBindVertexArray(m_nVao);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEbo);

	if (m_nUpdateFlags & Flags::UpdateIndex)
	{
		// Dung lượng thay đổi → cấp phát lại
		glBufferData(GL_ELEMENT_ARRAY_BUFFER, m_vecIndices.size() * sizeof(GLuint), m_vecIndices.data(), GL_DYNAMIC_DRAW);
	}
	else
	{
		// Kích thước không đổi → update nhanh
		glBufferSubData(GL_ELEMENT_ARRAY_BUFFER, 0, m_vecIndices.size() * sizeof(GLuint), m_vecIndices.data());
	}

	glBindVertexArray(0);
}

void GLPolyRenderData::Release()
{
	ReleaseBuffer();
	m_vecRenderData.clear();
	m_vecIndices.clear();
	m_nUpdateFlags = 0;
}

