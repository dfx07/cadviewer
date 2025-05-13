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
	public enum CProgressBarStyle
	{
		Line,
		Circle
	}

	public enum CProgressBarStatus
	{
		Info,
		Warn,
		Error,
	}

	public class CProgressBar : ProgressBar
	{
		static CProgressBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CProgressBar),
				new FrameworkPropertyMetadata(typeof(CProgressBar)));
		}

		private Path _ProgressArc = null;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{
				_ProgressArc = GetTemplateChild("PART_ProgressArc") as Path;
				UpdateArc();
			};
		}

		private void UpdateArc()
		{
			if (ProgressStyle != CProgressBarStyle.Circle)
				return;

			double percent = Math.Max(0, Math.Min(100, Value));
			double angle = percent / 100 * 360;

			if (angle <= 0)
			{
				_ProgressArc.Data = Geometry.Empty;
				return;
			}

			double radius = ProgressBarHeight / 2 - 4;
			Point center = new Point(ProgressBarHeight / 2, ProgressBarHeight / 2);

			int segments = 100; // Tăng số đoạn cho mượt hơn
			double angleStep = angle / segments;

			var figure = new PathFigure();
			for (int i = 0; i <= segments; i++)
			{
				double currentAngle = -90 + i * angleStep; // Bắt đầu từ đỉnh (12h)
				double rad = currentAngle * Math.PI / 180;

				double x = center.X + radius * Math.Cos(rad);
				double y = center.Y + radius * Math.Sin(rad);

				if (i == 0)
					figure.StartPoint = new Point(x, y);
				else
					figure.Segments.Add(new LineSegment(new Point(x, y), true));
			}

			var geometry = new PathGeometry();
			geometry.Figures.Add(figure);
			_ProgressArc.Data = geometry;
		}

		public static readonly DependencyProperty ProgressStyleProperty =
			DependencyProperty.Register(nameof(ProgressStyle), typeof(CProgressBarStyle), typeof(CProgressBar), new PropertyMetadata(CProgressBarStyle.Line));

		public CProgressBarStyle ProgressStyle
		{
			get => (CProgressBarStyle)GetValue(ProgressStyleProperty);
			set => SetValue(ProgressStyleProperty, value);
		}


		public static readonly DependencyProperty ProgressStatusProperty =
			DependencyProperty.Register(nameof(ProgressStatus), typeof(CProgressBarStatus), typeof(CProgressBar), new PropertyMetadata(CProgressBarStatus.Info));

		public CProgressBarStatus ProgressStatus
		{
			get => (CProgressBarStatus)GetValue(ProgressStatusProperty);
			set => SetValue(ProgressStatusProperty, value);
		}

		public string Message
		{
			get { return (string)GetValue(MessageProperty); }
			set { SetValue(MessageProperty, value); }
		}

		public static readonly DependencyProperty MessageProperty =
			DependencyProperty.Register(nameof(Message), typeof(string), typeof(CProgressBar), new PropertyMetadata("Waiting..."));

		public double ProgressBarHeight
		{
			get => (double)GetValue(ProgressBarHeightProperty);
			set => SetValue(ProgressBarHeightProperty, value);
		}

		public static readonly DependencyProperty ProgressBarHeightProperty =
			DependencyProperty.Register(nameof(ProgressBarHeight), typeof(double), typeof(CProgressBar), new PropertyMetadata(20.0));

		public static readonly DependencyProperty ShowProgressCountProperty =
			DependencyProperty.Register(nameof(ShowProgressCount), typeof(bool), typeof(CProgressBar), new PropertyMetadata(true));

		public bool ShowProgressCount
		{
			get => (bool)GetValue(ShowProgressCountProperty);
			set => SetValue(ShowProgressCountProperty, value);
		}

		public static readonly DependencyProperty DisplayFormatProperty =
		DependencyProperty.Register(nameof(DisplayFormat), typeof(string), typeof(CProgressBar), new PropertyMetadata("_val_"));

		public string DisplayFormat
		{
			get => (string)GetValue(DisplayFormatProperty);
			set => SetValue(DisplayFormatProperty, value);
		}
	}
}
