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
using CadViewer.ViewModels;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CadViewer.UIControls
{
	public class OverlayWindow : Window
	{
		protected readonly Grid _container;
		private readonly Stack<UIElement> _viewStack = new Stack<UIElement>();

		public OverlayWindow(Window owner)
		{
			Owner = owner;
			WindowStyle = WindowStyle.None;
			ResizeMode = ResizeMode.NoResize;
			ShowInTaskbar = false;

			_container = new Grid
			{
				Background = new SolidColorBrush(Color.FromArgb(255, 243, 243, 243)),
				HorizontalAlignment = HorizontalAlignment.Stretch,
				VerticalAlignment = VerticalAlignment.Stretch
			};

			Content = _container;

			Loaded += (_, __) =>
			{
				CompositionTarget.Rendering += OnRendering;
			};

			Closed += (_, __) =>
			{
				CompositionTarget.Rendering -= OnRendering;
			};
		}

		public void SetOverlayBackground(Brush background)
		{
			if (background == null)
				throw new ArgumentNullException(nameof(background));

			_container.Background = background;
		}

		public void SetOverlayContent(object content)
		{
			if (content == null)
				throw new ArgumentNullException(nameof(content));

			_container.Children.Clear();

			var bitmap = CaptureGridToBitmap(Owner.Content as FrameworkElement);

			_container.Background = new ImageBrush(bitmap)
			{
				Stretch = Stretch.Fill,
				AlignmentX = AlignmentX.Left,
				AlignmentY = AlignmentY.Top
			};

			if (content is ViewModelBase)
			{
				var viewModel = content as ViewModelBase;

				var contentControl = new ContentControl
				{
					Content = viewModel
				};

				_container.Children.Add(contentControl);
			}
			else
			{
				if (content is UIElement uiElement)
				{
					_container.Children.Add(uiElement);
				}
				else
				{
					throw new ArgumentException("Content must be a UIElement or ViewModelBase", nameof(content));
				}
			}

			UpdateOverlayBounds();


		}

		private void UpdateOverlayBounds()
		{
			if (Owner.WindowState == WindowState.Minimized || !this.IsVisible)
				return;

			if (Owner == null)
				return;

			var pos = Owner.PointToScreen(new Point(0, 0));
			var content = Owner.Content as FrameworkElement;

			if (Left != pos.X || Top != pos.Y ||
				Width != content.ActualWidth || Height != content.ActualHeight)
			{
				Left = pos.X ;
				Top = pos.Y;
				Width = content.ActualWidth;
				Height = content.ActualHeight;
			}
		}

		public static RenderTargetBitmap CaptureGridToBitmap(FrameworkElement element)
		{
			if (element == null)
				return null;

			int width = (int)element.ActualWidth;
			int height = (int)element.ActualHeight;

			if (width == 0 || height == 0)
				return null;

			var dpi = 96;

			var rtb = new RenderTargetBitmap(width, height, dpi, dpi, PixelFormats.Pbgra32);
			rtb.Render(element);
			return rtb;
		}

		private void OnRendering(object sender, EventArgs e)
		{
			if (Owner.WindowState == WindowState.Minimized || !this.IsVisible)
				return;

			if (Owner == null)
				return;

			var pos = Owner.PointToScreen(new Point(0, 0));
			var content = Owner.Content as FrameworkElement;

			if (Left != pos.X || Top != pos.Y ||
				Width != content.ActualWidth || Height != content.ActualHeight)
			{
				Left = pos.X;
				Top = pos.Y;
				Width = content.ActualWidth;
				Height = content.ActualHeight;
			}
		}

		public void Push(UIElement view)
		{
			//if (!IsVisible)
			//{
			//	SyncWithOwner(null, null);
			//	Show();
			//}

			//_viewStack.Push(view);
			//_container.Children.Add(view);
		}

		public void Pop()
		{
			//if (_viewStack.Count > 0)
			//{
			//	var top = _viewStack.Pop();
			//	_container.Children.Remove(top);
			//}

			//if (_viewStack.Count == 0)
			//{
			//	Hide();
			//}
		}

		public void Clear()
		{
			//_viewStack.Clear();
			//_container.Children.Clear();
			//Hide();
		}
	}

	public class OverlayWindowTransparent : OverlayWindow
	{
		public OverlayWindowTransparent(Window owner):
			base(owner)
		{
			Background = Brushes.Transparent;
			AllowsTransparency = true;
			CacheMode = new BitmapCache();

			if (_container == null)
				throw new InvalidOperationException("Container is not initialized.");

			_container.Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0));
		}
	}
}
