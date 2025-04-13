using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace CadViewer.Animations
{
	public class CornerRadiusAnimation : AnimationTimeline
	{
		public override Type TargetPropertyType => typeof(CornerRadius);

		public CornerRadius From
		{
			get => (CornerRadius)GetValue(FromProperty);
			set => SetValue(FromProperty, value);
		}
		public static readonly DependencyProperty FromProperty =
			DependencyProperty.Register("From", typeof(CornerRadius), typeof(CornerRadiusAnimation));

		public CornerRadius To
		{
			get => (CornerRadius)GetValue(ToProperty);
			set => SetValue(ToProperty, value);
		}
		public static readonly DependencyProperty ToProperty =
			DependencyProperty.Register("To", typeof(CornerRadius), typeof(CornerRadiusAnimation));

		public IEasingFunction EasingFunction
		{
			get => (IEasingFunction)GetValue(EasingFunctionProperty);
			set => SetValue(EasingFunctionProperty, value);
		}
		public static readonly DependencyProperty EasingFunctionProperty =
			DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(CornerRadiusAnimation));

		public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
		{
			CornerRadius from = this.From;
			CornerRadius to = this.To;

			double progress = animationClock.CurrentProgress ?? 0.0;

			if (EasingFunction != null)
			{
				progress = EasingFunction.Ease(progress);
			}

			return new CornerRadius(
				from.TopLeft + (to.TopLeft - from.TopLeft) * progress,
				from.TopRight + (to.TopRight - from.TopRight) * progress,
				from.BottomRight + (to.BottomRight - from.BottomRight) * progress,
				from.BottomLeft + (to.BottomLeft - from.BottomLeft) * progress
			);
		}

		protected override Freezable CreateInstanceCore()
		{
			return new CornerRadiusAnimation();
		}
	}
}
