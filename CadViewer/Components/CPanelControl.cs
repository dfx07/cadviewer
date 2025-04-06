using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CadViewer.Components
{
	public class CPanelControl : UserControl
	{
		public CPanelControl()
		{
			SetCurrentValue(PN_BorderThicknessProperty, new Thickness(1));
			SetCurrentValue(PN_CornerRadiusProperty, new CornerRadius(15));
			SetCurrentValue(PN_BackgroundColorProperty, new SolidColorBrush(Color.FromRgb(120, 120, 120)));
			SetCurrentValue(PN_BorderColorProperty, new SolidColorBrush(Color.FromRgb(255, 0, 0)));
		}
		public Thickness PN_BorderThickness
		{
			get { return (Thickness)GetValue(PN_BorderThicknessProperty); }
			set { SetValue(PN_BorderThicknessProperty, value); }
		}

		public static readonly DependencyProperty PN_BorderThicknessProperty =
			DependencyProperty.Register(nameof(PN_BorderThickness), typeof(Thickness), typeof(CPanelControl),
				new PropertyMetadata());
		public Brush PN_BorderColor
		{
			get { return (Brush)GetValue(PN_BorderColorProperty); }
			set { SetValue(PN_BorderColorProperty, value); }
		}

		public static readonly DependencyProperty PN_BorderColorProperty =
			DependencyProperty.Register(nameof(PN_BorderColor), typeof(Brush), typeof(CPanelControl),
				new PropertyMetadata());

		public CornerRadius PN_CornerRadius
		{
			get { return (CornerRadius)GetValue(PN_CornerRadiusProperty); }
			set { SetValue(PN_CornerRadiusProperty, value); }
		}

		public static readonly DependencyProperty PN_CornerRadiusProperty =
			DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CPanelControl),
				new PropertyMetadata());

		public Brush PN_BackgroundColor
		{
			get { return (Brush)GetValue(PN_BackgroundColorProperty); }
			set { SetValue(PN_BackgroundColorProperty, value); }
		}

		public static readonly DependencyProperty PN_BackgroundColorProperty =
			DependencyProperty.Register(nameof(PN_BackgroundColorProperty), typeof(Brush), typeof(CPanelControl),
				new PropertyMetadata());


	}
}
