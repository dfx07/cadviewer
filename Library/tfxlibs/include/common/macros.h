////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     macros.h
* @create   Nov 09, 2025
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/
#ifndef MACROS_H
#define MACROS_H

#ifdef _interface
#undef _interface
#endif
#define _interface struct

#if defined(_WIN32) || defined(_WIN64)
#	define _WIN_FLAT 1
#else
#	define _WIN_FLAT 0
#endif

#ifdef _WIN_FLAT
#    ifdef LIBRARY_EXPORTS
#        define LIBRARY_API __declspec(dllexport)
#        define LIBRARY_API_INLINE inline LIBRARY_API

#ifndef _NAME_MANGLING
#        define C_STYLE extern "C"
#else
#        define C_STYLE
#endif // _NAME_MANGLING

#    else
#        define LIBRARY_API __declspec(dllimport)
#    endif
#elif
#    define LIBRARY_API
#endif

/* utility macros */
#define NULL_RETURN(ptr, ...) if ((ptr) == nullptr) return __VA_ARGS__
#define NULL_CONTINUE(ptr) if((ptr) == nullptr) continue
#define NULL_BREAK(ptr) if ((ptr) == nullptr) break
#define SAFE_DELETE(ptr) if ((ptr) != nullptr) { delete (ptr); (ptr) = nullptr; }

#ifndef M_PI
#define M_PI 3.14159265358979323846
#endif // !M_PI

#endif // !MACROS_H