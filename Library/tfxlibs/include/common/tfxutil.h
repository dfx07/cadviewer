////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     tfxutil.h
* @create   July 23, 2025
* @brief    Define utility
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/

#ifndef TFXUTIL_H
#define TFXUTIL_H

#include "tfxdef.h"

__BEGIN_NAMESPACE__


float Deg2Rad(const float fAngleDeg)
{
	return fAngleDeg * M_PI / 180.0;
}

float Rad2Deg(const float fAngleRad)
{
	return fAngleRad * 180.0 / M_PI;
}

__END_NAMESPACE__

#endif // !TFXUTIL_H
