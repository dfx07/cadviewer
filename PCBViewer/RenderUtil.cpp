#include "RenderUtil.h"

Point2 Rotate(Point2 point, Point2 center, float angleRad)
{
	double cosAngle = cos(static_cast<double>(angleRad));
	double sinAngle = sin(static_cast<double>(angleRad));

	// Translate point to origin
	point.x -= center.x;
	point.y -= center.y;

	// Rotate point
	double xNew = (double)point.x * cosAngle - (double)point.y * sinAngle;
	double yNew = (double)point.x * sinAngle + (double)point.y * cosAngle;

	// Translate point back
	point.x = xNew + center.x;
	point.y = yNew + center.y;

	return point;
}
