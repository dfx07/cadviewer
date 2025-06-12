using CadViewer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Interfaces
{
	interface INotificationHandler
	{
		void OnHandleNotify(string message, int nParam, int nWaram);
	}

	public class NotificationHandler : INotificationHandler
	{
		public NotificationHandler()
		{
			m_pCallbackUI = new CallbackFunctionNotifyUI(this.OnHandleNotify);
		}

		public virtual void OnHandleNotify(string message, int nParam, int nWaram)
		{
			//TODO : implement
		}

		public IntPtr m_pHandler = IntPtr.Zero; // pointer in C++
		public CallbackFunctionNotifyUI m_pCallbackUI = null;
	}
}
