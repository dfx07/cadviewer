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
	public class CSlider : Slider
	{
		static CSlider()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CSlider),
				new FrameworkPropertyMetadata(typeof(CSlider)));
		}

		Thumb _Thumb = null;
		Popup _Tooltip = null;
		Border _ValueTip = null;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{
				var thumb = GetTemplateChild("PART_Thumb") as Thumb;
				var toolTip = GetTemplateChild("PART_Tooltip") as Popup;
				var valueTip = GetTemplateChild("TooltipValue") as Border;

				if (thumb != null)
				{
					thumb.DragStarted += Thumb_DragStarted;
					thumb.DragDelta += Thumb_DragDelta;
					thumb.DragCompleted += Thumb_DragCompleted;
					thumb.MouseEnter += Thumb_MouseEnter;
					thumb.MouseLeave += Thumb_MouseLeave;

					_Thumb = thumb;
					_Tooltip = toolTip;
					_ValueTip = valueTip;
				}
			};
		}
		private void RecalTooltip()
		{
			if (_Thumb == null || _Tooltip == null)
				return;

			Dispatcher.BeginInvoke(new Action(() =>
			{
				double offsetX = (_Thumb.ActualWidth - _ValueTip.ActualWidth) / 2;

				_Tooltip.HorizontalOffset = offsetX - 0.1;
				_Tooltip.HorizontalOffset = offsetX;

			}), System.Windows.Threading.DispatcherPriority.Loaded);
		}

		private void Thumb_MouseLeave(object sender, MouseEventArgs e)
		{
			if(_Tooltip.IsOpen)
				_Tooltip.IsOpen = false;
		}

		private async void Thumb_MouseEnter(object sender, MouseEventArgs e)
		{
			await Task.Delay(500);

			if(!_Tooltip.IsOpen)
			{
				RecalTooltip();
				_Tooltip.IsOpen = true;
			}
		}

		private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
		{
			if (!_Tooltip.IsOpen)
				_Tooltip.IsOpen = true;

			RecalTooltip();
		}

		private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if (!_Tooltip.IsOpen)
				_Tooltip.IsOpen = true;

			RecalTooltip();
		}

		private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			if (_Tooltip.IsOpen)
				_Tooltip.IsOpen = false;
		}

		public static readonly DependencyProperty TrackHeightProperty =
		DependencyProperty.Register(nameof(TrackHeight), typeof(double), typeof(CSlider), new PropertyMetadata(5.0));

		public double TrackHeight
		{
			get => (double)GetValue(TrackHeightProperty);
			set => SetValue(TrackHeightProperty, value);
		}

		public static readonly DependencyProperty TrackRadiusProperty =
		DependencyProperty.Register(nameof(TrackRadius), typeof(CornerRadius), typeof(CSlider), new PropertyMetadata(new CornerRadius(3)));

		public CornerRadius TrackRadius
		{
			get => (CornerRadius)GetValue(TrackRadiusProperty);
			set => SetValue(TrackRadiusProperty, value);
		}

		public static readonly DependencyProperty ShowRangeLabelProperty =
		DependencyProperty.Register( nameof(ShowRangeLabel), typeof(bool), typeof(CSlider), new PropertyMetadata(true));

		public bool ShowRangeLabel
		{
			get => (bool)GetValue(ShowRangeLabelProperty);
			set => SetValue(ShowRangeLabelProperty, value);
		}
	}
}
