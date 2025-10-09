#include "graphics/camera/xcamera.h"

Camera::Camera():
	m_iWidth(0), m_iHeight(0), m_fFar(0.f), m_fNear(0.f)
{
	m_position = { 0.f, 0.f, 0.f };
	m_direction = { 0.f, 0.f, 1.f };
	m_up = { 0.f, 1.f, 0.f };

	m_viewMat = Mat4(1.f);
	m_projMat = Mat4(1.f);
	m_modelMat = Mat4(1.f);
}

Camera::~Camera()
{

}

/*******************************************************************************
*! @brief  : Chuyển đổi từ tọa độ [Trái trên] sang tọa độ tại [Trung tâm] trên view
*! @param    [in] p : tọa độ trên view
*! @return : Vec2 tọa độ thực tế
*! @author : thuong.nv          - [Date] : 22/04/2023
*! @note   : Tọa độ [x, y] đầu vào là tọa độ trên view  (Left Top)
*******************************************************************************/

Vec2 Camera::PointLT2Center(const Vec2& p) const
{
	Vec2 point = { 0, 0 };  // Để mặc định
	point.x = (p.x - m_iWidth / 2);
	point.y = -(p.y - m_iHeight / 2);
	return point;
}


Mat4& Camera::GetViewMatrix()
{
	return m_viewMat;
}


Mat4& Camera::GetProjMatrix()
{
	return m_projMat;
}


Mat4& Camera::GetModelMatrix()
{
	return m_modelMat;
}

Vec3 Camera::World2ScreenPoint(const Vec3& p) const
{
	return Vec3();
}

Vec3 Camera::Screen2WorldPoint(const Vec3& p) const
{
	return Vec3();
}

void Camera::ZoomTo(Vec3 ptTarget, const float delta_z)
{
}


void Camera::SetCamera(const Vec3& pos, const Vec3& dir, const Vec3& up)
{
	m_position = pos;
	m_direction = dir;
	m_up = up;
}


void Camera::SetView(const int width, const int height)
{
	m_iWidth = width;
	m_iHeight = height;
}

Vec2 Camera::GetView() const
{
	return Vec2(m_iWidth, m_iHeight);
}


void Camera::SetDistPlane(const float _near, const float _far)
{
	m_fNear = _near;
	m_fFar = _far;
}


Vec3 Camera::GetPosition() const
{
	return m_position;
}


void Camera::SetPosition(const Vec3& pos)
{
	m_position = pos;
}


void Camera::SetPosition(float phi, float theta, float distance, Vec3 target)
{
	// caculate position based on spherical coordinates
	float x = cos(phi) * sin(theta);
	float y = sin(phi);
	float z = cos(phi) * cos(theta);
	Vec3 position = Vec3(x, y, z) * distance;

	// move camera to target position
	float dis = glm::distance(target, Vec3(0, 0, 0));
	if (fabs(dis) > 0.1)
	{
		Vec3 vut = glm::normalize(target - Vec3(0, 0, 0));

		m_position = position + (vut * dis);
		m_direction = glm::normalize(m_position - target);
	}
	else
	{
		m_position = position;
	}
}


void Camera::Move(const Vec3& vOffset)
{
	m_position += vOffset;

	UpdateViewMatrix();
}

void Camera::UpdateZoom(const float fDelta)
{
}


/// <summary>
/// Camera2D ////////////////////////////////////////////////////////////
/// </summary>

Camera2D::Camera2D() : Camera(),
m_fZoom(1.f)
{
	m_projMatNozoom = Mat4(1.f);
}

Camera2D::~Camera2D()
{

}

/*******************************************************************************
*! @brief  : Chuyển đổi tọa độ thực tế sang tọa độ view
*! @param    [in] p : tọa độ thực tế
*! @return : Vec3 tọa độ trên screen
*! @author : thuong.nv          - [Date] : 2025.06.12
*! @note   : Tọa độ [x, y] đầu vào là tọa độ trên view  (Left Top)
*******************************************************************************/
Vec3 Camera2D::World2ScreenPoint(const Vec3& p) const
{
	Vec3 point = { 0 , 0 , 0};   // Để mặc định
	point.x = (float(m_iWidth / 2) - m_fZoom * (m_position.x - p.x));
	point.y = (m_fZoom * (m_position.y - p.y) + float(m_iHeight / 2));
	return point;
}

/*******************************************************************************
*! @brief  : Chuyển đổi tọa độ screen sang tọa độ thực tế
*! @param    [in] p : tọa độ screen
*! @return : Vec3 tọa độ thực tế.
*! @author : thuong.nv          - [Date] : 2025.06.12
*! @note   : Tọa độ [x, y] đầu vào là tọa độ trên view  (Left Top)
*******************************************************************************/
Vec3 Camera2D::Screen2WorldPoint(const Vec3& p) const
{
	Vec3 point = { 0 , 0 , 0 };    // Để mặc định
	point.x = m_position.x + (p.x - float(m_iWidth / 2)) / m_fZoom;
	point.y = m_position.y - (p.y - float(m_iHeight / 2)) / m_fZoom;
	return point;
}

/*******************************************************************************
*! @brief  : Target camera vào một vị trí (tọa độ local) với lượng zoom delta
*! @return : void
*! @author : thuong.nv          - [Date] : 2025.06.12
*******************************************************************************/
void Camera2D::ZoomTo(Vec3 ptTarget, const float delta_z)
{
	ptTarget.z = 0.f;

	// Tính tọa độ thực tế của nó trước và sau khi set zoom
	Vec2 pGlobalOld = Screen2WorldPoint(ptTarget);
	UpdateZoom(delta_z);
	Vec2 pGlobalNew = Screen2WorldPoint(ptTarget);
	Vec2 vMove = pGlobalNew - pGlobalOld;

	// Di chuyển vị trí của camera một đoạn vì vị trí x, y được giữ nguyên
	m_position.x -= vMove.x;
	m_position.y -= vMove.y;
}

float Camera2D::GetZoom() const
{
	return m_fZoom;
}

Mat4& Camera2D::GetProjMatrixNozoom()
{
	return m_projMatNozoom;
}


/*******************************************************************************
*! @brief  : Cập nhật view matrix
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera2D::UpdateViewMatrix()
{
	m_viewMat = glm::lookAt(m_position, m_position - m_direction, m_up);
}

/*******************************************************************************
*! @brief  : Cập nhật projection matrix
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera2D::UpdateProjMatrix()
{
	// Projection matrix không sử dụng zoom
	float left		= -float(m_iWidth / 2);
	float right		=  float(m_iWidth / 2);
	float bottom	= -float(m_iHeight / 2);
	float top		=  float(m_iHeight / 2);

	m_projMatNozoom = glm::ortho(left, right, bottom, top, float(m_fNear), float(m_fFar));

	// Projection matrix sử dụng thuộc tính zoom
	left	= left / m_fZoom;
	right	= right / m_fZoom;
	bottom	= bottom / m_fZoom;
	top		= top / m_fZoom;

	m_projMat = glm::ortho(left, right, bottom, top, float(m_fNear), float(m_fFar));
}

/*******************************************************************************
*! @brief  : Load ma trận cho  GPU
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera2D::UpdateMatrix()
{
	UpdateViewMatrix();
	UpdateProjMatrix();
}

/*******************************************************************************
*! @brief  : Cập nhật thông số zoom có giới hạn giá trị
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera2D::UpdateZoom(float zDelta)
{
	static constexpr float ZOOM_MIN = 0.01f;
	static constexpr float ZOOM_MAX = 100.f;
	static constexpr float ZOOM_FACTOR = 0.9f;

	// [Note] Điều chỉnh zoom này có lợi thế là sẽ zoom đều             
	// Khi zoom càng bé thì giảm zoom càng chậm thay cho việc tuyến tính
	// Tương tự nếu tăng sẽ nhanh nếu nếu zoom càng lớn                 
	if (zDelta > 0)
	{
		m_fZoom /= ZOOM_FACTOR;
	}
	else
	{
		m_fZoom *= ZOOM_FACTOR;
	}

	// Xử lý trong trường hợp giới hạn
	if (m_fZoom < ZOOM_MIN)
	{
		m_fZoom = ZOOM_MIN;
	}

	else if (m_fZoom > ZOOM_MAX)
	{
		m_fZoom = ZOOM_MAX;
	}
}

CameraType Camera2D::GetType() const
{
	return CameraType::C2D;
}

/*******************************************************************************
*! @brief  : Dịch chuyển tọa độ camera đi một khoảng delta x và delta y
*! @param    [in] delx : tọa độ x trên local view
*! @param    [in] dely : tọa độ y trên local view
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera2D::Move(float delx, float dely)
{
	float deltax_glo = -delx / m_fZoom;
	float deltay_glo = dely / m_fZoom;

	m_position += Vec3(deltax_glo, deltay_glo, 0);
}

/// <summary>
/// Camera3D ////////////////////////////////////////////////////////////
/// </summary>

Camera3D::Camera3D() : Camera(), m_fFov(1.f), m_fTheta(0.f), m_fPhi(0.f),
m_fDis(0.f), m_iMode(0)
{
	m_modelMat = Mat4(1.f);
	m_vTarget = { 0.f, 0.f, 0.f };
}

Camera3D::~Camera3D()
{

}

/*******************************************************************************
*! @brief  : Cập nhật model matrix
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera3D::SetModelMatrix(Mat4& mat)
{
	m_modelMat = mat;
}

/*******************************************************************************
*! @brief  : Cập nhật view matrix
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera3D::UpdateViewMatrix()
{
	m_viewMat = glm::lookAt(m_position, m_position - m_direction, m_up);
}

/*******************************************************************************
*! @brief  : Cập nhật projection matrix
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera3D::UpdateProjMatrix()
{
	float aspect = float(m_iWidth) / float(m_iHeight);
	m_projMat = glm::perspective(glm::radians(m_fFov), aspect, m_fNear, m_fFar);
}

/*******************************************************************************
*! @brief  : Load ma trận cho  GPU
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera3D::UpdateMatrix()
{
	this->UpdateViewMatrix();
	this->UpdateProjMatrix();
}

void Camera3D::UpdateOrbitTarget(float phi, float theta)
{
	//↓ 2021.09.22 N.V.Thuong [Xử lý ngoại lệ và khoảng xoay]
	//if (fabs(phi) > M3_HPI)
	//{
	//	phi = (phi < 0.0) ? (-M3_HPI + M3_ALG_EPSILON) : (M3_HPI - M3_ALG_EPSILON);
	//}
	//if (theta > M3_2PI)
	//{
	//	int factor = int(theta / M3_2PI);
	//	theta = theta - factor * M3_2PI;
	//}
	//else if (theta < 0)
	//{
	//	int factor = int(theta / M3_2PI);
	//	theta = theta + abs(factor + 1) * M3_2PI;
	//}
	////↑ 2021.09.22 N.V.Thuong [Xử lý ngoại lệ và khoảng xoay]

	////↓ 2021.09.22 N.V.Thuong [Cập nhật thông số camera]
	//m_fPhi = phi;
	//m_fTheta = theta;
	//m_position = M3D_CalPosition(m_vTarget, m_fDis, m_fPhi, m_fTheta);

	//if (m_fDis >= M3_EPSILON)
	//{
	//	m_direction = glm::normalize(m_position - m_vTarget);
	//}
	//else
	//{
	//	m_direction = Vec3(0, 0, 0);
	//}
	//↑ 2021.09.22 N.V.Thuong [Cập nhật thông số camera]
}

CameraType Camera3D::GetType() const
{
	return CameraType::C3D;
}

void Camera3D::SetFieldOfView(float fov)
{
	m_fFov = fov;
}

void Camera3D::SetUpOrbitTarget(Vec3 position, Vec3 target)
{
	m_vTarget = target;
	m_fDis = glm::distance(position, target);
	//M3D_GetAnglePositionOxyz(position, target, m_fPhi, m_fTheta);
	this->UpdateOrbitTarget(m_fPhi, m_fTheta);
}

void Camera3D::OrbitTarget(float delPhi, float delTheta)
{
	float phi = m_fPhi + delPhi;
	float theta = m_fTheta + delTheta;
	this->UpdateOrbitTarget(phi, theta);
}

/*******************************************************************************
*! @brief  : Thu vị trí camera vào vị trí target đã được đặt trước
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera3D::ZoomTo(float delta)
{
	Vec3 vUint = glm::normalize(m_direction);
	Vec3 vPositionNew = m_position - vUint * delta;
	Vec3 vUintNew = glm::normalize(vPositionNew - m_vTarget);

	float fDot = glm::dot(vUint, vUintNew);
	if (fDot <= 0.0)
	{
		m_position = m_vTarget + vUint;
	}
	else
	{
		m_position = vPositionNew;
	}
	m_fDis = glm::distance(m_position, m_vTarget);
}

/*******************************************************************************
*! @brief  : Di chuyển camera theo hướng tạo bưởi direction và up
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera3D::OrbitMove(float delta)
{
	Vec3 vuint_move = glm::cross(m_up, -m_direction);
	m_position += vuint_move * delta;
	m_vTarget += vuint_move * delta;
	this->UpdateViewMatrix();
}

Vec3 Camera3D::World2ScreenPoint(const Vec3& p) const
{
	return Vec3();
}

/*******************************************************************************
*! @brief  :  Di chuyển camera theo mặt phẳng của camera
*! @param    [in] mode : Chế độ zoom hoặc không zoom
*! @return : void
*! @author : thuong.nv          - [Date] : 22/04/2023
*******************************************************************************/
void Camera3D::OrbitMove1(float x, float y)
{
	float xtemp = m_position.x + x;
	float ytemp = m_position.y + y;

	//↓ 2021.09.22 N.V.Thuong [Tính tọa độ z của camera mới]
	Vec3 vn = glm::normalize(m_direction);
	float d = vn.x * m_position.x + vn.y * m_position.y + vn.z * m_position.z;
	float ztemp = (d - (vn.x * xtemp + vn.y * ytemp)) / vn.z;

	Vec3 temp(xtemp, ytemp, ztemp);
	if (glm::distance(temp, m_position) > 0.001f)
	{
		Vec3 v = glm::normalize(temp - m_position);
		m_position += v * (0.12f);
		m_vTarget += v * (0.12f);
		this->UpdateViewMatrix();
	}
}

/*******************************************************************************
*! @brief  : Chuyển đổi tọa độ screen sang tọa độ thực tế
*! @param    [in] p : tọa độ screen
*! @return : Vec3 tọa độ thực tế.
*! @author : thuong.nv          - [Date] : 2025.06.12
*! @note   : Tọa độ [x, y] đầu vào là tọa độ trên view  (Left Top)
*******************************************************************************/
Vec3 Camera3D::Screen2WorldPoint(const Vec3& p) const
{
	// TODO : Implement
	return Vec3();
}

/*******************************************************************************
*! @brief  : Target camera vào một vị trí (tọa độ local) với lượng zoom delta
*! @return : void
*! @author : thuong.nv          - [Date] : 2025.06.12
*******************************************************************************/
void Camera3D::ZoomTo(Vec3 ptTarget, const float delta_z)
{
	// TODO : Implement
}


