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

		private void UpdateVisualState()
		{
			var state = "";

			if(IsMouseOver && IsChecked is true)
			{
				state = "MouseOver_Checked";
			}
			else if (IsMouseOver && IsChecked is false)
			{
				state = "MouseOver_Unchecked";
			}
			else if (!IsMouseOver && IsChecked is true)
			{
				state = "Normal_Checked";
			}
			else if (!IsMouseOver && IsChecked is false)
			{
				state = "Normal_Unchecked";
			}

			VisualStateManager.GoToState(this, state, true);
		}

		protected override void OnMouseEnter(MouseEventArgs e)
		{
			base.OnMouseEnter(e);
			UpdateVisualState();
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);
			UpdateVisualState();
		}

		protected override void OnChecked(RoutedEventArgs e)
		{
			base.OnChecked(e);
			UpdateVisualState();
		}

		protected override void OnUnchecked(RoutedEventArgs e)
		{
			base.OnUnchecked(e);
			UpdateVisualState();
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
