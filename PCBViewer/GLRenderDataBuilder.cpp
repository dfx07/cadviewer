#include "GLRenderDataBuilder.h"

#include "PolyDrawObject.h"

#include "graphics/rendering/OpenGL/glew.h"


GLPolyRenderData::GLPolyRenderData()
{

}

GLPolyRenderData::~GLPolyRenderData()
{

}

bool GLPolyRenderData::CreateBuffers()
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

void GLPolyRenderData::UpdateVertexBuffer()
{
	if (m_nVao == 0 || m_nEbo == 0 || m_nVbo == 0)
	{
		ReleaseBuffer();
		if (CreateBuffers() == false)
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

void GLPolyRenderData::ReleaseBuffer()
{
	if (m_nVbo)
		glDeleteBuffers(1, &m_nVbo);

	if (m_nEbo)
		glDeleteBuffers(1, &m_nEbo);

	if (m_nVao)
		glDeleteVertexArrays(1, &m_nVao);
}

float GLRenderDataBuilder::NextZ()
{
	float z = m_fCurrentZ;
	m_fCurrentZ += m_fZStep;
	return z;
}

RenderDataPtr GLRenderDataBuilder::Make(const PolyDrawObjectList* model)
{
	GLPolyRenderDataPtr pData = std::make_shared<GLPolyRenderData>();

	for (auto& poly : model->m_vecPolys)
	{
		size_t nVertexCnt = poly->m_vecPoints.size();

		size_t nStartIndex = pData->m_vecRenderData.size();

		float z = NextZ();

		for (size_t i = 0; i < nVertexCnt; i++)
		{
			auto& vtxData = poly->m_vecPoints[i];

			pData->m_vecRenderData.push_back({ Vec3(vtxData.x, vtxData.y, z), poly->m_clColor, poly->m_fThickness, poly->GetObjectID()});

			size_t prev = nStartIndex + (i + nVertexCnt - 1) % nVertexCnt;
			size_t cur1 = nStartIndex + i;
			size_t cur2 = nStartIndex + (i + 1) % nVertexCnt;
			size_t next = nStartIndex + (i + 2) % nVertexCnt;

			pData->m_vecIndices.push_back(static_cast<unsigned int>(prev));
			pData->m_vecIndices.push_back(static_cast<unsigned int>(cur1));
			pData->m_vecIndices.push_back(static_cast<unsigned int>(cur2));
			pData->m_vecIndices.push_back(static_cast<unsigned int>(next));
		}
	}

	pData->CreateBuffers();
	pData->UpdateVertexBuffer();

	return pData;
}

