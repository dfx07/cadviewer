using CadViewer.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace CadViewer.Components
{
	/***
	 * Create window handle for OpenGL. This component is used to embed in WPF components(usercontrol, grid,..).
	 * HwndHost does not automatically support layout changes in WPF. If the size of the UserControl changes,
	 * the OpenGL context size needs to be updated accordingly
	 */
	public class OpenGLHost : HwndHost
	{
		private IntPtr hwnd;
		private IntPtr hdc;
		private IntPtr hglrc;


		public OpenGLHost()
		{
			SizeChanged += OpenGLHost_SizeChanged;
		}


		private void OpenGLHost_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			wglMakeCurrent(hdc, hglrc);
			//GL.ClearColor(0.1f, 0.2f, 0.3f, 1.0f);
			//GL.Clear(GL.COLOR_BUFFER_BIT);

			PCBViewerAPI.Draw(IntPtr.Zero);
			SwapBuffers(hdc);
		}
		protected override HandleRef BuildWindowCore(HandleRef hwndParent)
		{
			// Tạo cửa sổ con sử dụng CreateWindowEx
			hwnd = CreateWindowEx(
				0,
				"static",
				"",
				WS_CHILD | WS_VISIBLE,
				0, 0, 100, 100,
				hwndParent.Handle,
				IntPtr.Zero,
				IntPtr.Zero,
				0);

			hdc = GetDC(hwnd);

			// Thiết lập ngữ cảnh OpenGL
			SetPixelFormat();
			hglrc = wglCreateContext(hdc);
			wglMakeCurrent(hdc, hglrc);

			// Trả về HandleRef cho HwndHost
			return new HandleRef(this, hwnd);
		}

		protected override void DestroyWindowCore(HandleRef hwnd)
		{
			wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
			wglDeleteContext(hglrc);
			ReleaseDC(hwnd.Handle, hdc);
			DestroyWindow(hwnd.Handle);
		}

		private void SetPixelFormat()
		{
			var pfd = new PIXELFORMATDESCRIPTOR
			{
				nSize = (ushort)Marshal.SizeOf(typeof(PIXELFORMATDESCRIPTOR)),
				nVersion = 1,
				dwFlags = PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER,
				iPixelType = PFD_TYPE_RGBA,
				cColorBits = 24,
				cDepthBits = 32,
				iLayerType = PFD_MAIN_PLANE
			};

			int pixelFormat = ChoosePixelFormat(hdc, ref pfd);
			SetPixelFormat(hdc, pixelFormat, ref pfd);
		}

		public void Render()
		{
			// Vẽ nội dung OpenGL
			wglMakeCurrent(hdc, hglrc);
			//GL.ClearColor(0.1f, 0.2f, 0.3f, 1.0f);
			//GL.Clear(GL.COLOR_BUFFER_BIT);

			PCBViewerAPI.Draw(IntPtr.Zero);
			SwapBuffers(hdc);
		}

		// Định nghĩa các hằng số và phương thức Win32
		private const int WS_CHILD = 0x40000000;
		private const int WS_VISIBLE = 0x10000000;
		private const int PFD_DRAW_TO_WINDOW = 0x00000004;
		private const int PFD_SUPPORT_OPENGL = 0x00000020;
		private const int PFD_DOUBLEBUFFER = 0x00000001;
		private const int PFD_TYPE_RGBA = 0;
		private const int PFD_MAIN_PLANE = 0;

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr CreateWindowEx(
			int dwExStyle, string lpClassName, string lpWindowName,
			int dwStyle, int x, int y, int nWidth, int nHeight,
			IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, int lpParam);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool DestroyWindow(IntPtr hwnd);

		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll")]
		private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("gdi32.dll")]
		private static extern int ChoosePixelFormat(IntPtr hdc, ref PIXELFORMATDESCRIPTOR ppfd);

		[DllImport("gdi32.dll")]
		private static extern bool SetPixelFormat(IntPtr hdc, int iPixelFormat, ref PIXELFORMATDESCRIPTOR ppfd);

		[DllImport("opengl32.dll")]
		private static extern IntPtr wglCreateContext(IntPtr hdc);

		[DllImport("opengl32.dll")]
		private static extern bool wglMakeCurrent(IntPtr hdc, IntPtr hglrc);

		[DllImport("opengl32.dll")]
		private static extern bool wglDeleteContext(IntPtr hglrc);

		[DllImport("gdi32.dll")]
		private static extern bool SwapBuffers(IntPtr hdc);

		[StructLayout(LayoutKind.Sequential)]
		private struct PIXELFORMATDESCRIPTOR
		{
			public ushort nSize;
			public ushort nVersion;
			public uint dwFlags;
			public byte iPixelType;
			public byte cColorBits;
			public byte cRedBits;
			public byte cRedShift;
			public byte cGreenBits;
			public byte cGreenShift;
			public byte cBlueBits;
			public byte cBlueShift;
			public byte cAlphaBits;
			public byte cAlphaShift;
			public byte cAccumBits;
			public byte cAccumRedBits;
			public byte cAccumGreenBits;
			public byte cAccumBlueBits;
			public byte cAccumAlphaBits;
			public byte cDepthBits;
			public byte cStencilBits;
			public byte cAuxBuffers;
			public byte iLayerType;
			public byte bReserved;
			public uint dwLayerMask;
			public uint dwVisibleMask;
			public uint dwDamageMask;
		}

		//private DispatcherTimer dispatcherTimer;
		//Dispatcher dispatcher;
		//public delegate void funOpenGLHostSizeChanged(int w, int h);

		//public void SetSizeChangedCallback(funOpenGLHostSizeChanged _func)
		//{
		//	funcSizeChangedCallback = _func;
		//}

		//public IntPtr Hwnd { get; set; } // Handle to the window

		//public OpenGLHost()
		//{
		//	SizeChanged += OpenGLHost_SizeChanged;
		//	//LocaltionChanged += OpenGLHost_LocaltionChanged;
		//	LayoutUpdated += HwndHost_LayoutUpdated;

		//	dispatcherTimer = new DispatcherTimer();
		//	dispatcherTimer.Interval = TimeSpan.FromMilliseconds(2);
		//	dispatcherTimer.Tick += OnTimedEvent;
		//	dispatcher = Dispatcher.CurrentDispatcher;
		//}

		//private void OnTimedEvent(object sender, EventArgs e)
		//{
		//	if (funcSizeChangedCallback != null)
		//	{
		//		funcSizeChangedCallback((int)wi, (int)he);
		//	}

		//	dispatcherTimer.Stop();
		//}

		//protected override HandleRef BuildWindowCore(HandleRef hwndParent)
		//{
		//	uint style = (WS_CHILD | WS_VISIBLE);

		//	Hwnd = CreateWindowEx(0, "STATIC", null, style,
		//						0, 0, 1920, 1768, hwndParent.Handle,
		//						IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

		//	if (Hwnd == IntPtr.Zero)
		//		throw new InvalidOperationException("Failed to create child window.");

		//	return new HandleRef(this, Hwnd);
		//}

		//protected override void DestroyWindowCore(HandleRef hwnd)
		//{
		//	if (hwnd.Handle != IntPtr.Zero)
		//	{
		//		DestroyWindow(hwnd.Handle);
		//	}
		//}

		//private void HwndHost_LayoutUpdated(object sender, EventArgs e)
		//{
		//	// Lấy kích thước mới của HwndHost

		//	//if(bCheck)
		//	{
		//		dispatcherTimer.Start();
		//	}

		//}

		//// Handle the SizeChanged event
		//private void OpenGLHost_SizeChanged(object sender, SizeChangedEventArgs e)
		//{
		//	wi = (uint)e.NewSize.Width;
		//	he= (uint)e.NewSize.Height;

		//	//if (funcSizeChangedCallback != null)
		//	//{
		//	//	funcSizeChangedCallback((int)wi, (int)he);
		//	//}

		//	//bCheck = true;

		//	e.Handled = true;
		//}

		//private bool bCheck = true;

		//private const int WM_ERASEBKGND = 0x14;

		//[DllImport("user32.dll", SetLastError = true)]
		//private static extern IntPtr CreateWindowEx(int exStyle, string lpClassName, string lpWindowName,
		//	uint dwStyle, int x, int y, int nWidth, int nHeight,
		//	IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

		//[DllImport("user32.dll", SetLastError = true)]
		//public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

		//[DllImport("user32.dll", SetLastError = true)]
		//private static extern bool DestroyWindow(IntPtr hwnd);


		//private uint wi				= 0;
		//private uint he				= 0;
		//private const uint WS_CHILD				= 0x40000000;
		//private const uint WS_POPUP				= 0x80000000;
		//private const uint WS_VISIBLE			= 0x10000000;

		//private const uint PFD_DRAW_TO_WINDOW	= 0x00000004;
		//private const uint PFD_SUPPORT_OPENGL	= 0x00000020;
		//private const uint PFD_DOUBLEBUFFER	= 0x00000001;

		//private const uint GL_COLOR_BUFFER_BIT	= 0x00004000;

		//private funOpenGLHostSizeChanged funcSizeChangedCallback = null;
	}
}
