using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace CadViewer.Components
{
	/***
	 * Create window handle for OpenGL. This component is used to embed in WPF components(usercontrol, grid,..).
	 * HwndHost does not automatically support layout changes in WPF. If the size of the UserControl changes,
	 * the OpenGL context size needs to be updated accordingly
	 */
	public class OpenGLHost : HwndHost
	{
		public IntPtr Hwnd { get; set; } // Handle to the window

		public OpenGLHost()
		{

		}
		protected override HandleRef BuildWindowCore(HandleRef hwndParent)
		{
			Hwnd = CreateWindowEx(0, "STATIC", null, WS_CHILD | WS_VISIBLE,
								0, 0, 100, 100, hwndParent.Handle,
								IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

			if (Hwnd == IntPtr.Zero)
				throw new InvalidOperationException("Failed to create child window.");

			return new HandleRef(this, Hwnd);
		}

		protected override void DestroyWindowCore(HandleRef hwnd)
		{
			if (hwnd.Handle != IntPtr.Zero)
			{
				DestroyWindow(hwnd.Handle);
			}
		}

		// Windows API declarations
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr CreateWindowEx(int exStyle, string lpClassName, string lpWindowName,
			int dwStyle, int x, int y, int nWidth, int nHeight,
			IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool DestroyWindow(IntPtr hwnd);

		private const int WS_CHILD				= 0x40000000;
		private const int WS_VISIBLE			= 0x10000000;

		private const int PFD_DRAW_TO_WINDOW	= 0x00000004;
		private const int PFD_SUPPORT_OPENGL	= 0x00000020;
		private const int PFD_DOUBLEBUFFER		= 0x00000001;

		private const uint GL_COLOR_BUFFER_BIT	= 0x00004000;
	}
}
