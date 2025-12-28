#include "FreeTypeFont.h"
#include <mutex>
#include "core/tfx_utils.h"

//==================================================================================
// @_@

FreeTypeFont::FreeTypeFont() :
	m_face(nullptr)
{
	m_strGUID = NSP::CreateGUID();
}

FreeTypeFont::~FreeTypeFont()
{

}

std::string FreeTypeFont::GetGUID()
{
	return m_strGUID;
}

void FreeTypeFont::Release()
{
	if(m_face)
		FT_Done_Face(m_face);
}

FT_Face FreeTypeFont::GetHandle() const
{
	return m_face;
}

void FreeTypeFont::SetHandle(FT_Face face)
{
	m_face = face;
}
