using CadViewer.Common;
using CadViewer.Interfaces;
using CadViewer.View;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;

namespace CadViewer.Components
{
	public class ViewPanel : Control
	{
		private IntPtr _deviceContext = IntPtr.Zero;
		private IntPtr _renderContext = IntPtr.Zero;

		public IWinformViewCtrlEventListener ViewControl { get; set; } = null;

		public ViewPanel()
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint |
						  ControlStyles.Opaque |
						  ControlStyles.UserPaint, true);

			this.SetStyle(ControlStyles.Selectable, true);
			this.TabStop = true;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_OnCreated(this, this.Handle);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_OnViewUpdate(this);
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_MouseMove(this, e);
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_MouseDown(this, e);
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_MouseUp(this, e);
		}

		protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_MouseWheel(this, e);
		}

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_KeyDown(this, e);
		}

		protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_KeyUp(this, e);
		}
		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_SizeChanged(this, new System.Windows.Size(ClientSize.Width, ClientSize.Height));
		}

		// Định nghĩa các hằng số OpenGL
		private const int PFD_DRAW_TO_WINDOW = 0x00000004;
		private const int PFD_SUPPORT_OPENGL = 0x00000020;
		private const int PFD_DOUBLEBUFFER = 0x00000001;
		private const int PFD_TYPE_RGBA = 0;
		private const int PFD_MAIN_PLANE = 0;
		private const int GL_COLOR_BUFFER_BIT = 0x4000;
		private const int GL_DEPTH_BUFFER_BIT = 0x0100;

		// Các hàm OpenGL
		[DllImport("opengl32.dll")]
		private static extern void glClearColor(float red, float green, float blue, float alpha);

		[DllImport("opengl32.dll")]
		private static extern void glClear(int mask);

		[DllImport("opengl32.dll")]
		private static extern void glBegin(int mode);

		[DllImport("opengl32.dll")]
		private static extern void glEnd();

		[DllImport("opengl32.dll")]
		private static extern void glColor3f(float red, float green, float blue);

		[DllImport("opengl32.dll")]
		private static extern void glVertex2f(float x, float y);

		[DllImport("gdi32.dll")]
		private static extern int ChoosePixelFormat(IntPtr hdc, [In] ref PIXELFORMATDESCRIPTOR ppfd);

		[DllImport("gdi32.dll")]
		private static extern int SetPixelFormat(IntPtr hdc, int format, [In] ref PIXELFORMATDESCRIPTOR ppfd);

		[DllImport("gdi32.dll")]
		private static extern bool SwapBuffers(IntPtr hdc);

		[DllImport("opengl32.dll")]
		private static extern IntPtr wglCreateContext(IntPtr hdc);

		[DllImport("opengl32.dll")]
		private static extern bool wglMakeCurrent(IntPtr hdc, IntPtr hglrc);

		[DllImport("opengl32.dll")]
		private static extern bool wglDeleteContext(IntPtr hglrc);

		[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport("user32.dll")]
		private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

		// Định nghĩa PIXELFORMATDESCRIPTOR
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
	}
}
