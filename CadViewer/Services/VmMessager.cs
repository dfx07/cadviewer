using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Services
{
	public class MessageParam
	{
		public string MessageID { get; set; }
		public object Sender { get; set; }
	}


	public class VmMessenger : IMessenger
	{
		private static readonly Lazy<VmMessenger> _instance = new Lazy<VmMessenger>(() => new VmMessenger());

		public static VmMessenger Instance => _instance.Value;


		private readonly Dictionary<Type, List<WeakReference>> _recipients = new Dictionary<Type, List<WeakReference>>();

		private VmMessenger()
		{
			// Private constructor to prevent instantiation from outside
		}

		public void Send<TMessage>(TMessage message)
		{
			var messageType = typeof(TMessage);

			if (_recipients.TryGetValue(messageType, out var recipients))
			{
				// Browse recipients in reverse order to avoid issues with collection modification
				for (int i = recipients.Count - 1; i >= 0; i--)
				{
					if (recipients[i].IsAlive)
					{
						if (recipients[i].Target is Action<TMessage> action)
							action(message);
					}
					else
					{
						// Delete weak reference if it is not alive
						recipients.RemoveAt(i);
					}
				}
			}
		}

		public void Register<TMessage>(object recipient, Action<TMessage> action)
		{
			var messageType = typeof(TMessage);
			if (!_recipients.TryGetValue(messageType, out var recipients))
			{
				recipients = new List<WeakReference>();
				_recipients[messageType] = recipients;
			}
			recipients.Add(new WeakReference(action));
		}

		public void Unregister<TMessage>(object recipient)
		{
			var messageType = typeof(TMessage);
			if (_recipients.TryGetValue(messageType, out var recipients))
			{
				recipients.RemoveAll(wr =>
				{
					if (wr.Target is Action<TMessage> action)
					{
						return true;
					}
					return false;
				});
			}
		}
	}
}
