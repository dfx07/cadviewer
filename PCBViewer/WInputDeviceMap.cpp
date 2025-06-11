#include "WInputDeviceMap.h"

/* WPF -> TFX
	//
	// Summary:
	//     The left mouse button.
	Left = 0,
	//
	// Summary:
	//     The middle mouse button.
	Middle = 1,
	//
	// Summary:
	//     The right mouse button.
	Right = 2,
	//
	// Summary:
	//     The first extended mouse button.
	XButton1 = 3,
	//
	// Summary:
	//     The second extended mouse button.
	XButton2 = 4
*/
TFXMouseButton WInputDeviceMap::MapWinBtn2TFXBtn(int nButton)
{
	switch (nButton)
	{
	case 0:
		return TFXMouseButton::LEFT;
	case 1:
		return TFXMouseButton::MID;
	case 2:
		return TFXMouseButton::RIGHT;
	default:
		return TFXMouseButton::UNKNOWN;
	}
}

TFXMouseDragDropState WInputDeviceMap::MapDrag2DragState(int nState)
{
	switch (nState)
	{
	case 0:
		return TFXMouseDragDropState::DRAG;
	case 1:
		return TFXMouseDragDropState::MOVE;
	case 2:
		return TFXMouseDragDropState::DROP;
	default:
		return TFXMouseDragDropState::UNKNOW;
	}
}
