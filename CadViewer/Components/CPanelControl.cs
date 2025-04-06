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
			SetCurrentValue(PN_CornerRadiusProperty, new CornerRadius(15));
		}

		public CornerRadius PN_CornerRadius
		{
			get { return (CornerRadius)GetValue(PN_CornerRadiusProperty); }
			set { SetValue(PN_CornerRadiusProperty, value); }
		}

		public static readonly DependencyProperty PN_CornerRadiusProperty =
			DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CPanelControl),
				new PropertyMetadata());
	}
}
