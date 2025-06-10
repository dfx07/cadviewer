﻿////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
* @Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*            All rights reserved.
************************************************************************************
* @file     xcamera.h
* @create   June 09, 2025
* @brief    Camera define
* @note     For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XCAMERA_H
#define XCAMERA_H

#include "common/tfxtype.h"

__BEGIN_NAMESPACE__

enum class CameraType
{
	NONE,
	C2D,
	C3D,
};

enum struct View
{

};

enum class CameraMode
{
	CMODE_NORMAL,
	CMODE_NOZOOM,
};

/**********************************************************************************
* ⮟⮟ Class name: Camera
*  Camera template class
***********************************************************************************/
class Camera
{
public:
	Camera();
	virtual ~Camera();

public:
	virtual CameraType GetType() const = 0;

	virtual void UpdateViewMatrix() = 0;
	virtual void UpdateProjMatrix() = 0;

	virtual void UpdateMatrix() = 0;

	virtual Mat4& GetViewMatrix();
	virtual Mat4& GetProjMatrix();
	virtual Mat4& GetModelMatrix();

public:
	/*******************************************************************************
	*! @brief  : Chuyển đổi từ tọa độ [Trái trên] sang tọa độ tại [Trung tâm] trên view
	*! @param    [in] p : tọa độ trên view
	*! @return : Vec2 tọa độ thực tế
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*! @note   : Tọa độ [x, y] đầu vào là tọa độ trên view  (Left Top)
	*******************************************************************************/
	Vec2 PointLT2Center(const Vec2& p) const;

public:
	/*******************************************************************************
	*! @brief  : Chuyển đổi từ tọa độ [Trái trên] sang tọa độ tại [Trung tâm] trên view   [3D]
	*! @param    [in] p : tọa độ trên view
	*! @return : Vec2 tọa độ thực tế
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*! @note   : Tọa độ [x, y] đầu vào là tọa độ trên view  (Left Top)
	*******************************************************************************/
	void SetCamera(const Vec3& pos, const Vec3& dir, const Vec3& up = Vec3(0, 1, 0));

	/*******************************************************************************
	*! @brief  : Set view size
	*! @param    [in] width  : width view
	*! @param    [in] height : height view
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void SetView(const int width, const int height);

	/*******************************************************************************
	*! @brief  : Set near and far distance camera
	*! @param    [in] _near : near distace clip plane
	*! @param    [in] _far  : far distance clip plane
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void SetDistPlane(const float _near, const float _far);

	/*******************************************************************************
	*! @brief  : Get camera position
	*! @param  : [in] pos : point3D
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	Vec3 GetPosition() const;

	/*******************************************************************************
	*! @brief  : Set camera position use point3D
	*! @param  : [in] pos : point3D
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void SetPosition(const Vec3& pos);

	/*******************************************************************************
	*! @brief  : Vị trí camera dự vào vị trí target góc quay và khoảng cách  [3D]
	*! @param  : [in] p : tọa độ trên view
	*! @return : Vec2 tọa độ thực tế
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*! @note   : Tọa độ [x, y] đầu vào là tọa độ trên view  (Left Top)
	*******************************************************************************/
	void SetPosition(float phi, float theta, float distance, Vec3 target);

	/*******************************************************************************
	*! @brief  : Move camera
	*! @param  : [in] vOffset : offset Vec3
	*! @return : Vec2 tọa độ thực tế
	*! @author : thuong.nv          - [Date] : 2025.06.10
	*******************************************************************************/
	void Move(const Vec3& vOffset);

protected:
	Vec3		m_position;     // Vị trí camera
	Vec3		m_direction;    // Hướng camera trỏ tới (ngược hướng với hướng camera thực tế) - Nó nên là vector đơn vị (normal)
	Vec3		m_up;           // Hướng lên camera

	int			m_iWidth;       // Chiều rộng của view
	int			m_iHeight;      // Chiều cao của view
	float		m_fNear;        // Mặt phẳng gần
	float		m_fFar;         // Mặt phẳng xa

	Mat4		m_viewMat;      // view matrix
	Mat4		m_projMat;      // projection matrix 
	Mat4		m_modelMat;     // model matrix (sử dụng OpenGL 1.1)
};

/**********************************************************************************
* ⮟⮟ Class name: Camera2D
* Base class for window handle inheritance
***********************************************************************************/
class Camera2D : public Camera
{
private:
	float	m_fZoom;         // Tỷ lệ zoom
	Mat4	m_projMatNozoom; //projection matrix nozoom

public:
	Camera2D();
	virtual ~Camera2D();

public:
	/*******************************************************************************
	*! @brief  : Chuyển đổi tọa độ điểm từ view sang tọa độ thực tế
	*! @param    [in] p : tọa độ trên view
	*! @return : Vec2 tọa độ thực tế
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*! @note   : Tọa độ [x, y] đầu vào là tọa độ trên view  (Left Top)
	*******************************************************************************/
	Vec2 PointLocal2Global(const Vec2& p) const;

	/*******************************************************************************
	*! @brief  : Chuyển đổi tọa độ điểm từ thực thế sang tọa độ view
	*! @param    [in] p : real position to local view
	*! @return : Vec2 view position
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*! @note   : Tọa độ [x, y] đầu vào là tọa độ trên view  (Left Top)
	*******************************************************************************/
	Vec2 PointGlobal2Local(const Vec2& p) const;

public:
	/*******************************************************************************
	*! @brief  : Lấy ma trận chiếu nhưng không zoom
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	Mat4& GetProjMatrixNozoom();

public:
	/*******************************************************************************
	*! @brief  : Cập nhật view matrix
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	virtual void UpdateViewMatrix();

	/*******************************************************************************
	*! @brief  : Cập nhật projection matrix
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	virtual void UpdateProjMatrix();

	/*******************************************************************************
	*! @brief  : Load ma trận cho  GPU
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	virtual void UpdateMatrix();

protected:
	/*******************************************************************************
	*! @brief  : Cập nhật thông số zoom có giới hạn giá trị
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void UpdateZoom(float zDelta);

public:
	virtual CameraType GetType() const;

	float GetZoom() const;

	/*******************************************************************************
	*! @brief  : Target camera vào một vị trí (tọa độ local) với lượng zoom delta
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void ZoomTo(float x, float y, float delta_z);

	/*******************************************************************************
	*! @brief  : Dịch chuyển tọa độ camera đi một khoảng delta x và delta y
	*! @param    [in] delx : tọa độ x trên local view
	*! @param    [in] dely : tọa độ y trên local view
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void Move(float delx, float dely);
};

/**********************************************************************************
* ⮟⮟ Class name: Camera3D
* Base class for window handle inheritance
***********************************************************************************/
class Camera3D : public Camera
{
public:
	Camera3D();

	virtual ~Camera3D();

	/*******************************************************************************
	*! @brief  : Cập nhật model matrix
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	virtual void SetModelMatrix(Mat4& mat);

	/*******************************************************************************
	*! @brief  : Cập nhật view matrix
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	virtual void UpdateViewMatrix();

	/*******************************************************************************
	*! @brief  : Cập nhật projection matrix
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	virtual void UpdateProjMatrix();

	/*******************************************************************************
	*! @brief  : Load ma trận cho  GPU
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	virtual void UpdateMatrix();

private:
	void UpdateOrbitTarget(float phi, float theta);

public:
	virtual CameraType GetType() const;

	void SetFieldOfView(float fov);

	/*******************************************************************************
	*! @brief  : Thiết lập thông số cho chế độ xoay target
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void SetUpOrbitTarget(Vec3 position, Vec3 target);

	void OrbitTarget(float delPhi, float delTheta);

	/*******************************************************************************
	*! @brief  : Thu vị trí camera vào vị trí target đã được đặt trước
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void ZoomTo(float delta);

	/*******************************************************************************
	*! @brief  : Di chuyển camera theo hướng tạo bưởi direction và up
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void OrbitMove(float delta);

	/*******************************************************************************
	*! @brief  :  Di chuyển camera theo mặt phẳng của camera
	*! @param    [in] mode : Chế độ zoom hoặc không zoom
	*! @return : void
	*! @author : thuong.nv          - [Date] : 22/04/2023
	*******************************************************************************/
	void OrbitMove1(float x, float y);

private:
	float			m_fFov;    // Góc nhìn

	float			m_fTheta;  // Góc với trục Oz
	float			m_fPhi;    // Góc phụ với trục Oy
	Vec3			m_vTarget; // Vị trí camera Target
	float			m_fDis;    // Vị trí camera Target

	int				m_iMode;   // Chế độ Camera đang hoạt động
};

__END_NAMESPACE__

#endif // !XGPCAMERA_H

