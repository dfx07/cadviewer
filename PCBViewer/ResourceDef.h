#pragma once

#include "core/macros.h"
#include <memory>
#include <string>

#include "core/tfx_type_traits.h"

_interface IFont;
typedef std::shared_ptr<IFont> FontPtr;

_interface IFontAtlas;
typedef std::shared_ptr<IFontAtlas> FontAtlasPtr;

class FontManager; // string + font
class FontAtlasManager; // string + font-atlas

