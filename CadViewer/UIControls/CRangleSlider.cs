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
	public class CRangeSliderTickBar : TickBar
	{
		protected override void OnRender(DrawingContext dc)
		{
			if (TickFrequency <= 0 || Maximum <= Minimum)
				return;

			double range = Maximum - Minimum;
			double tickCount = range / TickFrequency;
			double tickSpacing = (this.ActualWidth) / tickCount;

			for (int i = 0; i <= tickCount; i++)
			{
				double x = i * tickSpacing;
				dc.DrawLine(new Pen(this.Fill, 1),
					new Point(x, 0),
					new Point(x, this.ActualHeight));
			}
		}
	}

	public class CRangeSlider : Slider
	{
		static CRangeSlider()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CRangeSlider),
				new FrameworkPropertyMetadata(typeof(CRangeSlider)));
		}

		private Thumb _ThumbLower = null;
		private Thumb _ThumbUpper = null;

		private Popup _Tooltip = null;
		private Border _BorderTrack = null;
		private Border _BorderTooltip = null;
		private TextBlock _ValueTip = null;

		private TranslateTransform _LowerTranslateTranform = null;
		private TranslateTransform _UpperTranslateTranform = null;

		private Border _SelectedRange = null;
		private DispatcherTimer _TooltipTimer = null;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{
				var thumb1 = GetTemplateChild("PART_LowerThumb") as Thumb;
				var toolTip = GetTemplateChild("PART_Tooltip") as Popup;
				var borderTrack = GetTemplateChild("xCRangeSliderTrackBackground") as Border;
				var borderTooltip = GetTemplateChild("BorderTooltipValue") as Border;
				var valueTip1 = GetTemplateChild("TooltipValue") as TextBlock;

				var thumb2 = GetTemplateChild("PART_UpperThumb") as Thumb;

				var tran1 = GetTemplateChild("LowerThumbTranslate") as TranslateTransform;
				var tran2 = GetTemplateChild("UpperThumbTranslate") as TranslateTransform;

				var selectedrangle = GetTemplateChild("SelectedRange") as Border;

				if (thumb1 != null)
				{
					thumb1.DragDelta += Thumb_DragDelta;
					thumb1.DragCompleted += Thumb_DragCompleted;
					thumb1.MouseEnter += Thumb_MouseEnter;
					thumb1.MouseLeave += Thumb_MouseLeave;

					_ThumbLower = thumb1;
					_LowerTranslateTranform = tran1;
				}

				if (thumb2 != null)
				{
					thumb2.DragDelta += Thumb_DragDelta;
					thumb2.DragCompleted += Thumb_DragCompleted;
					thumb2.MouseEnter += Thumb_MouseEnter;
					thumb2.MouseLeave += Thumb_MouseLeave;

					_ThumbUpper = thumb2;
					_UpperTranslateTranform = tran2;
				}

				_Tooltip = toolTip;
				_BorderTrack = borderTrack;
				_ValueTip = valueTip1;
				_BorderTooltip = borderTooltip;
				_SelectedRange = selectedrangle;

				UpdateThumbsPosition(false);
				UpdateThumbsPosition(true);

				UpdateSelectedRange();
			};
		}

		private void RecalTooltip(Thumb thumb)
		{
			if (thumb == null || _Tooltip == null)
				return;

			Dispatcher.BeginInvoke(new Action(() =>
			{
				double offsetX = (thumb.ActualWidth - _BorderTooltip.ActualWidth) / 2;

				_Tooltip.HorizontalOffset = offsetX - 0.1;
				_Tooltip.HorizontalOffset = offsetX;

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
			var TrackLength = _BorderTrack.ActualWidth - _ThumbLower.ActualWidth;

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
			if (_SelectedRange != null)
			{
				Dispatcher.BeginInvoke(new Action(() =>
				{
					double left = _LowerTranslateTranform.X + _ThumbUpper.ActualWidth / 2;
					double right = _UpperTranslateTranform.X + _ThumbUpper.ActualWidth / 2;
					double width = right - left;

					Canvas.SetLeft(_SelectedRange, left);
					_SelectedRange.Width = Math.Abs(width);

				}), System.Windows.Threading.DispatcherPriority.Loaded);
			}
		}

		private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if(sender is Thumb thumb)
			{
				var TrackLength = _BorderTrack.ActualWidth - thumb.ActualWidth;

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

				RecalTooltip(thumb);

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

		private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			_TooltipTimer?.Stop();
			_Tooltip.IsOpen = false;
		}

		private void TooltipTimer_Tick(object sender, EventArgs e)
		{
			_TooltipTimer?.Stop();

			if (_Tooltip != null)
			{
				if ((bool)_TooltipTimer.Tag == true)
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

		private void Thumb_MouseEnter(object sender, MouseEventArgs e)
		{
			if (_TooltipTimer == null)
			{
				_TooltipTimer = new DispatcherTimer
				{
					Interval = TimeSpan.FromMilliseconds(400),
				};
				_TooltipTimer.Tick += TooltipTimer_Tick;
			}
			
			if(sender is Thumb thumb)
			{
				_TooltipTimer.Tag = (thumb == _ThumbLower) ? true : false;
			}

			_TooltipTimer.Start();
		}
		private void Thumb_MouseLeave(object sender, MouseEventArgs e)
		{
			_Tooltip.IsOpen = false;
			_TooltipTimer?.Stop();
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
