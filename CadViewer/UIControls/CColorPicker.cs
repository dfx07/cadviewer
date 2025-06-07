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
	public class CColorPicker : ContentControl
	{
		static CColorPicker()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CColorPicker),
				new FrameworkPropertyMetadata(typeof(CColorPicker)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (GetTemplateChild("PART_Button") is Button button)
			{
				button.Click += ChooseColor;
			}

			UpdateDisplay();

			Loaded += (s, e) =>
			{

			};
		}

		private void ChooseColor(object sender, RoutedEventArgs e)
		{
			var dialog = new System.Windows.Forms.ColorDialog();
			dialog.Color = System.Drawing.Color.FromArgb(SelectedColor.A, SelectedColor.R, SelectedColor.G, SelectedColor.B);
			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				SelectedColor = Color.FromArgb(dialog.Color.A, dialog.Color.R, dialog.Color.G, dialog.Color.B);

				UpdateDisplay();
			}
		}

		private void UpdateDisplay()
		{
			if (GetTemplateChild("PART_ColorDisplay") is Border display)
			{
				display.Background = new SolidColorBrush(SelectedColor);
			}
		}

		public static readonly DependencyProperty SelectedColorProperty =
		DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(CColorPicker), new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public Color SelectedColor
		{
			get => (Color)GetValue(SelectedColorProperty);
			set => SetValue(SelectedColorProperty, value);
		}
	}
}
