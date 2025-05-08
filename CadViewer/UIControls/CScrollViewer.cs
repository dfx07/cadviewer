using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using CadViewer.Animations;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Threading;

namespace CadViewer.UIControls
{
	public class CScrollViewer : ScrollViewer
	{
		private double _targetOffset;
		private double _animationSpeed = 0.3;
		private bool _isAnimating;

		static CScrollViewer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CScrollViewer),
				new FrameworkPropertyMetadata(typeof(CScrollViewer)));
		}

		public CScrollViewer()
		{
			PreviewMouseWheel += OnPreviewMouseWheel;
			CompositionTarget.Rendering += AnimateScroll;
		}

		private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			e.Handled = true;
			_targetOffset = Math.Max(0, Math.Min(this.ScrollableHeight, _targetOffset - e.Delta));
			_isAnimating = true;
		}

		private void AnimateScroll(object sender, EventArgs e)
		{
			if (!_isAnimating) return;

			double currentOffset = this.VerticalOffset;
			double delta = _targetOffset - currentOffset;

			if (Math.Abs(delta) < 0.5)
			{
				this.ScrollToVerticalOffset(_targetOffset);
				_isAnimating = false;
				return;
			}

			this.ScrollToVerticalOffset(currentOffset + delta * _animationSpeed);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			var hScrollBar = GetTemplateChild("PART_HorizontalScrollBar") as ScrollBar;
			Debug.WriteLine($"Horizontal ScrollBar: {(hScrollBar != null ? "Found" : "Missing")}");

			Loaded += (s, e) =>
			{

			};
		}

		//public static readonly DependencyProperty ImageSourceProperty =
		//DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CScrollViewer), new PropertyMetadata(null));

		//public ImageSource ImageSource
		//{
		//	get => (ImageSource)GetValue(ImageSourceProperty);
		//	set => SetValue(ImageSourceProperty, value);
		//}

		//public static readonly DependencyProperty ImagePlacementProperty =
		//DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CScrollViewer), new PropertyMetadata(ImagePlacement.Left));

		//public ImagePlacement ImagePlacement
		//{
		//	get => (ImagePlacement)GetValue(ImagePlacementProperty);
		//	set => SetValue(ImagePlacementProperty, value);
		//}

		//public static readonly DependencyProperty HeaderHeightProperty =
		//DependencyProperty.Register(nameof(HeaderHeight), typeof(double), typeof(CScrollViewer), new PropertyMetadata(20.0));

		//public double HeaderHeight
		//{
		//	get => (double)GetValue(HeaderHeightProperty);
		//	set => SetValue(HeaderHeightProperty, value);
		//}

		//public static readonly DependencyProperty ImageWidthProperty =
		//DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CScrollViewer), new PropertyMetadata(double.NaN));
		//public double ImageWidth
		//{
		//	get => (double)GetValue(ImageWidthProperty);
		//	set => SetValue(ImageWidthProperty, value);
		//}

		//public static readonly DependencyProperty ShowHeaderProperty =
		//DependencyProperty.Register(nameof(ShowHeader), typeof(bool), typeof(CScrollViewer), new PropertyMetadata(true));

		//public bool ShowHeader
		//{
		//	get => (bool)GetValue(ShowHeaderProperty);
		//	set => SetValue(ShowHeaderProperty, value);
		//}
	}
}
