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
			double targetHeight = _contentPresenter.DesiredSize.Height;

			double from = this.IsVisible ? 0 : targetHeight;
			double to = this.IsVisible ? targetHeight : 0;

			var animation = new DoubleAnimation
			{
				From = from,
				To = to,
				Duration = TimeSpan.FromMilliseconds(300),
				EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
			};

			_contentHost.BeginAnimation(Border.HeightProperty, animation);
		}

		public static readonly DependencyProperty CornerRadiusProperty =
		DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CSlideDownContainer), new PropertyMetadata(new CornerRadius(0, 0, 0, 0)));

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}
	}
}
