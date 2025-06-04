using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Interfaces
{
	public interface IMessenger
	{
		void Send<TMessage>(TMessage message);
		void Register<TMessage>(object recipient, Action<TMessage> handler);
		void Unregister<TMessage>(object recipient);
	}
}
