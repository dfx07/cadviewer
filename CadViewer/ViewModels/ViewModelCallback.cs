using CadViewer.API;
using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.ViewModels
{
	public class ViewModelCallback : ViewModelBase, INotificationHandler
	{
		public ViewModelCallback()
		{
			m_pCallbackUI = new CallbackFunctionNotifyUI(this.OnHandleNotify);
		}

		public virtual void OnHandleNotify(string message, int nParam, int nWaram)
		{
			// TODO: Implement
		}

		public IntPtr m_pHandler = IntPtr.Zero; // pointer in C++
		public CallbackFunctionNotifyUI m_pCallbackUI = null;
	}
}
