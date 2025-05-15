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
using System.Windows.Documents;
using System.IO;
using System.Xml;
using System.Windows.Markup;
using System.ComponentModel;

namespace CadViewer.UIControls
{
	public enum EFlexPopupOpenMode
	{
		AlwaysOpen,// ~ StaysOpen = true
		AutoClose, // ~ StaysOpen = false
		AutoClose_AllowEvent
	}

	public class CFlexPopup : Popup
	{
		private Window _ParentWindow = null;

		static CFlexPopup()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CFlexPopup), new FrameworkPropertyMetadata(typeof(CFlexPopup)));
			IsOpenProperty.OverrideMetadata(typeof(CFlexPopup), new FrameworkPropertyMetadata(false, OnIsOpenChanged));
		}

		public CFlexPopup()
		{
			Loaded += (s, e) =>
			{
				ApplyOpenMode();
			};
		}

		public void ApplyOpenMode()
		{
			Application.Current.Deactivated -= AutoClosePopupTrigger_Window;
			Application.Current.MainWindow.PreviewMouseLeftButtonDown -= AutoClosePopupTrigger_Outside;

			if (OpenMode == EFlexPopupOpenMode.AutoClose)
			{
				StaysOpen = false;
			}
			else if (OpenMode == EFlexPopupOpenMode.AlwaysOpen)
			{
				StaysOpen = true;
			}
			else
			{
				StaysOpen = true;

				Application.Current.Deactivated += AutoClosePopupTrigger_Window;
				Application.Current.MainWindow.PreviewMouseLeftButtonDown += AutoClosePopupTrigger_Outside;
			}
		}

		private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is CFlexPopup popup)
			{
				if(popup.OpenMode == EFlexPopupOpenMode.AutoClose_AllowEvent)
				{
					if ((bool)e.NewValue)
						popup.OnBaseOpened(popup, EventArgs.Empty);
					else
						popup.OnBaseClosed(popup, EventArgs.Empty);
				}
			}
		}

		public void AutoClosePopupTrigger_Window(object sender, EventArgs e)
		{
			if (IsOpen)
				IsOpen = false;
		}

		public void AutoClosePopupTrigger_Outside(object sender, EventArgs e)
		{
			if (IsOpen && !IsClickInside(this, (MouseButtonEventArgs)e))
			{
				IsOpen = false;
			};
		}

		public void OnBaseOpened(object sender, EventArgs e)
		{
			_ParentWindow = Window.GetWindow(this);
			if (_ParentWindow != null)
			{
				_ParentWindow.LocationChanged += AutoClosePopupTrigger_Window;
				_ParentWindow.SizeChanged += AutoClosePopupTrigger_Window;
				_ParentWindow.StateChanged += AutoClosePopupTrigger_Window;
			}
		}

		public void OnBaseClosed(object sender, EventArgs e)
		{
			if (_ParentWindow != null)
			{
				_ParentWindow.LocationChanged -= AutoClosePopupTrigger_Window;
				_ParentWindow.SizeChanged -= AutoClosePopupTrigger_Window;
				_ParentWindow.StateChanged -= AutoClosePopupTrigger_Window;
				_ParentWindow = null;
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

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool StaysOpen
		{
			get => base.StaysOpen;
			set => base.StaysOpen = value;
		}

		private static void OnOpenModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is CFlexPopup popup)
			{
				popup.ApplyOpenMode();
			}
		}

		public static readonly DependencyProperty OpenModeProperty =
		DependencyProperty.Register(nameof(OpenMode), typeof(EFlexPopupOpenMode), typeof(CFlexPopup), 
			new PropertyMetadata(EFlexPopupOpenMode.AutoClose, OnOpenModeChanged));

		public EFlexPopupOpenMode OpenMode
		{
			get => (EFlexPopupOpenMode)GetValue(OpenModeProperty);
			set => SetValue(OpenModeProperty, value);
		}
	}
}
