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
	public class CRadioButton : RadioButton
	{
		static CRadioButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CRadioButton), new FrameworkPropertyMetadata(typeof(CRadioButton)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{
				var thumb = GetTemplateChild("PART_Thumb") as Thumb;
				var toolTip = GetTemplateChild("PART_Tooltip") as Popup;
				var valueTip = GetTemplateChild("TooltipValue") as Border;
			};
		}

		public static readonly DependencyProperty TrackHeightProperty =
		DependencyProperty.Register(nameof(TrackHeight), typeof(double), typeof(CRadioButton), new PropertyMetadata(5.0));

		public double TrackHeight
		{
			get => (double)GetValue(TrackHeightProperty);
			set => SetValue(TrackHeightProperty, value);
		}

		public static readonly DependencyProperty TrackRadiusProperty =
		DependencyProperty.Register(nameof(TrackRadius), typeof(CornerRadius), typeof(CRadioButton), new PropertyMetadata(new CornerRadius(3)));

		public CornerRadius TrackRadius
		{
			get => (CornerRadius)GetValue(TrackRadiusProperty);
			set => SetValue(TrackRadiusProperty, value);
		}

		public static readonly DependencyProperty ShowRangeLabelProperty =
		DependencyProperty.Register( nameof(ShowRangeLabel), typeof(bool), typeof(CRadioButton), new PropertyMetadata(true));

		public bool ShowRangeLabel
		{
			get => (bool)GetValue(ShowRangeLabelProperty);
			set => SetValue(ShowRangeLabelProperty, value);
		}
	}
}
