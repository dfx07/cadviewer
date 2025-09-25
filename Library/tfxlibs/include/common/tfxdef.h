////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     tfxdef.h
* @create   June 09, 2025
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/
#ifndef TFXDEF_H
#define TFXDEF_H

#define TFX_VERSION "1.0.0"

#define __NAME_SPACE__ tfx

#define __BEGIN_NAMESPACE__ namespace __NAME_SPACE__ {
#define __BEGIN_NAMESPACE_NO_TFX_ namespace {
#define __END_NAMESPACE__ }

#define USING_NAMESPACE using namespace __NAME_SPACE__;

#ifdef _interface
#undef _interface
#endif
#define _interface struct

#ifndef M_PI
#define M_PI 3.14159265358979323846
#endif // !M_PI

#endif // !TFXDEF_H