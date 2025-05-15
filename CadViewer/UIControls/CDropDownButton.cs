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
	public enum EDropDownButtonFlowDirection
	{
		Horizontal = 0,
		Vertical = 1
	}

	public class CDropDownButton : ToggleButton
	{
		static CDropDownButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CDropDownButton),
				new FrameworkPropertyMetadata(typeof(CDropDownButton)));
		}

		public CDropDownButton()
		{
			
		}

		private ToggleButton _DropDownButton = null;
		private Border _ContentBound = null;
		private CFlexPopup _Popup = null;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_DropDownButton = GetTemplateChild("PART_DropDownButton") as ToggleButton;
			_ContentBound = GetTemplateChild("PART_Content_Bound") as Border;
			_Popup = GetTemplateChild("PART_Popup") as CFlexPopup;

			if (_Popup != null)
			{
				_Popup.Closed += (s, e) =>
				{
					UpdateVisualState();
				};
			}

			Loaded += (s, e) =>
			{
				_DropDownButton.MouseLeave += DropDownButton_MouseLeave;
				_ContentBound.MouseEnter += ContentBound_MouseEnter;
				_ContentBound.MouseLeave += ContentBound_MouseLeave;
			};

			if (ButtonFlowDirection == EDropDownButtonFlowDirection.Vertical)
			{
				DropDownButtonSize = 15;
			}
		}

		private void UpdateVisualState()
		{
			if (_DropDownButton.IsChecked == true)
			{
				VisualStateManager.GoToState(_DropDownButton, "_NormalChecked", true);
			}
			else
			{
				VisualStateManager.GoToState(_DropDownButton, "_Normal", true);
			}
		}
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			if (IsDropDownOpen)
				return;

			base.OnMouseLeftButtonDown(e);

			VisualStateManager.GoToState(this, "AllPressed", true);
		}

		private void ContentBound_MouseEnter(object sender, MouseEventArgs e)
		{
			VisualStateManager.GoToState(this, "AllMouseOver", true);
		}

		private void ContentBound_MouseLeave(object sender, MouseEventArgs e)
		{
			VisualStateManager.GoToState(this, "Normal", true);
		}

		private void DropDownButton_MouseEnter(object sender, MouseEventArgs e)
		{
			VisualStateManager.GoToState(this, "Normal", true);
		}
		private void DropDownButton_MouseLeave(object sender, MouseEventArgs e) => UpdateVisualState();


		public static readonly DependencyProperty IsDropDownOpenProperty =
		DependencyProperty.Register(nameof(IsDropDownOpen), typeof(bool), typeof(CDropDownButton), new PropertyMetadata(false));
		public bool IsDropDownOpen
		{
			get => (bool)GetValue(IsDropDownOpenProperty);
			set => SetValue(IsDropDownOpenProperty, value);
		}

		public static readonly DependencyProperty DropDownContentProperty =
		DependencyProperty.Register(nameof(DropDownContent), typeof(object), typeof(CDropDownButton));
		public object DropDownContent
		{
			get => GetValue(DropDownContentProperty);
			set => SetValue(DropDownContentProperty, value);
		}

		public static readonly DependencyProperty ButtonFlowDirectionProperty =
		DependencyProperty.Register(nameof(ButtonFlowDirection), typeof(EDropDownButtonFlowDirection), typeof(CDropDownButton),
			new PropertyMetadata(EDropDownButtonFlowDirection.Horizontal));

		public EDropDownButtonFlowDirection ButtonFlowDirection
		{
			get => (EDropDownButtonFlowDirection)GetValue(ButtonFlowDirectionProperty);
			set => SetValue(ButtonFlowDirectionProperty, value);
		}

		public static readonly DependencyProperty DropDownButtonSizeProperty =
		DependencyProperty.Register(nameof(DropDownButtonSize), typeof(double), typeof(CDropDownButton), new PropertyMetadata(20.0));
		public double DropDownButtonSize
		{
			get => (double)GetValue(DropDownButtonSizeProperty);
			set => SetValue(DropDownButtonSizeProperty, value);
		}
	}
}
