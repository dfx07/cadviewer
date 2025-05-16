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
	public class CScrollViewer : ScrollViewer
	{
		private double _targetVerticalOffset;
		private double _targetHorizontalOffset;
		private double _animationSpeed = 0.2;
		private bool _isAnimatingVertical = false;
		private bool _isAnimatingHorizontal = false;

		static CScrollViewer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CScrollViewer),
				new FrameworkPropertyMetadata(typeof(CScrollViewer)));
		}

		public CScrollViewer()
		{
			PreviewMouseWheel += OnPreviewMouseWheel;
			CompositionTarget.Rendering += AnimateScroll;
		}

		private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
			{
				_targetHorizontalOffset = Math.Max(0, Math.Min(this.ScrollableWidth, _targetHorizontalOffset - e.Delta));
				_isAnimatingHorizontal = true;
			}
			else
			{
				_targetVerticalOffset = Math.Max(0, Math.Min(this.ScrollableHeight, _targetVerticalOffset - e.Delta));
				_isAnimatingVertical = true;
			}

			e.Handled = true;
		}

		private void AnimateScroll(object sender, EventArgs e)
		{
			if (_isAnimatingVertical)
			{
				double current = this.VerticalOffset;
				double delta = _targetVerticalOffset - current;

				if (Math.Abs(delta) < 0.5)
				{
					this.ScrollToVerticalOffset(_targetVerticalOffset);
					_isAnimatingVertical = false;
				}
				else
				{
					this.ScrollToVerticalOffset(current + delta * _animationSpeed);
				}
			}

			if (_isAnimatingHorizontal)
			{
				double current = this.HorizontalOffset;
				double delta = _targetHorizontalOffset - current;

				if (Math.Abs(delta) < 0.5)
				{
					this.ScrollToHorizontalOffset(_targetHorizontalOffset);
					_isAnimatingHorizontal = false;
				}
				else
				{
					this.ScrollToHorizontalOffset(current + delta * _animationSpeed);
				}
			}
		}

		public static readonly DependencyProperty AutoHideScrollBarProperty =
		DependencyProperty.Register(nameof(AutoHideScrollBar), typeof(bool), typeof(CScrollViewer), new PropertyMetadata(false));

		public bool AutoHideScrollBar
		{
			get => (bool)GetValue(AutoHideScrollBarProperty);
			set => SetValue(AutoHideScrollBarProperty, value);
		}

		public static readonly DependencyProperty ViewThicknessProperty =
		DependencyProperty.Register(nameof(ViewThickness), typeof(Thickness), typeof(CScrollViewer), new PropertyMetadata(new Thickness(0)));

		public Thickness ViewThickness
		{
			get => (Thickness)GetValue(ViewThicknessProperty);
			set => SetValue(ViewThicknessProperty, value);
		}
	}
}
