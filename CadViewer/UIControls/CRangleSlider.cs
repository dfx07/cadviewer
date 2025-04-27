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
	public class CRangeSlider : RangeBase
	{
		static CRangeSlider()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CRangeSlider),
				new FrameworkPropertyMetadata(typeof(CRangeSlider)));
		}

		Thumb _ThumbLower = null;
		Popup _TooltipLower = null;
		Popup _Tooltip = null;
		Border _LowerValueTip = null;
		Border _BorderValueTip = null;
		TextBlock _ValueTip = null;

		TranslateTransform _LowerTranslateTranform = null;

		Thumb _ThumbUpper = null;
		Popup _TooltipUpper = null;
		Border _UpperValueTip = null;

		TranslateTransform _UpperTranslateTranform = null;

		Border _SelectedRangle = null;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{
				var thumb1 = GetTemplateChild("PART_LowerThumb") as Thumb;
				var toolTip = GetTemplateChild("xCRangeSliderTooltip") as Popup;
				var valueTip = GetTemplateChild("BorderValueTip") as Border;
				var valueTip1 = GetTemplateChild("ValueTip") as TextBlock;

				var thumb2 = GetTemplateChild("PART_UpperThumb") as Thumb;
				var toolTip2 = GetTemplateChild("PART_TooltipUpper") as Popup;
				var valueTip2 = GetTemplateChild("UpperValueTip") as Border;

				var tran1 = GetTemplateChild("LowerThumbTranslate") as TranslateTransform;
				var tran2 = GetTemplateChild("UpperThumbTranslate") as TranslateTransform;

				var selectedrangle = GetTemplateChild("SelectedRange") as Border;

				if (thumb1 != null)
				{
					thumb1.DragStarted += ThumbLower_DragStarted;
					thumb1.DragDelta += Thumb_DragDelta;
					thumb1.DragCompleted += ThumbLower_DragCompleted;
					thumb1.MouseEnter += Thumb_MouseEnter;
					thumb1.MouseLeave += Thumb_MouseLeave;

					_ThumbLower = thumb1;
					_Tooltip = toolTip;
					_BorderValueTip = valueTip;
					_ValueTip = valueTip1;

					_LowerTranslateTranform = tran1;
				}

				if (thumb2 != null)
				{
					thumb2.DragStarted += ThumbUpper_DragStarted;
					thumb2.DragDelta += Thumb_DragDelta;
					thumb2.DragCompleted += ThumbUpper_DragCompleted;
					thumb2.MouseEnter += Thumb_MouseEnter;
					thumb2.MouseLeave += Thumb_MouseLeave;

					_ThumbUpper = thumb2;

					_UpperTranslateTranform = tran2;
				}

				_SelectedRangle = selectedrangle;

				UpdateThumbsPosition(false);
				UpdateThumbsPosition(true);

				UpdateSelectedRange();
			};
		}

		private void RecalTooltipLower()
		{
			if (_ThumbLower == null || _TooltipLower == null)
				return;

			_TooltipLower.Dispatcher.BeginInvoke(new Action(() =>
			{
				double offsetX = (_ThumbLower.ActualWidth - _LowerValueTip.ActualWidth) / 2;

				_TooltipLower.HorizontalOffset = offsetX - 0.1;
				_TooltipLower.HorizontalOffset = offsetX;

			}), System.Windows.Threading.DispatcherPriority.Loaded);
		}

		private void SetShowValue(bool lower)
		{
			if (lower)
			{
				_Tooltip.PlacementTarget = _ThumbLower;

				_ValueTip.Text = LowerValue.ToString("F0");
			}
			else
			{
				_Tooltip.PlacementTarget = _ThumbUpper;

				_ValueTip.Text = UpperValue.ToString("F0");
			}
		}

		private void UpdateThumbsPosition(bool lower)
		{
			var TrackLength = ActualWidth - _ThumbLower.ActualWidth;

			if (lower)
			{
				if(LowerValue > UpperValue)
				{
					LowerValue = UpperValue;
				}

				double percent = (LowerValue - Minimum) / (Maximum - Minimum);

				if (percent > 1.0)
					percent = 1.0;

				_LowerTranslateTranform.X = percent * TrackLength;
			}
			else
			{
				if (UpperValue < LowerValue)
				{
					UpperValue = LowerValue;
				}

				double percent = (UpperValue - Minimum) / (Maximum - Minimum);

				if (percent > 1.0)
					percent = 1.0;

				_UpperTranslateTranform.X = percent * TrackLength;
			}

			UpdateSelectedRange();
		}

		private void UpdateSelectedRange()
		{
			if (_SelectedRangle != null)
			{
				Dispatcher.BeginInvoke(new Action(() =>
				{
					double left = _LowerTranslateTranform.X + _ThumbUpper.ActualWidth / 2;
					double right = _UpperTranslateTranform.X + _ThumbUpper.ActualWidth / 2;
					double width = right - left;

					Canvas.SetLeft(_SelectedRangle, left);
					_SelectedRangle.Width = width;

				}), System.Windows.Threading.DispatcherPriority.Loaded);
			}
		}

		// Lower
		private void ThumbLower_MouseLeave(object sender, MouseEventArgs e)
		{
			//if (_TooltipLower.IsOpen)
			//	_TooltipLower.IsOpen = false;
		}
		private async void ThumbLower_MouseEnter(object sender, MouseEventArgs e)
		{
			await Task.Delay(500);

			//if (!_TooltipLower.IsOpen)
			//{
			//	_TooltipLower.IsOpen = true;
			//}
		}

		private void ThumbLower_DragStarted(object sender, DragStartedEventArgs e)
		{
			//if (!_TooltipLower.IsOpen)
			//	_TooltipLower.IsOpen = true;

			//UpdateThumbsPosition();

			//_TooltipLower.CaptureMouse();
		}

		private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if(sender is Thumb thumb)
			{
				var TrackLength = ActualWidth - thumb.ActualWidth;

				double left = 0.0;

				if (thumb == _ThumbLower)
				{
					left = _LowerTranslateTranform.X + e.HorizontalChange;
				}
				else
				{
					left = _UpperTranslateTranform.X + e.HorizontalChange;
				}

				left = Math.Max(0, Math.Min(left, TrackLength));

				double ratio = left / TrackLength;

				double newValue = Minimum + ratio * (Maximum - Minimum);

				if (thumb == _ThumbLower)
				{
					LowerValue = newValue;
					UpdateThumbsPosition(true);

					SetShowValue(true);
				}
				else
				{
					UpperValue = newValue;
					UpdateThumbsPosition(false);

					SetShowValue(false);
				}

				if (!_Tooltip.IsOpen)
					_Tooltip.IsOpen = true;
			}
		}

		private void ThumbLower_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			//if (_TooltipLower.IsOpen)
			//	_TooltipLower.IsOpen = false;
		}

		// Upper
		private void Thumb_MouseLeave(object sender, MouseEventArgs e)
		{
			if (_Tooltip.IsOpen)
				_Tooltip.IsOpen = false;
		}

		private async void Thumb_MouseEnter(object sender, MouseEventArgs e)
		{
			await Task.Delay(500);

			if(_Tooltip != null && sender is Thumb thumb)
			{
				if (!_Tooltip.IsOpen)
				{
					if (_Tooltip != null && thumb != null)
						_Tooltip.PlacementTarget = thumb;

					if (thumb == _ThumbLower)
					{
						SetShowValue(true);
						UpdateThumbsPosition(true);
					}
					else
					{
						SetShowValue(false);
						UpdateThumbsPosition(false);
					}

					_Tooltip.IsOpen = true;
				}
			}
		}

		private void ThumbUpper_DragStarted(object sender, DragStartedEventArgs e)
		{
			//if (!_TooltipUpper.IsOpen)
			//	_TooltipUpper.IsOpen = true;
			//UpdateThumbsPosition();
		}

		private void ThumbUpper_DragDelta(object sender, DragDeltaEventArgs e)
		{
			//if (!_TooltipUpper.IsOpen)
			//	_TooltipUpper.IsOpen = true;

			double a = e.HorizontalChange;

			//UpdateThumbsPosition();
		}

		private void ThumbUpper_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			//if (_TooltipUpper.IsOpen)
			//	_TooltipUpper.IsOpen = false;
		}

		public static readonly DependencyProperty LowerValueProperty =
		DependencyProperty.Register(nameof(LowerValue), typeof(double), typeof(CRangeSlider), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public double LowerValue
		{
			get => (double)GetValue(LowerValueProperty);
			set => SetValue(LowerValueProperty, value);
		}

		public static readonly DependencyProperty UpperValueProperty =
			DependencyProperty.Register(nameof(UpperValue), typeof(double), typeof(CRangeSlider), new FrameworkPropertyMetadata(100d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public double UpperValue
		{
			get => (double)GetValue(UpperValueProperty);
			set => SetValue(UpperValueProperty, value);
		}


		public static readonly DependencyProperty TrackHeightProperty =
		DependencyProperty.Register(nameof(TrackHeight), typeof(double), typeof(CRangeSlider), new PropertyMetadata(5.0));

		public double TrackHeight
		{
			get => (double)GetValue(TrackHeightProperty);
			set => SetValue(TrackHeightProperty, value);
		}

		public static readonly DependencyProperty TrackRadiusProperty =
		DependencyProperty.Register(nameof(TrackRadius), typeof(CornerRadius), typeof(CRangeSlider), new PropertyMetadata(new CornerRadius(3)));

		public CornerRadius TrackRadius
		{
			get => (CornerRadius)GetValue(TrackRadiusProperty);
			set => SetValue(TrackRadiusProperty, value);
		}

		public static readonly DependencyProperty ShowRangeLabelProperty =
		DependencyProperty.Register( nameof(ShowRangeLabel), typeof(bool), typeof(CRangeSlider), new PropertyMetadata(true));

		public bool ShowRangeLabel
		{
			get => (bool)GetValue(ShowRangeLabelProperty);
			set => SetValue(ShowRangeLabelProperty, value);
		}
	}
}
