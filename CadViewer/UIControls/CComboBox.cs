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

namespace CadViewer.UIControls
{
	public class CComboBox : ComboBox
	{
		static CComboBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CComboBox),
				new FrameworkPropertyMetadata(typeof(CComboBox)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty IsDropDownOpenProperty =
		DependencyProperty.Register(nameof(IsDropDownOpen), typeof(bool), typeof(CComboBox), new PropertyMetadata(false));
		public bool IsDropDownOpen
		{
			get => (bool)GetValue(IsDropDownOpenProperty);
			set => SetValue(IsDropDownOpenProperty, value);
		}
	}
}
