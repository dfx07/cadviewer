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
	public enum ESlideDownDirection
	{
		WIDTH,
		HEIGHT,
	}

	public class CSlideDownContainer : ContentControl
	{
		private Border _contentHost;
		private ContentPresenter _contentPresenter;
		private ScaleTransform _scale;

		static CSlideDownContainer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CSlideDownContainer),
				new FrameworkPropertyMetadata(typeof(CSlideDownContainer)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_contentHost = GetTemplateChild("PART_ContentHost") as Border;
			_contentPresenter = GetTemplateChild("PART_ContentPresenter") as ContentPresenter;

			_scale = GetTemplateChild("PART_Scale") as ScaleTransform;

			this.IsVisibleChanged -= OnIsVisibleChanged;
			this.IsVisibleChanged += OnIsVisibleChanged;

			if (IsVisible == false)
			{
				_contentHost.Height = 0;
			}
		}

		private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (_contentHost == null)
				return;

			_contentHost.Measure(new Size(_contentHost.ActualWidth, double.PositiveInfinity));
			double targetValue = _contentPresenter.DesiredSize.Width;

			if(UseSelf)
			{
				targetValue = SliderDirection == ESlideDownDirection.HEIGHT ?
					ActualHeight : ActualWidth;
			}
			else
			{
				targetValue = SliderDirection == ESlideDownDirection.HEIGHT ?
					_contentPresenter.DesiredSize.Height : _contentPresenter.DesiredSize.Width;
			}

			double from = this.IsVisible ? 0 : targetValue;
			double to = this.IsVisible ? targetValue : 0;

			var animation = new DoubleAnimation
			{
				From = from,
				To = to,
				Duration = TimeSpan.FromMilliseconds(300),
				EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
			};

			if(SliderDirection == ESlideDownDirection.HEIGHT)
			{
				_contentHost.BeginAnimation(Border.HeightProperty, animation);
			}
			else
			{
				_contentHost.BeginAnimation(Border.WidthProperty, animation);
			}
		}

		public static readonly DependencyProperty CornerRadiusProperty =
		DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CSlideDownContainer), new PropertyMetadata(new CornerRadius(0, 0, 0, 0)));

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		/* true : use the size of selfit | false : use the size of content*/
		public static readonly DependencyProperty UseSelfProperty =
		DependencyProperty.Register(nameof(UseSelf), typeof(bool), typeof(CSlideDownContainer), new PropertyMetadata(false));

		public bool UseSelf
		{
			get => (bool)GetValue(UseSelfProperty);
			set => SetValue(UseSelfProperty, value);
		}

		public static readonly DependencyProperty SliderDirectionProperty =
		DependencyProperty.Register(nameof(SliderDirection), typeof(ESlideDownDirection), typeof(CSlideDownContainer), new PropertyMetadata(ESlideDownDirection.HEIGHT));

		public ESlideDownDirection SliderDirection
		{
			get => (ESlideDownDirection)GetValue(SliderDirectionProperty);
			set => SetValue(SliderDirectionProperty, value);
		}
	}
}
