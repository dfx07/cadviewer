using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace CadViewer.UIControls
{
	public class OverlayWindow : Window
	{
		private readonly Grid _container;
		private readonly Stack<UIElement> _viewStack = new Stack<UIElement>();

		public OverlayWindow(Window owner)
		{
			Owner = owner;
			WindowStyle = WindowStyle.None;
			AllowsTransparency = true;
			Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)); // semi-transparent
			ShowInTaskbar = false;

			_container = new Grid();
			Content = _container;

			//owner.LocationChanged += SyncWithOwner;
			//owner.SizeChanged += SyncWithOwner;
			//owner.StateChanged += SyncWithOwner;
			//Loaded += (_, __) => SyncWithOwner(null, null);

			Loaded += (_, __) =>
			{
				CompositionTarget.Rendering += SyncWithOwnerOnRender;
			};
			Closed += (_, __) => CompositionTarget.Rendering -= SyncWithOwnerOnRender;
		}

		private void SyncWithOwnerOnRender(object sender, EventArgs e)
		{
			if (Owner.WindowState == WindowState.Minimized || !this.IsVisible)
				return;

			//var rect = DwmHelper.GetExtendedFrameBounds(_owner);

			//if (this.Left != rect.Left || this.Top != rect.Top ||
			//	this.Width != rect.Width || this.Height != rect.Height)
			//{
			//	this.Left = rect.Left;
			//	this.Top = rect.Top;
			//	this.Width = rect.Width;
			//	this.Height = rect.Height;
			//}

			if (Owner == null) return;

			var pos = Owner.PointToScreen(new Point(0, 0));

			Rect bounds = GetExtendedFrameBounds(Owner);

			var content = Owner.Content as FrameworkElement;


			if (this.Left != pos.X || this.Top != pos.Y ||
				this.Width != content.Width || this.Height != content.Height)
			{
				Left = pos.X;
				Top = pos.Y;
				Width = content.ActualWidth;
				Height = content.ActualHeight;
			}
		}

		public void Push(UIElement view)
		{
			if (!IsVisible)
			{
				SyncWithOwner(null, null);
				Show();
			}

			_viewStack.Push(view);
			_container.Children.Add(view);
		}

		public void Pop()
		{
			if (_viewStack.Count > 0)
			{
				var top = _viewStack.Pop();
				_container.Children.Remove(top);
			}

			if (_viewStack.Count == 0)
			{
				Hide();
			}
		}

		public void Clear()
		{
			_viewStack.Clear();
			_container.Children.Clear();
			Hide();
		}

		private void SyncWithOwner(object sender, EventArgs e)
		{
			if (Owner == null) return;

			var pos = Owner.PointToScreen(new Point(0, 0));

			Rect bounds = GetExtendedFrameBounds(Owner);

			var content = Owner.Content as FrameworkElement;

			Left = pos.X;
			Top = pos.Y;
			Width = content.ActualWidth;
			Height = content.ActualHeight;
		}



		[DllImport("dwmapi.dll")]
		private static extern int DwmGetWindowAttribute(
			IntPtr hwnd,
			int dwAttribute,
			out RECT pvAttribute,
			int cbAttribute);

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}


		private const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;
		public static Rect GetExtendedFrameBounds(Window window)
		{
			var hwnd = new WindowInteropHelper(window).Handle;

			var result = DwmGetWindowAttribute(hwnd, DWMWA_EXTENDED_FRAME_BOUNDS, out RECT rect, Marshal.SizeOf(typeof(RECT)));

			if (result != 0)
			{
				throw new InvalidOperationException("DwmGetWindowAttribute failed with result " + result);
			}

			return new Rect(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
		}

	}
}
