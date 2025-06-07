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
using System.Collections.ObjectModel;

namespace CadViewer.UIControls
{
	public enum InputType
	{
		String,
		Integer,
		Double
	}

	public class CInputBox : TextBox
	{
		static CInputBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CInputBox), new FrameworkPropertyMetadata(typeof(CInputBox)));
		}

		public static readonly DependencyProperty InputTypeProperty =
			DependencyProperty.Register(nameof(InputType), typeof(InputType), typeof(CInputBox), new PropertyMetadata(InputType.String));

		public InputType InputType
		{
			get => (InputType)GetValue(InputTypeProperty);
			set => SetValue(InputTypeProperty, value);
		}

		public static readonly DependencyProperty CornerRadiusProperty =
		DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CInputBox), new PropertyMetadata(new CornerRadius(0, 0, 0, 0)));

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			base.OnPreviewTextInput(e);

			if (!IsInputValid(e.Text))
				e.Handled = true;
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			base.OnTextChanged(e);

			if (!IsFullTextValid())
				BorderBrush = System.Windows.Media.Brushes.Red;
			else
				ClearValue(BorderBrushProperty);
		}

		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			if (InputType != InputType.String && e.Key == Key.Space)
			{
				e.Handled = true;
			}
			base.OnPreviewKeyDown(e);
		}

		private bool IsInputValid(string input)
		{
			if (InputType == InputType.String)
				return true;

			if (InputType == InputType.Integer)
				return int.TryParse(this.Text + input, out _);

			if (InputType == InputType.Double)
				return double.TryParse(this.Text + input, out _);

			return true;
		}

		private bool IsFullTextValid()
		{
			if (InputType == InputType.String)
				return true;

			if (InputType == InputType.Integer)
				return int.TryParse(this.Text, out _);

			if (InputType == InputType.Double)
				return double.TryParse(this.Text, out _);

			return true;
		}
	}
}
