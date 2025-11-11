////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     tfx_def.h
* @create   June 09, 2025
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/
#ifndef TFX_DEF_H
#define TFX_DEF_H

#include "macros.h"

#define TFX_VERSION "1.0.0"

#define TFX_NAMESPACE tfx

#define NSP TFX_NAMESPACE

#define __BEGIN_NAMESPACE__ namespace TFX_NAMESPACE {
#define __BEGIN_NAMESPACE_NO_TFX_ namespace {
#define __END_NAMESPACE__ }

#define __NAMESPACE_SEC__(_name_) namespace _name_ {

#define USING_NAMESPACE using namespace TFX_NAMESPACE;

#endif // !TFX_DEF_H