#include "FontLoader.h"

#include "FreeTypeFont.h"
#include <cassert>

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

#define FreeTypeSytem FreeTypeFontSystem::GetIntance()

FreeTypeFontLoader::FreeTypeFontLoader()
{
	assert(FreeTypeSytem);
}

FreeTypeFontLoader::~FreeTypeFontLoader()
{

}

std::unique_ptr<IFont> FreeTypeFontLoader::Load(const wchar_t* path)
{
	std::unique_ptr<FreeTypeFont> pFTFont = std::make_unique<FreeTypeFont>();

	FT_Face face;
	if (FT_New_Face(FreeTypeSytem, path, 0, &face))
	{
		return nullptr;
	}

	pFTFont->SetHandle(face);

	return std::unique_ptr<IFont>();
}

void FreeTypeFontLoader::UnLoad(IFont* pFont)
{

}
