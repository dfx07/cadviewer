#include "FreeTypeFont.h"
#include <mutex>

class FreeTypeFontSystem
{
private:
	FreeTypeFontSystem()
	{
		FT_Error err = FT_Init_FreeType(&m_ftLib);
		if (err != 0)
		{
			assert(0);
			m_bInit = false;
			return;
		}

		m_bInit = true;
	}

	~FreeTypeFontSystem()
	{
		if (m_ftLib)
		{
			FT_Done_FreeType(m_ftLib);
			m_ftLib = nullptr;
		}
	}

private:
	FreeTypeFontSystem& operator=(const FreeTypeFontSystem&) = delete;
	FreeTypeFontSystem(const FreeTypeFontSystem&) = delete;

public:
	bool IsInit() const
	{
		return m_ftLib != nullptr;
	}

protected:
	FT_Library m_ftLib;
	bool m_bInit = false;

public:

	static FT_Library GetIntance()
	{
		static FreeTypeFontSystem ftLib;
		return ftLib.m_ftLib;
	}
};

#define FTSytem FreeTypeFontSystem::GetIntance()


//==================================================================================
// @_@

FreeTypeFont::FreeTypeFont() :
	m_face(nullptr)
{
	assert(FTSytem);
}

FreeTypeFont::~FreeTypeFont()
{
	Unload();
}

bool FreeTypeFont::Load(const char* font_path)
{
	FT_Face face;
	if (FT_New_Face(FTSytem, font_path, 0, &face)) {
		
		return false;
	}

	m_face = face;

	return true;
}

void FreeTypeFont::Unload()
{
	if(m_face)
		FT_Done_Face(m_face);
}

FT_Face FreeTypeFont::GetHandle() const
{
	return m_face;
}
