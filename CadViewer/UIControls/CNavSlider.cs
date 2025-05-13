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
using System.Drawing;

namespace CadViewer.UIControls
{
	public class CNavSlider : ContentControl
	{
		static CNavSlider()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CNavSlider),
				new FrameworkPropertyMetadata(typeof(CNavSlider)));
		}

		private TranslateTransform _Translate;
		private double _LastWidth = 250;
		private double _LastHeight = 250;
		private double _InitWidth = 250;
		private double _InitHeight = 250;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_Translate = GetTemplateChild("PART_Translate") as TranslateTransform;

			Loaded += (s, e) =>
			{
				_LastWidth  = _InitWidth = ActualWidth;
				_LastHeight = _InitHeight = ActualHeight;

				if (_Translate != null)
				{
					if(Orientation == Orientation.Vertical)
					{
						_Translate.Y = IsOpen ? 0 : -_InitHeight;
					}
					else
					{
						_Translate.X = IsOpen ? 0 : -_InitWidth ;
					}
				}
			};
		}

		public static readonly DependencyProperty IsOpenProperty =
		DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(CNavSlider), new PropertyMetadata(false, OnIsOpenChanged));

		public bool IsOpen
		{
			get => (bool)GetValue(IsOpenProperty);
			set => SetValue(IsOpenProperty, value);
		}

		public static readonly DependencyProperty IsUseAnimationProperty =
		DependencyProperty.Register(nameof(IsUseAnimation), typeof(bool), typeof(CNavSlider), new PropertyMetadata(true));

		public bool IsUseAnimation
		{
			get => (bool)GetValue(IsUseAnimationProperty);
			set => SetValue(IsUseAnimationProperty, value);
		}

		public static readonly DependencyProperty OrientationProperty =
		DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(CNavSlider), new PropertyMetadata(Orientation.Horizontal));

		public Orientation Orientation
		{
			get => (Orientation)GetValue(OrientationProperty);
			set => SetValue(OrientationProperty, value);
		}

		private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is CNavSlider nav && nav._Translate != null)
			{
				nav.Animate((bool)e.NewValue);
			}
		}
		private void Animate(bool bOpen)
		{
			if (_Translate == null) return;

			if (!bOpen)
			{
				_LastWidth = ActualWidth;
				_LastHeight = ActualHeight;
			}

			if(Orientation == Orientation.Vertical)
			{
				AnimationVertical(bOpen);
			}
			else
			{
				AnimationHorizontal(bOpen);
			}
		}

		private void AnimationVertical(bool bOpen)
		{
			if(!IsUseAnimation)
			{
				_Translate.Y = bOpen ? 0 : -_LastHeight;
				return;
			}

			if (bOpen)
			{
				this.Visibility = Visibility.Visible;

				var slideIn = new DoubleAnimation
				{
					From = -_LastHeight,
					To = 0,
					Duration = TimeSpan.FromMilliseconds(300),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
				};

				var heightIn = new DoubleAnimation
				{
					From = 0,
					To = _LastHeight,
					Duration = TimeSpan.FromMilliseconds(300),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
				};

				_Translate.BeginAnimation(TranslateTransform.YProperty, slideIn);
				this.BeginAnimation(HeightProperty, heightIn);
			}
			else
			{
				var slideOut = new DoubleAnimation
				{
					To = -_LastHeight,
					Duration = TimeSpan.FromMilliseconds(300),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
				};

				var heightOut = new DoubleAnimation
				{
					To = 0,
					Duration = TimeSpan.FromMilliseconds(300),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
				};

				heightOut.Completed += (s, e) =>
				{
					this.Visibility = Visibility.Collapsed;
				};

				_Translate.BeginAnimation(TranslateTransform.YProperty, slideOut);
				this.BeginAnimation(HeightProperty, heightOut);
			}
		}

		private void AnimationHorizontal(bool bOpen)
		{
			if (!IsUseAnimation)
			{
				_Translate.X = bOpen ? 0 : -_LastWidth;
				return;
			}

			if (bOpen)
			{
				this.Visibility = Visibility.Visible;

				var slideIn = new DoubleAnimation
				{
					From = -_LastWidth,
					To = 0,
					Duration = TimeSpan.FromMilliseconds(300),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
				};

				var widthIn = new DoubleAnimation
				{
					From = 0,
					To = _LastWidth,
					Duration = TimeSpan.FromMilliseconds(300),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
				};

				_Translate.BeginAnimation(TranslateTransform.XProperty, slideIn);
				this.BeginAnimation(WidthProperty, widthIn);
			}
			else
			{
				var slideOut = new DoubleAnimation
				{
					To = -_LastWidth,
					Duration = TimeSpan.FromMilliseconds(300),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
				};

				var widthOut = new DoubleAnimation
				{
					To = 0,
					Duration = TimeSpan.FromMilliseconds(300),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
				};

				widthOut.Completed += (s, e) =>
				{
					this.Visibility = Visibility.Collapsed;
				};

				_Translate.BeginAnimation(TranslateTransform.XProperty, slideOut);
				this.BeginAnimation(WidthProperty, widthOut);
			}
		}
	}
}
