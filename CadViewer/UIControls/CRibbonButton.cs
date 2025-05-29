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

namespace CadViewer.UIControls
{
	public class CRibbonButton : ToggleButton
	{
		static CRibbonButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CRibbonButton),
				new FrameworkPropertyMetadata(typeof(CRibbonButton)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}

		// Only support normal button style
		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CRibbonButton), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CRibbonButton), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty OnTextProperty =
		DependencyProperty.Register(nameof(OnText), typeof(string), typeof(CRibbonButton), new PropertyMetadata("On"));

		public string OnText
		{
			get => (string)GetValue(OnTextProperty);
			set => SetValue(OnTextProperty, value);
		}

		public static readonly DependencyProperty OffTextProperty =
		DependencyProperty.Register(nameof(OffText), typeof(string), typeof(CRibbonButton), new PropertyMetadata("Off"));

		public string OffText
		{
			get => (string)GetValue(OffTextProperty);
			set => SetValue(OffTextProperty, value);
		}
	}
}
