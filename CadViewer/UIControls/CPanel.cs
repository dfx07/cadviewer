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
	public enum EPanelHeaderDirection
	{
		Left,
		Right,
		Top,
		Bottom
	}

	public class CPanel : ContentControl
	{
		static CPanel()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CPanel), new FrameworkPropertyMetadata(typeof(CPanel)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty HeaderContentProperty =
		DependencyProperty.Register(nameof(HeaderContent), typeof(object), typeof(CPanel), null);

		public object HeaderContent
		{
			get => GetValue(HeaderContentProperty);
			set => SetValue(HeaderContentProperty, value);
		}

		public static readonly DependencyProperty HeaderDirectionProperty =
		DependencyProperty.Register(nameof(HeaderDirection), typeof(EPanelHeaderDirection), typeof(CPanel), new PropertyMetadata(EPanelHeaderDirection.Top));

		public EPanelHeaderDirection HeaderDirection
		{
			get => (EPanelHeaderDirection)GetValue(HeaderDirectionProperty);
			set => SetValue(HeaderDirectionProperty, value);
		}

		public static readonly DependencyProperty HeaderHeightProperty =
		DependencyProperty.Register(nameof(HeaderHeight), typeof(double), typeof(CPanel), new PropertyMetadata(25.0));

		public double HeaderHeight
		{
			get => (double)GetValue(HeaderHeightProperty);
			set => SetValue(HeaderHeightProperty, value);
		}

		//public static readonly DependencyProperty ImageSourceProperty =
		//DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CPanel), new PropertyMetadata(null));

		//public ImageSource ImageSource
		//{
		//	get => (ImageSource)GetValue(ImageSourceProperty);
		//	set => SetValue(ImageSourceProperty, value);
		//}

		//public static readonly DependencyProperty ImagePlacementProperty =
		//DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CPanel), new PropertyMetadata(ImagePlacement.Left));

		//public ImagePlacement ImagePlacement
		//{
		//	get => (ImagePlacement)GetValue(ImagePlacementProperty);
		//	set => SetValue(ImagePlacementProperty, value);
		//}

		//public static readonly DependencyProperty HeaderHeightProperty =
		//DependencyProperty.Register(nameof(HeaderHeight), typeof(double), typeof(CPanel), new PropertyMetadata(20.0));

		//public double HeaderHeight
		//{
		//	get => (double)GetValue(HeaderHeightProperty);
		//	set => SetValue(HeaderHeightProperty, value);
		//}

		//public static readonly DependencyProperty ImageWidthProperty =
		//DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CPanel), new PropertyMetadata(double.NaN));
		//public double ImageWidth
		//{
		//	get => (double)GetValue(ImageWidthProperty);
		//	set => SetValue(ImageWidthProperty, value);
		//}

		//public static readonly DependencyProperty ShowHeaderProperty =
		//DependencyProperty.Register(nameof(ShowHeader), typeof(bool), typeof(CPanel), new PropertyMetadata(true));

		//public bool ShowHeader
		//{
		//	get => (bool)GetValue(ShowHeaderProperty);
		//	set => SetValue(ShowHeaderProperty, value);
		//}
	}
}
