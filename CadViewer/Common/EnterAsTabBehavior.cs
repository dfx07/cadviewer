using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace CadViewer.Common
{
	public static class EnterAsTabBehavior
	{
		public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);
		public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.RegisterAttached(
				"IsEnabled",
				typeof(bool),
				typeof(EnterAsTabBehavior),
				new UIPropertyMetadata(false, OnIsEnabledChanged));

		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is UIElement element)
			{
				if ((bool)e.NewValue)
					element.PreviewKeyDown += Element_PreviewKeyDown;
				else
					element.PreviewKeyDown -= Element_PreviewKeyDown;
			}
		}

		private static void Element_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				e.Handled = true;
				(sender as UIElement)?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
			}
		}
	}
}
