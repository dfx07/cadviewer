using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CadViewer.Common
{
	public static class MouseMoveBehavior
	{
		public static ICommand GetMouseMoveCommand(UIElement element) =>
			EventBehavior<MouseEventArgs>.GetEventCommand(element);

		public static void SetMouseMoveCommand(UIElement element, ICommand value) =>
			EventBehavior<MouseEventArgs>.SetEventCommand(element, value);
	}
	public static class MouseEnterBehavior
	{
		public static ICommand GetMouseEnterCommand(UIElement element) =>
			EventBehavior<MouseEventArgs>.GetEventCommand(element);

		public static void SetMouseEnterCommand(UIElement element, ICommand value) =>
			EventBehavior<MouseEventArgs>.SetEventCommand(element, value);
	}
}
