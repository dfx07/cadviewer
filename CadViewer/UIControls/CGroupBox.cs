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
	public class CGroupBox : GroupBox
	{
		static CGroupBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CGroupBox),
				new FrameworkPropertyMetadata(typeof(CGroupBox)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CGroupBox), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImagePlacementProperty =
		DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CGroupBox), new PropertyMetadata(ImagePlacement.Left));

		public ImagePlacement ImagePlacement
		{
			get => (ImagePlacement)GetValue(ImagePlacementProperty);
			set => SetValue(ImagePlacementProperty, value);
		}

		//public static readonly DependencyProperty TrackHeightProperty =
		//DependencyProperty.Register(nameof(TrackHeight), typeof(double), typeof(CSlider), new PropertyMetadata(5.0));

		//public double TrackHeight
		//{
		//	get => (double)GetValue(TrackHeightProperty);
		//	set => SetValue(TrackHeightProperty, value);
		//}

		//public static readonly DependencyProperty TrackRadiusProperty =
		//DependencyProperty.Register(nameof(TrackRadius), typeof(CornerRadius), typeof(CSlider), new PropertyMetadata(new CornerRadius(3)));

		//public CornerRadius TrackRadius
		//{
		//	get => (CornerRadius)GetValue(TrackRadiusProperty);
		//	set => SetValue(TrackRadiusProperty, value);
		//}

		//public static readonly DependencyProperty ShowRangeLabelProperty =
		//DependencyProperty.Register( nameof(ShowRangeLabel), typeof(bool), typeof(CSlider), new PropertyMetadata(true));

		//public bool ShowRangeLabel
		//{
		//	get => (bool)GetValue(ShowRangeLabelProperty);
		//	set => SetValue(ShowRangeLabelProperty, value);
		//}
	}
}
