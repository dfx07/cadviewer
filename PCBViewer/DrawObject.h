#pragma once


#include "common/tfxtype.h"
#include <memory>

typedef tfx::Vec2 Vec2;
typedef tfx::Vec3 Vec3;
typedef tfx::Vec4 Vec4;
typedef tfx::Vec4 Col4;

class DrawObject;
typedef std::shared_ptr<DrawObject> DrawObjectPtr;

class DrawObject
{
public:
	DrawObject();
	virtual ~DrawObject();

public:
	virtual DrawObjectPtr Clone() = 0;
	virtual void Copy(DrawObject* pSource) = 0;

private:
	// Add any private members or methods if needed
};

