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
	Release();
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


static float quadVertices[8]
{
	-1.0f, -1.0f, // Bottom-left
	 1.0f, -1.0f, // Bottom-right
	-1.0f,  1.0f, // Top-left
	 1.0f,  1.0f  // Top-right
};

static unsigned int quadIndices[6]
{
	0, 1, 2,
	2, 1, 3
};

GLCircleRenderData::GLCircleRenderData()
{
}

GLCircleRenderData::~GLCircleRenderData()
{
	Release();
}

bool GLCircleRenderData::CreateBorderRender()
{
	glGenVertexArrays(1, &m_nBorderVao);
	glBindVertexArray(m_nBorderVao);

	// Create draw buffer is quad = (2 triangle)

	if (m_nIncVbo == 0)
	{
		glGenBuffers(1, &m_nIncVbo);
		glBindBuffer(GL_ARRAY_BUFFER, m_nIncVbo);
		glBufferData(GL_ARRAY_BUFFER, sizeof(quadVertices), quadVertices, GL_STATIC_DRAW);

		glEnableVertexAttribArray(0);
		glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);
		glVertexAttribDivisor(0, 0);

		glGenBuffers(1, &m_nEbo);
		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEbo);
		glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(quadIndices), quadIndices, GL_STATIC_DRAW);
	}
	else
	{
		glBindBuffer(GL_ARRAY_BUFFER, m_nIncVbo);
		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEbo);
	}

	// Create intances buffer
	glGenBuffers(1, &m_nBorderVbo);
	glBindBuffer(GL_ARRAY_BUFFER, m_nBorderVbo);
	glBufferData(GL_ARRAY_BUFFER, m_vecBorderRenderData.size() * sizeof(CircleBorderVertexData),
		m_vecBorderRenderData.data(), GL_STATIC_DRAW);

	// Layout
	glEnableVertexAttribArray(1);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(CircleBorderVertexData), (void*)offsetof(CircleBorderVertexData, center));
	glVertexAttribDivisor(1, 1);

	glEnableVertexAttribArray(2);
	glVertexAttribPointer(2, 1, GL_FLOAT, GL_FALSE, sizeof(CircleBorderVertexData), (void*)offsetof(CircleBorderVertexData, radius));
	glVertexAttribDivisor(2, 1);

	glEnableVertexAttribArray(3);
	glVertexAttribPointer(3, 1, GL_FLOAT, GL_FALSE, sizeof(CircleBorderVertexData), (void*)offsetof(CircleBorderVertexData, thickness));
	glVertexAttribDivisor(3, 1);

	glEnableVertexAttribArray(4);
	glVertexAttribPointer(4, 4, GL_FLOAT, GL_FALSE, sizeof(CircleBorderVertexData), (void*)offsetof(CircleBorderVertexData, color));
	glVertexAttribDivisor(4, 1);

	glBindVertexArray(0);

	return true;
}

bool GLCircleRenderData::CreateFillRender()
{
	glGenVertexArrays(1, &m_nVao);
	glBindVertexArray(m_nVao);

	// Create draw buffer is quad = (2 triangle)
	glGenBuffers(1, &m_nIncVbo);
	glBindBuffer(GL_ARRAY_BUFFER, m_nIncVbo);
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
	glBufferData(GL_ARRAY_BUFFER, m_vecFillRenderData.size() * sizeof(CircleFillVertexData), m_vecFillRenderData.data(), GL_STATIC_DRAW);

	// Layout
	glEnableVertexAttribArray(1);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(CircleFillVertexData), (void*)offsetof(CircleFillVertexData, center));
	glVertexAttribDivisor(1, 1);

	glEnableVertexAttribArray(2);
	glVertexAttribPointer(2, 1, GL_FLOAT, GL_FALSE, sizeof(CircleFillVertexData), (void*)offsetof(CircleFillVertexData, radius));
	glVertexAttribDivisor(2, 1);

	glEnableVertexAttribArray(3);
	glVertexAttribPointer(3, 4, GL_FLOAT, GL_FALSE, sizeof(CircleFillVertexData), (void*)offsetof(CircleFillVertexData, color));
	glVertexAttribDivisor(3, 1);

	glBindVertexArray(0);

	return true;
}

void GLCircleRenderData::ReleaseBorderRender()
{
	if (m_nBorderVao != 0)
	{
		glDeleteVertexArrays(1, &m_nBorderVao);
		m_nBorderVao = 0;
	}
	if (m_nBorderVbo != 0)
	{
		glDeleteBuffers(1, &m_nBorderVbo);
		m_nBorderVbo = 0;
	}
}

void GLCircleRenderData::ReleaseFillRender()
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
	if (m_nIncVbo != 0)
	{
		glDeleteBuffers(1, &m_nIncVbo);
		m_nIncVbo = 0;
	}
}

void GLCircleRenderData::RemoveData()
{
	m_vecFillRenderData.clear();
	m_vecBorderRenderData.clear();
}

bool GLCircleRenderData::Create()
{
	if (m_nVao != 0 || m_nVbo != 0 || m_nEbo != 0)
	{
		Release();
	}

	if (CreateBorderRender() == false)
		return false;

	if (CreateFillRender() == false)
		return false;

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
		glBufferData(GL_ARRAY_BUFFER, m_vecFillRenderData.size() * sizeof(CircleFillVertexData), m_vecFillRenderData.data(), GL_DYNAMIC_DRAW);
	}
	else
	{
		// Kích thước không đổi → update nhanh
		glBufferSubData(GL_ARRAY_BUFFER, 0, m_vecFillRenderData.size() * sizeof(CircleFillVertexData), m_vecFillRenderData.data());
	}
}

void GLCircleRenderData::Release()
{
	ReleaseBorderRender();
	ReleaseFillRender();

	m_nUpdateFlags = 0;
}


float RectVertices[] = {
	-0.5f, -0.5f, // Bottom-left
	 0.5f, -0.5f, // Bottom-right
	-0.5f,  0.5f, // Top-left
	 0.5f,  0.5f  // Top-right
};

static unsigned int RectIndices[6]
{
	0, 1, 2,
	2, 1, 3
};

GLRectRenderData::GLRectRenderData()
{
}

GLRectRenderData::~GLRectRenderData()
{
	Release();
}

float GLRectRenderData::s_quadVertices[]
{
	-1.0f, -1.0f, // Bottom-left
	 1.0f, -1.0f, // Bottom-right
	-1.0f,  1.0f, // Top-left
	 1.0f,  1.0f  // Top-right
};

unsigned int GLRectRenderData::s_quadIndices[]
{
	0, 1, 2,
	2, 1, 3
};

bool GLRectRenderData::CreateBorderRender()
{
	glGenVertexArrays(1, &m_nBorderVao);
	glBindVertexArray(m_nBorderVao);

	glGenBuffers(1, &m_nBorderVbo);
	glBindBuffer(GL_ARRAY_BUFFER, m_nBorderVbo);
	glBufferData(GL_ARRAY_BUFFER, m_vecBorderRenderData.size() * sizeof(RectBorderVertexData),
		m_vecBorderRenderData.data(), GL_STATIC_DRAW);

	glGenBuffers(1, &m_nBorderEbo);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nBorderEbo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, m_vecBorderIndices.size() * sizeof(GLuint),
		m_vecBorderIndices.data(), GL_STATIC_DRAW);

	// Layout
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(RectBorderVertexData), (void*)offsetof(RectBorderVertexData, position));
	glEnableVertexAttribArray(0);

	glVertexAttribPointer(1, 4, GL_FLOAT, GL_FALSE, sizeof(RectBorderVertexData), (void*)offsetof(RectBorderVertexData, color));
	glEnableVertexAttribArray(1);

	glVertexAttribPointer(2, 1, GL_FLOAT, GL_FALSE, sizeof(RectBorderVertexData), (void*)offsetof(RectBorderVertexData, thickness));
	glEnableVertexAttribArray(2);

	glVertexAttribPointer(3, 1, GL_INT, GL_FALSE, sizeof(RectBorderVertexData), (void*)offsetof(RectBorderVertexData, rectID));
	glEnableVertexAttribArray(3);

	glBindVertexArray(0);

	return true;
}

bool GLRectRenderData::CreateFillRender()
{
	glGenVertexArrays(1, &m_nVao);
	glBindVertexArray(m_nVao);

	glGenBuffers(1, &m_nVbo);
	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	glBufferData(GL_ARRAY_BUFFER, sizeof(RectVertices), RectVertices, GL_STATIC_DRAW);

	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);
	glVertexAttribDivisor(0, 0);

	glGenBuffers(1, &m_nEbo);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEbo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(RectIndices), RectIndices, GL_STATIC_DRAW);

	// Create intances buffer
	glGenBuffers(1, &m_nVboInst);
	glBindBuffer(GL_ARRAY_BUFFER, m_nVboInst);
	glBufferData(GL_ARRAY_BUFFER, m_vecFillRenderData.size() * sizeof(RectFillVertexData),
		m_vecFillRenderData.data(), GL_STATIC_DRAW);

	// Layout
	glEnableVertexAttribArray(1);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(RectFillVertexData), (void*)offsetof(RectFillVertexData, position));
	glVertexAttribDivisor(1, 1);

	glEnableVertexAttribArray(2);
	glVertexAttribPointer(2, 1, GL_FLOAT, GL_FALSE, sizeof(RectFillVertexData), (void*)offsetof(RectFillVertexData, angle));
	glVertexAttribDivisor(2, 1);

	glEnableVertexAttribArray(3);
	glVertexAttribPointer(3, 2, GL_FLOAT, GL_FALSE, sizeof(RectFillVertexData), (void*)offsetof(RectFillVertexData, size));
	glVertexAttribDivisor(3, 1);

	glEnableVertexAttribArray(4);
	glVertexAttribPointer(4, 4, GL_FLOAT, GL_FALSE, sizeof(RectFillVertexData), (void*)offsetof(RectFillVertexData, color));
	glVertexAttribDivisor(4, 1);

	glBindVertexArray(0);

	return true;
}

bool GLRectRenderData::CreateBuffersAndVAO()
{
	glGenVertexArrays(1, &m_nVao);

	glGenBuffers(1, &m_nVbo);
	glGenBuffers(1, &m_nVboInst);
	glGenBuffers(1, &m_nEboInst);

	if (m_nVao == 0 || m_nVbo == 0 || m_nVboInst == 0 || m_nEboInst == 0)
		return false;

	GLenum err = glGetError();
	if (err != GL_NO_ERROR)
		return false;

	return true;
}

bool GLRectRenderData::BuildRenderData()
{
	glBindVertexArray(m_nVao);

	glBindBuffer(GL_ARRAY_BUFFER, m_nVboInst);
	glBufferData(GL_ARRAY_BUFFER, sizeof(s_quadVertices), s_quadVertices, GL_STATIC_DRAW);

	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);
	glVertexAttribDivisor(0, 0);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, m_nEboInst);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(s_quadIndices), s_quadIndices, GL_STATIC_DRAW);

	// Create intances buffer
	glBindBuffer(GL_ARRAY_BUFFER, m_nVbo);
	{
		glBufferData(GL_ARRAY_BUFFER, m_vecRenderData.size() * sizeof(RectVertexData), m_vecRenderData.data(), GL_STATIC_DRAW);

		// Layout
		glEnableVertexAttribArray(1);
		glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(RectVertexData), (void*)offsetof(RectVertexData, position));
		glVertexAttribDivisor(1, 1);

		glEnableVertexAttribArray(2);
		glVertexAttribPointer(2, 1, GL_FLOAT, GL_FALSE, sizeof(RectVertexData), (void*)offsetof(RectVertexData, angle));
		glVertexAttribDivisor(2, 1);

		glEnableVertexAttribArray(3);
		glVertexAttribPointer(3, 2, GL_FLOAT, GL_FALSE, sizeof(RectVertexData), (void*)offsetof(RectVertexData, size));
		glVertexAttribDivisor(3, 1);

		glEnableVertexAttribArray(4);
		glVertexAttribPointer(4, 1, GL_FLOAT, GL_FALSE, sizeof(RectVertexData), (void*)offsetof(RectVertexData, thickness));
		glVertexAttribDivisor(4, 1);

		glEnableVertexAttribArray(5);
		glVertexAttribPointer(5, 4, GL_FLOAT, GL_FALSE, sizeof(RectVertexData), (void*)offsetof(RectVertexData, thickness_color));
		glVertexAttribDivisor(5, 1);

		glEnableVertexAttribArray(6);
		glVertexAttribPointer(6, 4, GL_FLOAT, GL_FALSE, sizeof(RectVertexData), (void*)offsetof(RectVertexData, fill_color));
		glVertexAttribDivisor(6, 1);
	}

	glBindVertexArray(0);

	return true;
}

void GLRectRenderData::ReleaseBorderRender()
{
	if (m_nBorderVao != 0)
	{
		glDeleteVertexArrays(1, &m_nBorderVao);
		m_nBorderVao = 0;
	}
	if (m_nBorderVbo != 0)
	{
		glDeleteBuffers(1, &m_nBorderVbo);
		m_nBorderVbo = 0;
	}
	if (m_nBorderEbo != 0)
	{
		glDeleteBuffers(1, &m_nBorderEbo);
		m_nBorderEbo = 0;
	}
}

void GLRectRenderData::ReleaseFillRender()
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
	if (m_nVboInst != 0)
	{
		glDeleteBuffers(1, &m_nVboInst);
		m_nVboInst = 0;
	}
}

void GLRectRenderData::RemoveData()
{
	m_vecFillRenderData.clear();
	m_vecBorderRenderData.clear();

	m_vecBorderIndices.clear();
}

bool GLRectRenderData::Create()
{
	if (m_nVao != 0 || m_nVbo != 0 || m_nEbo != 0)
	{
		Release();
	}

	if (!CreateBuffersAndVAO())
		return false;

	if (!BuildRenderData())
		return false;

	return true;
}

void GLRectRenderData::Update()
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
		glBufferData(GL_ARRAY_BUFFER, m_vecFillRenderData.size() * sizeof(RectFillVertexData),
			m_vecFillRenderData.data(), GL_DYNAMIC_DRAW);
	}
	else
	{
		// Kích thước không đổi → update nhanh
		glBufferSubData(GL_ARRAY_BUFFER, 0, m_vecFillRenderData.size() * sizeof(RectFillVertexData),
			m_vecFillRenderData.data());
	}
}

void GLRectRenderData::Release()
{
	ReleaseFillRender();
	ReleaseBorderRender();

	RemoveData();

	m_nUpdateFlags = 0;
}
