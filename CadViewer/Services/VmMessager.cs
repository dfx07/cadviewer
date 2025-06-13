using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Services
{
	public class MessageArgs
	{
		public string MessageID { get; set; }
		public object Sender { get; set; }
	}

	public class ShapeActionMessageArgs : MessageArgs
	{
		public bool IsActive { get; set; }
		public string ShapeType { get; set; } // e.g., "Line", "Circle", etc.
		public string ActionType { get; set; } // e.g., "Create", "Update", "Delete"
	}

	public class HomeMenuActionMessageArgs : MessageArgs
	{
		public string Action { get; set; }
	}

	public class VmMessenger : IMessenger
	{
		private class StrongAction<TMessage>
		{
			public WeakReference Recipient { get; }
			private readonly Action<TMessage> _strongAction;

			public StrongAction(object recipient, Action<TMessage> action)
			{
				Recipient = new WeakReference(recipient);
				_strongAction = action;
			}

			public bool Invoke(TMessage message)
			{
				if (Recipient.IsAlive && _strongAction != null)
				{
					_strongAction(message);
					return true;
				}
				return false;
			}

			public bool IsMatch(object recipient)
			{
				return Recipient.Target == recipient;
			}

			public bool IsAlive => Recipient.IsAlive;
		}

		private static readonly Lazy<VmMessenger> _instance = new Lazy<VmMessenger>(() => new VmMessenger());
		public static VmMessenger Instance => _instance.Value;

		private readonly Dictionary<Type, List<object>> _recipients = new Dictionary<Type, List<object>>();

		private VmMessenger() { }

		public void Send<TMessage>(TMessage message)
		{
			var messageType = typeof(TMessage);
			if (_recipients.TryGetValue(messageType, out var list))
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					if (list[i] is StrongAction<TMessage> wa)
					{
						if (!wa.Invoke(message))
							list.RemoveAt(i);
					}
				}
			}
		}

		public void Register<TMessage>(object recipient, Action<TMessage> action)
		{
			var messageType = typeof(TMessage);
			if (!_recipients.TryGetValue(messageType, out var list))
			{
				list = new List<object>();
				_recipients[messageType] = list;
			}
			list.Add(new StrongAction<TMessage>(recipient, action));
		}

		public void Unregister<TMessage>(object recipient)
		{
			var messageType = typeof(TMessage);
			if (_recipients.TryGetValue(messageType, out var list))
			{
				list.RemoveAll(obj =>
					obj is StrongAction<TMessage> wa && wa.IsMatch(recipient));
			}
		}
	}
}
