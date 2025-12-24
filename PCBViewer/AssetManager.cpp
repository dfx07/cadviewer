#pragma once

#include "ResourceDef.h"
#include "core/tfx_type_traits.h"
#include "Font.h"


class FontManager : public tfx::_tfxManager<std::string, FontPtr>
{
public:
	virtual bool Add(const std::string& name, FontPtr& font) override;
	virtual bool Remove(const std::string& name) override;
};

class FontAtlasManager : public tfx::_tfxManager<std::string, FontAtlasPtr>
{

};
