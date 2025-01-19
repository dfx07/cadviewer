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
			m_pCallbackUI = new CallbackFunctionNotifyUI(this.OnNotifyHandle);
		}

		public virtual void OnNotifyHandle(string message, int nParam, int nWaram)
		{
			// TODO: Implement
		}

		public IntPtr m_pViewModel = IntPtr.Zero; // pointer in C++
		public CallbackFunctionNotifyUI m_pCallbackUI = null;
	}
}
