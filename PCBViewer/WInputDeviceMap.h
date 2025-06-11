////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
* MIT software Licencs, see the accompanying
* http://www.opensource.org/licenses/mit-license.php
* For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************
* @file     WViewDeicve.h
* @create   June 11, 2025
* @brief    Defines interfaces (abstract classes) representing behaviors that can be attached to a "view"
***********************************************************************************/

#include "modules/inputdevice.h"

class WInputDeviceMap
{
public:
	static TFXMouseButton MapWinBtn2TFXBtn(int nButton);
	static TFXMouseDragDropState MapDrag2DragState(int nState);
};