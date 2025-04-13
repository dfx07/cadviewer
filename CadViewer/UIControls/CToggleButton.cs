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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Windows.Input;
using CadViewer.Animations;

namespace CadViewer.Components
{
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

			_Thumb = GetTemplateChild("xCToggleButtonThumb") as Border;

			_ThumbWidth = _Thumb.ActualWidth;

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
	}
}
