////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     tfx_util.h
* @create   July 23, 2025
* @brief    Define utility functions
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/

#ifndef TFX_UTILS_H
#define TFX_UTILS_H

#include "tfx_def.h"
#include <string>

__BEGIN_NAMESPACE__

template<typename U, typename T = U>
constexpr T Deg2Rad(const U dbAngleDeg)
{
	return static_cast<T>(dbAngleDeg) * static_cast<T>(M_PI) / static_cast<T>(180.0);
};

template<typename U, typename T = U>
constexpr T Rad2Deg(const U dbAngleRad)
{
	return static_cast<T>(dbAngleRad) * static_cast<T>(180.0) / static_cast<T>(M_PI);
};

std::string CreateGUID();

__END_NAMESPACE__

#endif // !TFX_UTILS_H
