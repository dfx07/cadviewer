////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     tfx_math.h
* @create   Nov 11, 2025
* @brief    Define common types and constants
* @note     For conditions of distribution and use, see copyright notice in readme.txt
***********************************************************************************/

#ifndef TFX_MATH_H
#define TFX_MATH_H

#include "tfx_def.h"
#include "tfx_type.h"

#ifndef M_PI
#define M_PI 3.14159265358979323846
#endif // !M_PI


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

// //////////////////////////////////////////////////////////////////////////////////
// Custom types for 2D, 3D, and 4D vectors and matrices
#else

__BEGIN_NAMESPACE__

struct tagVec2
{
	float x, y;
	tagVec2() : x(0), y(0) {}
	tagVec2(float _x, float _y) : x(_x), y(_y) {}
	tagVec2 operator+(const tagVec2& other) const { return tagVec2(x + other.x, y + other.y); }
	tagVec2 operator-(const tagVec2& other) const { return tagVec2(x - other.x, y - other.y); }
	tagVec2 operator*(float scalar) const { return tagVec2(x * scalar, y * scalar); }
	tagVec2 operator/(float scalar) const { return tagVec2(x / scalar, y / scalar); }
};

struct tagVec3
{
	float x, y, z;
	tagVec3() : x(0), y(0), z(0) {}
	tagVec3(float _x, float _y) : x(_x), y(_y) {}
	tagVec3 operator+(const tagVec3& other) const { return tagVec3(x + other.x, y + other.y, z + other.z); }
	tagVec3 operator-(const tagVec3& other) const { return tagVec3(x - other.x, y - other.y, z - other.z); }
	tagVec3 operator*(float scalar) const { return tagVec3(x * scalar, y * scalar, z * scalar); }
	tagVec3 operator/(float scalar) const { return tagVec3(x / scalar, y / scalar, z / scalar); }
};


typedef tagVec2 Vec2;
typedef tagVec3 Vec3;
typedef tagVec2 Vec4;
typedef tagVec2 Mat2;
typedef tagVec2 Mat3;
typedef tagVec2 Mat4;

template<typename T>
auto ValuePtr(const T& obj) -> decltype(value_ptr(obj)) {
	return glm::value_ptr(obj);
}

#endif // USE_GLM

__END_NAMESPACE__

#endif // !TFX_TYPE_H
