using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CadViewer.Common
{
	public static class EventBehavior<TEventArgs> where TEventArgs : EventArgs
	{
		public static readonly DependencyProperty EventCommandProperty =
			DependencyProperty.RegisterAttached(
				"EventCommand",
				typeof(ICommand),
				typeof(EventBehavior<TEventArgs>),
				new PropertyMetadata(null, OnEventCommandChanged));

		public static ICommand GetEventCommand(DependencyObject obj) =>
			(ICommand)obj.GetValue(EventCommandProperty);

		public static void SetEventCommand(DependencyObject obj, ICommand value) =>
			obj.SetValue(EventCommandProperty, value);

		private static void OnEventCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is UIElement element)
			{
				RemoveEventHandler(element);

				// assgin new event
				if (e.NewValue is ICommand)
				{
					AddEventHandler(element);
				}
			}
		}

		private static void AddEventHandler(UIElement element)
		{
			var eventName = typeof(TEventArgs).Name.Replace("EventArgs", "");
			var eventInfo = element.GetType().GetEvent(eventName);
			if (eventInfo != null)
			{
				var handler = new EventHandler<TEventArgs>((sender, args) =>
				{
					var command = GetEventCommand(element);
					if (command?.CanExecute(args) == true)
					{
						command.Execute(args);
					}
				});

				eventInfo.AddEventHandler(element, handler);
				element.SetValue(EventHandlerProperty, handler);
			}
		}

		private static void RemoveEventHandler(UIElement element)
		{
			var handler = element.GetValue(EventHandlerProperty) as Delegate;
			if (handler != null)
			{
				var eventName = typeof(TEventArgs).Name.Replace("EventArgs", "");
				var eventInfo = element.GetType().GetEvent(eventName);
				eventInfo?.RemoveEventHandler(element, handler);
				element.ClearValue(EventHandlerProperty);
			}
		}

		private static readonly DependencyProperty EventHandlerProperty =
			DependencyProperty.RegisterAttached(
				"EventHandler",
				typeof(Delegate),
				typeof(EventBehavior<TEventArgs>),
				new PropertyMetadata(null));
	}
}
