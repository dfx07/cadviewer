using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace CadViewer.UIControls
{
	public class OverlayLayer
	{
		private readonly Popup _popup;
		private readonly FrameworkElement _content;
		private readonly Window _ownerWindow;
		private readonly Border _overlayBorder;

		public OverlayLayer(UIElement content, Window ownerWindow)
		{
			_ownerWindow = ownerWindow;
			_content = content as FrameworkElement ?? throw new ArgumentException("Content must be FrameworkElement");

			_overlayBorder = new Border
			{
				Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)), // Semi-transparent black
				Child = _content
			};

			_popup = new Popup
			{
				AllowsTransparency = true,
				Placement = PlacementMode.Absolute,
				PopupAnimation = PopupAnimation.None,
				StaysOpen = true,
				Child = _overlayBorder,
			};

			// Hook events to track window size/location
			_ownerWindow.LocationChanged += UpdateOverlay;
			_ownerWindow.SizeChanged += UpdateOverlay;
			_ownerWindow.ContentRendered += (_, __) => UpdateOverlay(null, null);
		}

		public void Show()
		{
			_popup.IsOpen = true;
			UpdateOverlay(null, null);
		}

		public void Hide()
		{
			_popup.IsOpen = false;
		}

		private void UpdateOverlay(object sender, EventArgs e)
		{
			if (_popup == null || !_popup.IsOpen || _ownerWindow == null) return;

			// Get top-left screen position of the window
			Point position = _ownerWindow.PointToScreen(new Point(0, 0));

			var content = _ownerWindow.Content as FrameworkElement;

			_popup.HorizontalOffset = position.X;
			_popup.VerticalOffset = position.Y;

			// Set the overlay size to match the window size
			_overlayBorder.Width = content.ActualWidth;
			_overlayBorder.Height = content.ActualHeight;
		}
	}
}
