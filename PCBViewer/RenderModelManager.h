////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Mangager for rendering models in PCB Viewer
* @file  : RenderModelManager.h
* @create: June 19, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef RENDERMODELMANAGER_H
#define RENDERMODELMANAGER_H

#include "PCBViewType.h"
#include <unordered_map>

class RenderModelManager
{
public:
	int AddModel(const DrawObjectPtr pObj);
	DrawObjectPtr GetModel(const int nID);
	void RemoveModel(const int nID);

private:
	std::unordered_map<int, DrawObjectPtr> m_models;
};

#endif // !RENDERMODELMANAGER_H

