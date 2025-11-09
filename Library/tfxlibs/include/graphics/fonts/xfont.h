////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Interface define font for rendering
* @file  : xfont.h
* @create: oct 06, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XFONT_H
#define XFONT_H

#include <memory>
#include "common/tfx_type.h"

#include <map>
#include <string>

_interface IFont
{
public:
	virtual ~IFont() = default;
public:
	virtual bool Load(const char* font_path) = 0;
	virtual void Unload() = 0;
	virtual std::string GetGUID() = 0;
};

class FontManager
{
public:
	FontManager() { }
	~FontManager() { }

public:
	void Add(const std::string& name, IFont* font)
	{
		m_man.insert({ name, font });
	}

	void Remove(const std::string& name)
	{
		auto itF = m_man.find(name);
		if (itF == m_man.end())
			return;

		itF->second->Unload();

		delete itF->second;
		m_man.erase(itF);
	}

	void Remove(IFont* font)
	{
		for (auto it = m_man.begin(); it != m_man.end(); ) {
			if (it->second == font)
				it = m_man.erase(it);
			else
				++it;
		}
	}

	IFont* Get(const std::string& name) const
	{
		auto itF = m_man.find(name);
		if (itF == m_man.end())
			return nullptr;

		return itF->second;
	}

protected:
	std::map<std::string, IFont*> m_man;
};

#endif // !XFONT_H