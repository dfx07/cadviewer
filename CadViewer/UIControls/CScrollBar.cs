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
	public enum ScrollDirection
	{
		Vertical,
		Horizontal
	}
	public enum LineButtonType
	{
		Up,
		Down
	}

	public class CScrollBarRepeatButton_ : RepeatButton
	{
		protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnPreviewMouseLeftButtonDown(e);

			var state = "";

			if (Direction == ScrollDirection.Vertical && ButtonType == LineButtonType.Up)
			{
				state = "Pressed_VerticalUp";
			}
			else if (Direction == ScrollDirection.Vertical && ButtonType == LineButtonType.Down)
			{
				state = "Pressed_VerticalDown";
			}
			else if (Direction == ScrollDirection.Horizontal && ButtonType == LineButtonType.Up)
			{
				state = "Pressed_HorizontalLeft";
			}
			else if (Direction == ScrollDirection.Horizontal && ButtonType == LineButtonType.Down)
			{
				state = "Pressed_HorizontalRight";
			}

			if (state != string.Empty)
				VisualStateManager.GoToState(this, state, true);
		}

		public static readonly DependencyProperty DirectionProperty =
		DependencyProperty.Register(nameof(Direction), typeof(ScrollDirection), typeof(CScrollBarRepeatButton_), new PropertyMetadata(ScrollDirection.Vertical));

		public ScrollDirection Direction
		{
			get => (ScrollDirection)GetValue(DirectionProperty);
			set => SetValue(DirectionProperty, value);
		}

		public static readonly DependencyProperty ButtonTypeProperty =
		DependencyProperty.Register(nameof(ButtonType), typeof(LineButtonType), typeof(CScrollBarRepeatButton_), new PropertyMetadata(LineButtonType.Up));

		public LineButtonType ButtonType
		{
			get => (LineButtonType)GetValue(ButtonTypeProperty);
			set => SetValue(ButtonTypeProperty, value);
		}
	}

	public class CScrollBar : ScrollBar
	{
		static CScrollBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CScrollBar),
				new FrameworkPropertyMetadata(typeof(CScrollBar)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty LineButtonWidthProperty =
		DependencyProperty.Register(nameof(LineButtonWidth), typeof(double), typeof(CScrollBar), new PropertyMetadata(10.0));

		public double LineButtonWidth
		{
			get => (double)GetValue(LineButtonWidthProperty);
			set => SetValue(LineButtonWidthProperty, value);
		}
	}
}
