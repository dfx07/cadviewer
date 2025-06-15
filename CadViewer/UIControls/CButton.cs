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

namespace CadViewer.UIControls
{
	public enum ImagePlacement
	{
		Left = 0,
		Right = 1
	}
	public class CButton : Button
	{
		static CButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CButton),
				new FrameworkPropertyMetadata(typeof(CButton)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CButton), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CButton), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty ImagePlacementProperty =
		DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CButton), new PropertyMetadata(ImagePlacement.Left));

		public ImagePlacement ImagePlacement
		{
			get => (ImagePlacement)GetValue(ImagePlacementProperty);
			set => SetValue(ImagePlacementProperty, value);
		}

		public static readonly DependencyProperty ImageMarginProperty =
		DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(CButton), new PropertyMetadata(new Thickness(1)));

		public Thickness ImageMargin
		{
			get => (Thickness)GetValue(ImageMarginProperty);
			set => SetValue(ImageMarginProperty, value);
		}
	}

	public class CImageButton : Button
	{
		static CImageButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CImageButton),
				new FrameworkPropertyMetadata(typeof(CImageButton)));
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CImageButton), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CImageButton), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty ImagePlacementProperty =
		DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CImageButton), new PropertyMetadata(ImagePlacement.Left));

		public ImagePlacement ImagePlacement
		{
			get => (ImagePlacement)GetValue(ImagePlacementProperty);
			set => SetValue(ImagePlacementProperty, value);
		}

		public static readonly DependencyProperty ImageMarginProperty =
		DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(CImageButton), new PropertyMetadata(new Thickness(1)));

		public Thickness ImageMargin
		{
			get => (Thickness)GetValue(ImageMarginProperty);
			set => SetValue(ImageMarginProperty, value);
		}

		public static readonly DependencyProperty CornerRadiusProperty =
			DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CImageButton), new PropertyMetadata(new CornerRadius(0, 0, 0, 0)));

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		public ImageSource NormalImage
		{
			get => (ImageSource)GetValue(NormalImageProperty);
			set => SetValue(NormalImageProperty, value);
		}

		public static readonly DependencyProperty NormalImageProperty =
			DependencyProperty.Register(nameof(NormalImage), typeof(ImageSource), typeof(CImageButton));

		public ImageSource HoverImage
		{
			get => (ImageSource)GetValue(HoverImageProperty);
			set => SetValue(HoverImageProperty, value);
		}

		public static readonly DependencyProperty HoverImageProperty =
			DependencyProperty.Register(nameof(HoverImage), typeof(ImageSource), typeof(CImageButton));

		public Brush HoverBackground
		{
			get => (Brush)GetValue(HoverBackgroundProperty);
			set => SetValue(HoverBackgroundProperty, value);
		}

		public static readonly DependencyProperty HoverBackgroundProperty =
			DependencyProperty.Register(nameof(HoverBackground), typeof(Brush), typeof(CImageButton));

		public Brush PressedBackground
		{
			get => (Brush)GetValue(PressedBackgroundProperty);
			set => SetValue(PressedBackgroundProperty, value);
		}

		public static readonly DependencyProperty PressedBackgroundProperty =
			DependencyProperty.Register(nameof(PressedBackground), typeof(Brush), typeof(CImageButton));
	}
}
