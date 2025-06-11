#pragma once
#include <Common/apidef.h>

struct ContextConfig
{
	_BOOL m_bUseContextExt{ TRUE };
	_INT  m_nAntialiasingLevel{ 0 }; // 0~8
};