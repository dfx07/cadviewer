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
	public enum EToggleButtonStyle
	{
		Normal,
		Switch,
		Square
	}

	public class CToggleButton : ToggleButton
	{
		private Border _Thumb;
		private double _ThumbWidth = 0.0;

		static CToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CToggleButton),
				new FrameworkPropertyMetadata(typeof(CToggleButton)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if(ButtonStyle == EToggleButtonStyle.Switch)
			{
				_Thumb = GetTemplateChild("xCToggleButtonThumb") as Border;

				_ThumbWidth = _Thumb.ActualWidth;
			}

			Loaded += (s, e) =>
			{
				if (_Thumb != null)
				{
					_ThumbWidth = _Thumb.ActualHeight;
				}
			};

			PreviewMouseLeftButtonDown -= CToggleButton_PreviewMouseLeftButtonDown;
			PreviewMouseLeftButtonUp -= CToggleButton_PreviewMouseLeftButtonUp; ;

			PreviewMouseLeftButtonDown += CToggleButton_PreviewMouseLeftButtonDown;
			PreviewMouseLeftButtonUp += CToggleButton_PreviewMouseLeftButtonUp;

			//UpdateVisualState(false);
		}

		void UpdateVisualState(bool useTransitions)
		{
		}

		protected override void OnMouseEnter(MouseEventArgs e)
		{
			base.OnMouseEnter(e);
			UpdateVisualState(true);
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);
			UpdateVisualState(true);
		}

		protected override void OnChecked(RoutedEventArgs e)
		{
			base.OnChecked(e);
			UpdateVisualState(true);
		}

		protected override void OnUnchecked(RoutedEventArgs e)
		{
			base.OnUnchecked(e);
			UpdateVisualState(true);
		}

		private void CToggleButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (_Thumb != null)
			{
				AnimateMouseUp(_Thumb);
			}
		}

		private void CToggleButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (_Thumb != null)
			{
				AnimateMouseDown(_Thumb);
			}
		}

		private void AnimateMouseUp(Border refThumb)
		{
			var xThumbTransformGroup = refThumb.RenderTransform as TransformGroup;
			double toThumbWidth = _ThumbWidth;

			// Animate Thumb Position
			if (xThumbTransformGroup != null)
			{
				var xTranThumbTransform = xThumbTransformGroup.Children[1] as TranslateTransform;

				if (xTranThumbTransform == null)
					return;

				double dbPadding = (IsChecked == true) ? refThumb.Margin.Left : refThumb.Margin.Right;
				double dbTarget = ActualWidth - refThumb.ActualWidth - refThumb.Margin.Left - refThumb.Margin.Right;

				dbTarget = (IsChecked == true) ? dbTarget : -dbTarget;

				DoubleAnimation anim = new DoubleAnimation
				{
					From = dbTarget,
					To = 0,
					Duration = TimeSpan.FromMilliseconds(400),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
				};

				anim.Completed += (s, e) =>
				{
					refThumb.BeginAnimation(Border.WidthProperty, null);
					refThumb.Width = _ThumbWidth;
					this.IsHitTestVisible = true;
				};

				xTranThumbTransform.BeginAnimation(TranslateTransform.XProperty, anim);
			}

			// Animate Thumb Width
			var widthAnim = new DoubleAnimation
			{
				To = toThumbWidth,
				Duration = TimeSpan.FromMilliseconds(400),
				EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
			};

			refThumb.BeginAnimation(Border.WidthProperty, widthAnim);
		}

		private void AnimateMouseDown(Border refThumb)
		{
			double toThumbWidth = _ThumbWidth + 6;

			// Animate Width
			var widthAnim = new DoubleAnimation
			{
				To = toThumbWidth,
				Duration = TimeSpan.FromMilliseconds(100),
				EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
			};
			refThumb.BeginAnimation(Border.WidthProperty, widthAnim);
		}

		public static readonly DependencyProperty CornerRadiusProperty =
		DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CToggleButton), new PropertyMetadata(new CornerRadius(0, 0, 0, 0)));

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		public static readonly DependencyProperty ButtonStyleProperty =
		DependencyProperty.Register(nameof(ButtonStyle), typeof(EToggleButtonStyle), typeof(CToggleButton), new PropertyMetadata(EToggleButtonStyle.Switch));

		public EToggleButtonStyle ButtonStyle
		{
			get => (EToggleButtonStyle)GetValue(ButtonStyleProperty);
			set => SetValue(ButtonStyleProperty, value);
		}

		// Only support normal button style
		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CToggleButton), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CToggleButton), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty OnTextProperty =
		DependencyProperty.Register(nameof(OnText), typeof(string), typeof(CToggleButton), new PropertyMetadata("On"));

		public string OnText
		{
			get => (string)GetValue(OnTextProperty);
			set => SetValue(OnTextProperty, value);
		}

		public static readonly DependencyProperty OffTextProperty =
		DependencyProperty.Register(nameof(OffText), typeof(string), typeof(CToggleButton), new PropertyMetadata("Off"));

		public string OffText
		{
			get => (string)GetValue(OffTextProperty);
			set => SetValue(OffTextProperty, value);
		}

		public static readonly DependencyProperty BackgroundOverColorProperty =
		DependencyProperty.Register(nameof(BackgroundOverColor), typeof(Color), typeof(CToggleButton), new PropertyMetadata(Colors.DodgerBlue));

		public Color BackgroundOverColor
		{
			get => (Color)GetValue(BackgroundOverColorProperty);
			set => SetValue(BackgroundOverColorProperty, value);
		}
	}


	public class CFlatToggleButton : ToggleButton
	{
		static CFlatToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CFlatToggleButton),
				new FrameworkPropertyMetadata(typeof(CFlatToggleButton)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}

		public static readonly DependencyProperty CornerRadiusProperty =
			DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CFlatToggleButton),
				new PropertyMetadata(new CornerRadius(0, 0, 0, 0)));

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		public Brush HoverBackground
		{
			get => (Brush)GetValue(HoverBackgroundProperty);
			set => SetValue(HoverBackgroundProperty, value);
		}

		public static readonly DependencyProperty HoverBackgroundProperty =
			DependencyProperty.Register(nameof(HoverBackground), typeof(Brush), typeof(CFlatToggleButton));

		public Brush CheckedBackground
		{
			get => (Brush)GetValue(CheckedBackgroundProperty);
			set => SetValue(CheckedBackgroundProperty, value);
		}

		public static readonly DependencyProperty CheckedBackgroundProperty =
			DependencyProperty.Register(nameof(CheckedBackground), typeof(Brush), typeof(CFlatToggleButton));

		public static readonly DependencyProperty ImageSourceProperty =
			DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CFlatToggleButton));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
			DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CFlatToggleButton), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}
	}
}
