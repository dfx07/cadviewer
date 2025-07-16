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


GLLineRenderData::GLLineRenderData()
{
}

GLLineRenderData::~GLLineRenderData()
{
}

bool GLLineRenderData::Create()
{
	glGenVertexArrays(1, &m_nVao);
	glBindVertexArray(m_nVao);

	glGenBuffers(1, &m_nVbo);
	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	glBufferData(GL_ARRAY_BUFFER, m_vecRenderData.size() * sizeof(LineVertexData), m_vecRenderData.data(), GL_STATIC_DRAW);

	// Layout
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(LineVertexData), (void*)offsetof(LineVertexData, position));
	glEnableVertexAttribArray(0);

	glVertexAttribPointer(1, 4, GL_FLOAT, GL_FALSE, sizeof(LineVertexData), (void*)offsetof(LineVertexData, color));
	glEnableVertexAttribArray(1);

	glVertexAttribPointer(2, 1, GL_FLOAT, GL_FALSE, sizeof(LineVertexData), (void*)offsetof(LineVertexData, thickness));
	glEnableVertexAttribArray(2);

	glBindVertexArray(0);

	return true;
}

void GLLineRenderData::Update()
{
	if (m_nVao == 0 || m_nVbo == 0)
	{
		if (Create() == false)
			return;
	}

	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	if (m_nUpdateFlags & Flags::UpdateVertex)
	{
		// Dung lượng thay đổi → cấp phát lại
		glBufferData(GL_ARRAY_BUFFER, m_vecRenderData.size() * sizeof(LineVertexData), m_vecRenderData.data(), GL_DYNAMIC_DRAW);
	}
	else
	{
		// Kích thước không đổi → update nhanh
		glBufferSubData(GL_ARRAY_BUFFER, 0, m_vecRenderData.size() * sizeof(LineVertexData), m_vecRenderData.data());
	}
}

void GLLineRenderData::Release()
{
	m_vecRenderData.clear();
	m_nUpdateFlags = 0;
}


GLCircleRenderData::GLCircleRenderData()
{
}

GLCircleRenderData::~GLCircleRenderData()
{
}

float quadVertices[8]
{
	-1.0f, -1.0f, // Bottom-left
	 1.0f, -1.0f, // Bottom-right
	-1.0f,  1.0f, // Top-left
	 1.0f,  1.0f  // Top-right
};

unsigned int quadIndices[6]
{
	0, 1, 2,
	2, 1, 3
};


float pointVertex[] = {
	0.0f, 0.0f
};

// Dùng chỉ số EBO (chỉ 1 đỉnh)
unsigned int indices[] = {
	0
};


glm::vec2 instanceOffsets[] = {
	{0.f, 0.f},
	{ 0.1f, -0.1f},
	{-0.1f,  0.1f},
	{ 0.1f,  0.1f}
};

bool GLCircleRenderData::Create()
{
	glGenVertexArrays(1, &m_nVao);
	glBindVertexArray(m_nVao);

	// Create draw buffer is quad = (2 triangle)

	glGenBuffers(1, &m_nQuadVbo);
	glBindBuffer(GL_ARRAY_BUFFER, m_nQuadVbo);
	glBufferData(GL_ARRAY_BUFFER, sizeof(quadVertices), quadVertices, GL_STATIC_DRAW);

	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);
	glVertexAttribDivisor(0, 0);

	glGenBuffers(1, &m_nEbo);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEbo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(quadIndices), quadIndices, GL_STATIC_DRAW);

	// Create intances buffer
	glGenBuffers(1, &m_nVbo);
	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	glBufferData(GL_ARRAY_BUFFER, m_vecRenderData.size() * sizeof(CircleVertexData), m_vecRenderData.data(), GL_STATIC_DRAW);

	// Layout
	glEnableVertexAttribArray(1);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(CircleVertexData), (void*)offsetof(CircleVertexData, center));
	glVertexAttribDivisor(1, 1);

	glEnableVertexAttribArray(2);
	glVertexAttribPointer(2, 1, GL_FLOAT, GL_FALSE, sizeof(CircleVertexData), (void*)offsetof(CircleVertexData, radius));
	glVertexAttribDivisor(2, 1);

	glEnableVertexAttribArray(3);
	glVertexAttribPointer(3, 1, GL_FLOAT, GL_FALSE, sizeof(CircleVertexData), (void*)offsetof(CircleVertexData, thickness));
	glVertexAttribDivisor(3, 1);

	glEnableVertexAttribArray(4);
	glVertexAttribPointer(4, 4, GL_FLOAT, GL_FALSE, sizeof(CircleVertexData), (void*)offsetof(CircleVertexData, thickness_color));
	glVertexAttribDivisor(4, 1);

	glEnableVertexAttribArray(5);
	glVertexAttribPointer(5, 4, GL_FLOAT, GL_FALSE, sizeof(CircleVertexData), (void*)offsetof(CircleVertexData, fill_color));
	glVertexAttribDivisor(5, 1);

	glBindVertexArray(0);


	//glGenVertexArrays(1, &m_nVao);
	//glBindVertexArray(m_nVao);

	//// Vertex buffer (1 đỉnh)
	//glGenBuffers(1, &m_nVbo);
	//glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	//glBufferData(GL_ARRAY_BUFFER, sizeof(pointVertex), pointVertex, GL_STATIC_DRAW);
	//glEnableVertexAttribArray(0); // layout 0
	//glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);

	//// Element buffer
	//glGenBuffers(1, &m_nEbo);
	//glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEbo);
	//glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

	//// Instance buffer
	//glGenBuffers(1, &m_nQuadVbo);
	//glBindBuffer(GL_ARRAY_BUFFER, m_nQuadVbo);
	//glBufferData(GL_ARRAY_BUFFER, sizeof(instanceOffsets), instanceOffsets, GL_STATIC_DRAW);
	//glEnableVertexAttribArray(1); // layout 1
	//glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);
	//glVertexAttribDivisor(1, 1); // Mỗi instance dùng 1 offset

	//glBindVertexArray(0);

	return true;
}

void GLCircleRenderData::Update()
{
	if (m_nVao == 0 || m_nVbo == 0)
	{
		if (Create() == false)
			return;
	}

	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	if (m_nUpdateFlags & Flags::UpdateVertex)
	{
		// Dung lượng thay đổi → cấp phát lại
		glBufferData(GL_ARRAY_BUFFER, m_vecRenderData.size() * sizeof(CircleVertexData), m_vecRenderData.data(), GL_DYNAMIC_DRAW);
	}
	else
	{
		// Kích thước không đổi → update nhanh
		glBufferSubData(GL_ARRAY_BUFFER, 0, m_vecRenderData.size() * sizeof(CircleVertexData), m_vecRenderData.data());
	}
}

void GLCircleRenderData::Release()
{
	m_vecRenderData.clear();
	m_nUpdateFlags = 0;
}