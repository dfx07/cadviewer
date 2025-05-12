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
	public enum CWaitingMessageDirection
	{
		Top,
		Bottom
	}

	public enum CWaitingIconStyle
	{
		Line,
		Circle
	}

	public class CWaiting : Control
	{
		private Canvas _canvas;

		static CWaiting()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CWaiting),
				new FrameworkPropertyMetadata(typeof(CWaiting)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_canvas = GetTemplateChild("PART_Canvas") as Canvas;
			if (_canvas != null)
			{
				_canvas.Children.Clear();
			}

			Loaded += (s, e) =>
			{
				CreateLoadingVisual();
			};
		}

		private void CreateLoadingVisual()
		{
			if (_canvas == null)
				return;

			double dbCanvasWidth = _canvas.Width;
			double dbCanvasHeight = _canvas.Width;

			int lineCount = 10;
			double radius = dbCanvasWidth / 2;
			double centerX = dbCanvasWidth / 2;
			double centerY = dbCanvasHeight / 2;

			double dbOpacityStart = 0.2;

			for (int i = 0; i < lineCount; i++)
			{
				double angle = i * 360.0 / lineCount;
				double rad = angle * Math.PI / 180;

				double x1 = centerX + Math.Cos(rad) * radius;
				double y1 = centerY + Math.Sin(rad) * radius;
				double x2 = centerX + Math.Cos(rad) * (radius - radius * 0.5);
				double y2 = centerY + Math.Sin(rad) * (radius - radius * 0.5);

				var line = new Line
				{
					X1 = x1,
					Y1 = y1,
					X2 = x2,
					Y2 = y2,
					Stroke = Brushes.Gray,
					StrokeThickness = 2,
					StrokeStartLineCap = PenLineCap.Round,
					StrokeEndLineCap = PenLineCap.Round,
					Opacity = dbOpacityStart
				};

				var animation = new DoubleAnimation
				{
					From = dbOpacityStart,
					To = 1,
					Duration = TimeSpan.FromMilliseconds(620),
					BeginTime = TimeSpan.FromMilliseconds(i * 100),
					AutoReverse = true,
					RepeatBehavior = RepeatBehavior.Forever
				};

				line.BeginAnimation(UIElement.OpacityProperty, animation);
				_canvas.Children.Add(line);
			}
		}

		private static void OnLoadIconWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is CWaiting control)
			{
				control.CreateLoadingVisual();
			}
		}

		public string Message
		{
			get { return (string)GetValue(MessageProperty); }
			set { SetValue(MessageProperty, value); }
		}

		public static readonly DependencyProperty MessageProperty =
			DependencyProperty.Register(nameof(Message), typeof(string), typeof(CWaiting), new PropertyMetadata("Waiting..."));

		public static readonly DependencyProperty LoadIconWidthProperty =
		DependencyProperty.Register(nameof(LoadIconWidth), typeof(double), typeof(CWaiting), new PropertyMetadata(20.0, OnLoadIconWidthChanged));

		public double LoadIconWidth
		{
			get => (double)GetValue(LoadIconWidthProperty);
			set => SetValue(LoadIconWidthProperty, value);
		}


		public static readonly DependencyProperty LoadIconTypeProperty =
		DependencyProperty.Register(nameof(LoadIconWidth), typeof(double), typeof(CWaiting), new PropertyMetadata(20.0, OnLoadIconWidthChanged));

		public double LoadIconTypeWidth
		{
			get => (double)GetValue(LoadIconTypeProperty);
			set => SetValue(LoadIconTypeProperty, value);
		}
	}
}
