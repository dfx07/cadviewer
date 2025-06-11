////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
* MIT software Licencs, see the accompanying
* http://www.opensource.org/licenses/mit-license.php
* For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************
* @file     inputdevice.h
* @create   June 11, 2025
* @brief    Defines input device
***********************************************************************************/

#ifndef INPUTDEVICE_H
#define INPUTDEVICE_H

enum class TFXMouseButton
{
	UNKNOWN,
	LEFT,
	MID,
	RIGHT
};

enum class TFXMouseDragDropState
{
	UNKNOW,
	DRAG,
	MOVE,
	DROP
};

enum class TFXMouseState
{
	UNKNOW,
	DOWN,
	UP,
	DBLCLICK,
};

typedef struct _TFX_PT
{
	int x;
	int y;

	_TFX_PT(int _x = 0, int _y = 0) :
		x(_x), y(_y)
	{

	}

} TFX_DevicePt;

enum class TFXKeyboard
{
	AltLeft			= 0x12, // (18)
	AltRight		= 0x12, // (18)
	CapsLock		= 0x14, // (20)
	ControlLeft		= 0x11, // (17)
	ControlRight	= 0x11, // (17)
	OSLeft			= 0x5B, // (91)
	OSRight			= 0x5C, // (92)
	ShiftLeft		= 0x10, // (16)
	ShiftRight		= 0x10, // (16)
	Delete			= 0x2E, // (46)
	Escapex			= 0x1B, // (27)

	Digit1			= 0x31, // (49)
	Digit2			= 0x32, // (50)
	Digit3			= 0x33, // (51)
	Digit4			= 0x34, // (52)
	Digit5			= 0x35, // (53)
	Digit6			= 0x36, // (54)
	Digit7			= 0x37, // (55)
	Digit8			= 0x38, // (56)
	Digit9			= 0x39, // (57)
	Digit0			= 0x30, // (48)
	KeyA			= 0x41, // (65)
	KeyB			= 0x42, // (66)
	KeyC			= 0x43, // (67)
	KeyD			= 0x44, // (68)
	KeyE			= 0x45, // (69)
	KeyF			= 0x46, // (70)
	KeyG			= 0x47, // (71)
	KeyH			= 0x48, // (72)
	KeyI			= 0x49, // (73)
	KeyJ			= 0x4A, // (74)
	KeyK			= 0x4B, // (75)
	KeyL			= 0x4C, // (76)
	KeyM			= 0x4D, // (77)
	KeyN			= 0x4E, // (78)
	KeyO			= 0x4F, // (79)
	KeyP			= 0x50, // (80)
	KeyQ			= 0x51, // (81)
	KeyR			= 0x52, // (82)
	KeyS			= 0x53, // (83)
	KeyT			= 0x54, // (84)
	KeyU			= 0x55, // (85)
	KeyV			= 0x56, // (86)
	KeyW			= 0x57, // (87)
	KeyX			= 0x58, // (88)
	KeyY			= 0x59, // (89)
	KeyZ			= 0x5A, // (90)


	Left			= 0x25, // (37)   Left arrow.
	Up				= 0x26, // (38)   Up arrow.
	Right			= 0x27, // (39)   Right arrow.
	Down			= 0x28, // (40)   Down arrow.


	F1				= 0x70, // (112)
	F2				= 0x71, // (113)
	F3				= 0x72, // (114)
	F4				= 0x73, // (115)
	F5				= 0x74, // (116)
	F6				= 0x75, // (117)
	F7				= 0x76, // (118)
	F8				= 0x77, // (119)
	F9				= 0x78, // (120)
	F10				= 0x79, // (121)
	F11				= 0x7A, // (122)
	F12				= 0x7B, // (123)
	F13				= 0x7C, // (124)
	F14				= 0x7D, // (125)
	F15				= 0x7E, // (126)
	F16				= 0x7F, // (127)
	F17				= 0x80, // (128)
	F18				= 0x81, // (129)
	F19				= 0x82, // (130)
	F20				= 0x83, // (131)
	F21				= 0x84, // (132)
	F22				= 0x85, // (133)
	F23				= 0x86, // (134)
	F24				= 0x87, // (135)
};

struct TFXEvent
{

};

struct TFXMouseEvent : TFXEvent
{
	TFX_DevicePt	m_Pt;
	TFXMouseButton	m_Button;
	TFXMouseState	m_State;
	float			m_Delta;
	TFXMouseDragDropState m_DragDropState;

	TFXMouseEvent():
		m_Delta(0.f), m_Button(TFXMouseButton::UNKNOWN),
		m_State(TFXMouseState::UNKNOW),
		m_DragDropState(TFXMouseDragDropState::UNKNOW)
	{

	}
};

struct TFXKeyEvent : TFXEvent
{
	TFXKeyboard		m_KeyCode;
};

#endif // !INPUTDEVICE_H
