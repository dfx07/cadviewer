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
	public enum OverlayWindowType
	{
		Default,            // Default background color
		Transparent,        // Transparent background
		Backdrop            // Background image of the owner window
	}

	public class OverlayWindow : Window
	{
		protected readonly Grid _container;
		private readonly Stack<UIElement> _viewStack = new Stack<UIElement>();

		public OverlayWindowType OverlayType { get; set; } = OverlayWindowType.Default;
		public Brush OverlayBackground = null;

		public OverlayWindow(Window owner, OverlayWindowType type)
		{
			Owner = owner;
			WindowStyle = WindowStyle.None;
			ResizeMode = ResizeMode.NoResize;
			ShowInTaskbar = false;

			OverlayType = type;

			if(OverlayType == OverlayWindowType.Default)
			{
				OverlayBackground = new SolidColorBrush(Color.FromArgb(255, 243, 243, 243));
			}
			else if(OverlayType == OverlayWindowType.Transparent)
			{
				AllowsTransparency = true;
				OverlayBackground = Brushes.Transparent;
			}
			else if(OverlayType == OverlayWindowType.Backdrop)
			{
				OverlayBackground = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0));
			}

			_container = new Grid
			{
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

			OverlayBackground = background;
		}

		public void SetOverlayContent(object content)
		{
			if (content == null)
				throw new ArgumentNullException(nameof(content));

			_container.Children.Clear();

			if(OverlayWindowType.Transparent == OverlayType ||
				OverlayWindowType.Default == OverlayType)
			{
				if(OverlayBackground != _container.Background)
					_container.Background = OverlayBackground;
			}
			else if (OverlayWindowType.Backdrop == OverlayType)
			{
				var bitmap = CaptureGridToBitmap(Owner.Content as FrameworkElement);

				_container.Background = new ImageBrush(bitmap)
				{
					Stretch = Stretch.Fill,
					AlignmentX = AlignmentX.Left,
					AlignmentY = AlignmentY.Top
				};

				_container.Children.Add(new Grid
				{
					Background = OverlayBackground,
					HorizontalAlignment = HorizontalAlignment.Stretch,
					VerticalAlignment = VerticalAlignment.Stretch
				});
			}

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
		}

		public void UpdateBackdropImage()
		{
			if (OverlayType != OverlayWindowType.Backdrop || Owner == null)
				return;

			var bitmap = CaptureGridToBitmap(Owner.Content as FrameworkElement);
			if (bitmap != null)
			{
				_container.Background = new ImageBrush(bitmap)
				{
					Stretch = Stretch.Fill,
					AlignmentX = AlignmentX.Left,
					AlignmentY = AlignmentY.Top
				};
			}
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

			var oldMode = RenderOptions.GetBitmapScalingMode(element);
			RenderOptions.SetBitmapScalingMode(element, BitmapScalingMode.HighQuality);

			rtb.Render(element);

			RenderOptions.SetBitmapScalingMode(element, oldMode);

			return rtb;
		}

		private void OnRendering(object sender, EventArgs e)
		{
			UpdateOverlayBounds();
		}

		public void DoShow()
		{
			if (Owner == null)
				throw new InvalidOperationException("Owner window must be set before showing the overlay.");

			base.Show();
		}

		public void DoHide()
		{
			if (Owner == null)
				throw new InvalidOperationException("Owner window must be set before hiding the overlay.");

			base.Hide();
		}
	}
}
