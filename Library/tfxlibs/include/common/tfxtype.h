////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     tfxtype.h
* @create   June 09, 2025
* @brief    Define common types and constants
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/

#ifndef TFXTYPE_H
#define TFXTYPE_H

#include "tfxdef.h"

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


// //////////////////////////////////////////////////////////////////////////////////
// User can define USE_GLM to use GLM types, otherwise custom types will be used
#ifdef USE_GLM

#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"
#include "glm/gtc/type_ptr.hpp"

__BEGIN_NAMESPACE__

typedef glm::vec2 Vec2;
typedef glm::vec3 Vec3;
typedef glm::vec4 Vec4;
typedef glm::mat2 Mat2;
typedef glm::mat3 Mat3;
typedef glm::mat4 Mat4;


template<typename T>
auto ValuePtr(const T& obj) -> decltype(glm::value_ptr(obj)) {
	return glm::value_ptr(obj);
}

typedef _tfx2Point<float> TFX2Point;

__END_NAMESPACE__

// //////////////////////////////////////////////////////////////////////////////////
// Custom types for 2D, 3D, and 4D vectors and matrices
#else

__BEGIN_NAMESPACE__

struct tagVec2
{
	float x, y;
	Vec2() : x(0), y(0) {}
	Vec2(float _x, float _y) : x(_x), y(_y) {}
	Vec2 operator+(const Vec2& other) const { return Vec2(x + other.x, y + other.y); }
	Vec2 operator-(const Vec2& other) const { return Vec2(x - other.x, y - other.y); }
	Vec2 operator*(float scalar) const { return Vec2(x * scalar, y * scalar); }
	Vec2 operator/(float scalar) const { return Vec2(x / scalar, y / scalar); }
};


typedef tagVec2 Vec2;
typedef tagVec2 Vec3;
typedef tagVec2 Vec4;
typedef tagVec2 Mat2;
typedef tagVec2 Mat3;
typedef tagVec2 Mat4;

__BEGIN_NAMESPACE__

#endif // USE_GLM

#endif // !TFXTYPE_H
