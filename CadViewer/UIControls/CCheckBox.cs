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
	public class CCheckBox : CheckBox
	{
		static CCheckBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CCheckBox),
				new FrameworkPropertyMetadata(typeof(CCheckBox)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty CheckWidthProperty =
		DependencyProperty.Register(nameof(CheckWidth), typeof(double), typeof(CButton), new PropertyMetadata(double.NaN));

		public double CheckWidth
		{
			get => (double)GetValue(CheckWidthProperty);
			set => SetValue(CheckWidthProperty, value);
		}
	}
}
