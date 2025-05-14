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
		private Popup _Popup = null;
		private Window _ParentWindow = null;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_DropDownButton = GetTemplateChild("PART_DropDownButton") as ToggleButton;
			_ContentBound = GetTemplateChild("PART_Content_Bound") as Border;
			_Popup = GetTemplateChild("PART_Popup") as Popup;

			if (_Popup != null)
			{
				_Popup.Opened += (s, e) =>
				{
					_ParentWindow = Window.GetWindow(this);
					if (_ParentWindow != null)
					{
						_ParentWindow.LocationChanged += OnWindowChanged;
						_ParentWindow.SizeChanged += OnWindowChanged;
						_ParentWindow.StateChanged += OnWindowChanged;
					}
				};

				_Popup.Closed += (s, e) =>
				{
					UpdateVisualState();

					if (_ParentWindow != null)
					{
						_ParentWindow.LocationChanged -= OnWindowChanged;
						_ParentWindow.SizeChanged -= OnWindowChanged;
						_ParentWindow.StateChanged -= OnWindowChanged;
						_ParentWindow = null;
					}
				};
			}

			Application.Current.Deactivated += (s, e) =>
			{
				if (_Popup.IsOpen)
					_Popup.IsOpen = false;
			};

			Application.Current.MainWindow.PreviewMouseLeftButtonDown += (s, e) =>
			{
				if (_Popup.IsOpen && !IsClickInside(_Popup, e))
				{
					_Popup.IsOpen = false;
				}
			};

			Loaded += (s, e) =>
			{
				_DropDownButton.MouseLeave += DropDownButton_MouseLeave;
				_ContentBound.MouseEnter += ContentBound_MouseEnter;
				_ContentBound.MouseLeave += ContentBound_MouseLeave;
			};
		}

		private void OnWindowChanged(object sender, EventArgs e)
		{
			if (_Popup != null && _Popup.IsOpen)
			{
				_Popup.IsOpen = false;
			}
		}

		private bool IsClickInside(Popup popup, MouseButtonEventArgs e)
		{
			var clickedElement = Mouse.DirectlyOver as DependencyObject;
			while (clickedElement != null)
			{
				if (clickedElement == popup.Child)
					return true;

				clickedElement = VisualTreeHelper.GetParent(clickedElement);
			}

			return false;
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
	}
}
