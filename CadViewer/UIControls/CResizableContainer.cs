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
	[Flags]
	public enum ResizeDirection
	{
		None = 0,
		Left = 1,
		Right = 2,
		Top = 4,
		Bottom = 8,
		All = Left | Right | Top | Bottom
	}

	public class CResizableContainer : ContentControl
	{
		static CResizableContainer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CResizableContainer),
				new FrameworkPropertyMetadata(typeof(CResizableContainer)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			HookThumb("PART_ResizeLeftThumb", ResizeDirection.Left, (dx, dy) =>
			{
				double newWidth = Width - dx;
				Width = Clamp(newWidth, MinWidth, MaxWidth);
			});
			HookThumb("PART_ResizeRightThumb", ResizeDirection.Right, (dx, dy) =>
			{
				double newWidth = Width + dx;
				Width = Clamp(newWidth, MinWidth, MaxWidth);
			});
			HookThumb("PART_ResizeTopThumb", ResizeDirection.Top, (dx, dy) =>
			{
				double newHeight = Height - dy;
				Height = Clamp(newHeight, MinHeight, MaxHeight);
			});
			HookThumb("PART_ResizeBottomThumb", ResizeDirection.Bottom, (dx, dy) =>
			{
				double newHeight = Height + dy;
				Height = Clamp(newHeight, MinHeight, MaxHeight);
			});

			Loaded += (s, e) =>
			{

			};

			if(GetTemplateChild("PART_Content") is ContentPresenter ctn)
			{
				var newMargin = new Thickness();
				if(ResizeDirections.HasFlag(ResizeDirection.Left))
				{
					newMargin.Left = 5;
				}
				if (ResizeDirections.HasFlag(ResizeDirection.Right))
				{
					newMargin.Right = 5;
				}
				if (ResizeDirections.HasFlag(ResizeDirection.Top))
				{
					newMargin.Top = 5;
				}
				if (ResizeDirections.HasFlag(ResizeDirection.Bottom))
				{
					newMargin.Bottom = 5;
				}

				ctn.Margin = newMargin;
			}
		}
		private double Clamp(double value, double min, double max)
		{
			return Math.Max(min, Math.Min(max, value));
		}

		private void HookThumb(string partName, ResizeDirection dir, Action<double, double> resizeAction)
		{
			if (ResizeDirections.HasFlag(dir) &&
				GetTemplateChild(partName) is Thumb thumb)
			{
				thumb.Visibility = Visibility.Visible;
				thumb.DragDelta += (s, e) => resizeAction(e.HorizontalChange, e.VerticalChange);
			}
			else if (GetTemplateChild(partName) is Thumb thumbHidden)
			{
				thumbHidden.Visibility = Visibility.Collapsed;
			}
		}

		public static readonly DependencyProperty ResizeDirectionsProperty =
		DependencyProperty.Register(nameof(ResizeDirections), typeof(ResizeDirection), typeof(CResizableContainer), new PropertyMetadata(ResizeDirection.All));

		public ResizeDirection ResizeDirections
		{
			get => (ResizeDirection)GetValue(ResizeDirectionsProperty);
			set => SetValue(ResizeDirectionsProperty, value);
		}

		public static readonly DependencyProperty CornerRadiusProperty =
		DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CResizableContainer), new PropertyMetadata(new CornerRadius(0, 0, 0, 0)));

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		//public static readonly DependencyProperty ImageSourceProperty =
		//DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CResizableContainer), new PropertyMetadata(null));

		//public ImageSource ImageSource
		//{
		//	get => (ImageSource)GetValue(ImageSourceProperty);
		//	set => SetValue(ImageSourceProperty, value);
		//}

		//public static readonly DependencyProperty ImagePlacementProperty =
		//DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CResizableContainer), new PropertyMetadata(ImagePlacement.Left));

		//public ImagePlacement ImagePlacement
		//{
		//	get => (ImagePlacement)GetValue(ImagePlacementProperty);
		//	set => SetValue(ImagePlacementProperty, value);
		//}

		//public static readonly DependencyProperty HeaderHeightProperty =
		//DependencyProperty.Register(nameof(HeaderHeight), typeof(double), typeof(CResizableContainer), new PropertyMetadata(20.0));

		//public double HeaderHeight
		//{
		//	get => (double)GetValue(HeaderHeightProperty);
		//	set => SetValue(HeaderHeightProperty, value);
		//}

		//public static readonly DependencyProperty ImageWidthProperty =
		//DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CResizableContainer), new PropertyMetadata(double.NaN));
		//public double ImageWidth
		//{
		//	get => (double)GetValue(ImageWidthProperty);
		//	set => SetValue(ImageWidthProperty, value);
		//}

		//public static readonly DependencyProperty ShowHeaderProperty =
		//DependencyProperty.Register(nameof(ShowHeader), typeof(bool), typeof(CResizableContainer), new PropertyMetadata(true));

		//public bool ShowHeader
		//{
		//	get => (bool)GetValue(ShowHeaderProperty);
		//	set => SetValue(ShowHeaderProperty, value);
		//}
	}
}
