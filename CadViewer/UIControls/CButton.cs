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
}
