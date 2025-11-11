
////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
* MIT software Licencs, see the accompanying
* http://www.opensource.org/licenses/mit-license.php
* For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************
* @file     ViewBehavior.h
* @create   June 11, 2025
* @brief    Defines interfaces (abstract classes) representing behaviors that can be attached to a "view"
***********************************************************************************/

#include <core/tfx_def.h>
#include <modules/inputdevice.h>

_interface ITFXMouseInteractiveView
{
public:
	virtual ~ITFXMouseInteractiveView() {}

	virtual void OnMouseEnter(TFXEvent* event) = 0;
	virtual void OnMouseMove(TFXMouseEvent* event) = 0;
	virtual void OnMouseDown(TFXMouseEvent* event) = 0;
	virtual void OnMouseUp(TFXMouseEvent* event) = 0;
	virtual void OnMouseDoubleClick(TFXMouseEvent* event) = 0;
	virtual void OnMouseWheel(TFXMouseEvent* event) = 0;
};

_interface ITFXKeyboardInteractiveView
{
public:
	virtual ~ITFXKeyboardInteractiveView() {}

	virtual void OnKeyDown(TFXKeyEvent* event) = 0;
	virtual void OnKeyUp(TFXKeyEvent* event) = 0;
};