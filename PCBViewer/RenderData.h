////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Define interface render data
* @file  : RenderData.h
* @create: July 03, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

class RenderData
{
public:
	RenderData() { };
	virtual ~RenderData() { };

public:
	void SetUpdateFlags(int nFlags)
	{
		m_nUpdateFlags = nFlags;
	}

public:
	virtual bool Create()
	{
		// Implement the logic to create render data
	}

	virtual void Release()
	{
		// Implement the logic to release render data
	}

protected:
	int m_nUpdateFlags;
};

