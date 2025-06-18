﻿#include "graphics/rendering/xopenglrenderer.h"
#include "graphics/camera/xcamera.h"

__BEGIN_NAMESPACE__

OpenGLRenderer::OpenGLRenderer()
{

}

OpenGLRenderer::OpenGLRenderer(CameraPtr pCamera):
	TFXRenderer(pCamera)
{
	m_v4ClearColor = Vec4(1.0f, 1.0f, 1.0f, 1.0f); // Default clear color is white
}

OpenGLRenderer::~OpenGLRenderer()
{
	// Cleanup if necessary
}

void OpenGLRenderer::SetViewPort(const int x, const int y, const int width, const int height)
{
	glViewport(x, y, width, height);
}

void OpenGLRenderer::SetClearColor(const float r, const float g, const float b, const float a)
{
	m_v4ClearColor = Vec4(r, g, b, a);
}

void OpenGLRenderer::Init()
{

}

void OpenGLRenderer::Render()
{
	if (!m_pContext->IsValid())
		return;

	if (!m_pContext->MakeCurrentContext())
		return;

	glClearColor(m_v4ClearColor.r, m_v4ClearColor.g, m_v4ClearColor.b, m_v4ClearColor.a);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glEnable(GL_BLEND);
	glEnable(GL_DEPTH_TEST);

	Mat4& viewMat = m_pCamera->GetViewMatrix();
	Mat4& projMat = m_pCamera->GetProjMatrix();

	glMatrixMode(GL_PROJECTION);
	glLoadMatrixf(tfx::ValuePtr(projMat));

	glMatrixMode(GL_MODELVIEW);
	glLoadMatrixf(tfx::ValuePtr(viewMat));

	for (auto& pObjRenderable : m_ObjectRenders)
	{
		pObjRenderable->Draw();
	}

	glPointSize(10.0);
	glColor3f(0, 1, 0);
		glBegin(GL_POINTS);
		glVertex2f(0.f, 0.f);
	glEnd();

	glBegin(GL_LINES);
		glVertex2f(-200.5f, -100.5f);
		glVertex2f(100.5f, 100.5f);
	glEnd();

	glColor3f(0.5, 1, 1);
		glBegin(GL_POINTS);
		glVertex2f(0.f, 100.f);
	glEnd();

	m_pContext->SwapBuffer();
}

void OpenGLRenderer::Update(float deltaTime)
{
}



__END_NAMESPACE__


