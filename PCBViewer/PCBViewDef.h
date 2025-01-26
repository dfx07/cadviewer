#pragma once
#include <Common/apidef.h>

struct ContextConfig
{
	Bool m_bUseContextExt{ True };
	Int  m_nAntialiasingLevel{ 0 }; // 0~8
};