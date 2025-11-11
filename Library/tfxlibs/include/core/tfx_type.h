////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     tfx_type.h
* @create   June 09, 2025
* @brief    Define common types and constants
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/

#ifndef TFX_TYPE_H
#define TFX_TYPE_H

#include "tfx_def.h"

__BEGIN_NAMESPACE__

template<typename T>
struct _tfx2Point
{
public:
	T x = static_cast<T>(0);
	T y = static_cast<T>(0);

	_tfx2Point() = default;

	_tfx2Point(T _x, T _y) : x(_x), y(_y) {}

	template<typename U, typename V>
	_tfx2Point(const U& _x, const V& _y) :
		x(static_cast<T>(_x)), y(static_cast<T>(_y)){

	}

	template<typename U>
	_tfx2Point<T>& operator=(const _tfx2Point<U>& other) {
		x = static_cast<T>(other.x);
		y = static_cast<T>(other.y);
		return *this;
	}

	template<typename U>
	_tfx2Point<T> operator+(const _tfx2Point<U>& other) const {
		return _tfx2Point<T>(x + static_cast<T>(other.x), y + static_cast<T>(other.y));
	}

	template<typename U>
	_tfx2Point<T> operator-(const _tfx2Point<U>& other) const {
		return _tfx2Point<T>(x - static_cast<T>(other.x), y - static_cast<T>(other.y));
	}

	template<typename U>
	_tfx2Point<T>& operator+=(const _tfx2Point<U>& other) {
		x += static_cast<T>(other.x);
		y += static_cast<T>(other.y);
		return *this;
	}

	template<typename U>
	_tfx2Point<T>& operator-=(const _tfx2Point<U>& other) {
		x -= static_cast<T>(other.x);
		y -= static_cast<T>(other.y);
		return *this;
	}
};

__END_NAMESPACE__

#endif // !TFX_TYPE_H
