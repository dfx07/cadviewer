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
	public class CTextBox : TextBox
	{
		static CTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CTextBox),
				new FrameworkPropertyMetadata(typeof(CTextBox)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CTextBox), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CTextBox), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty ImagePlacementProperty =
		DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CTextBox), new PropertyMetadata(ImagePlacement.Left));

		public ImagePlacement ImagePlacement
		{
			get => (ImagePlacement)GetValue(ImagePlacementProperty);
			set => SetValue(ImagePlacementProperty, value);
		}

		public static readonly DependencyProperty ImageMarginProperty =
		DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(CTextBox), new PropertyMetadata(new Thickness(1)));
		public Thickness ImageMargin
		{
			get => (Thickness)GetValue(ImageMarginProperty);
			set => SetValue(ImageMarginProperty, value);
		}

		public static readonly DependencyProperty PlaceholderProperty =
		DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(CTextBox), new PropertyMetadata(string.Empty));
		public string Placeholder
		{
			get => (string)GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}
	}
}
