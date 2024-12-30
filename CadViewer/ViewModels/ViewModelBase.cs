using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CadViewer.API;
using CadViewer.Common;

namespace CadViewer.ViewModels
{
	public class ViewModelBase : NotifyPropertyChangedBase
	{
		public ViewModelBase()
		{
			m_pBaseHandle = new GCHandleProvider(this);
			m_pCallbackUI = new CallbackFunctionNotifyUI(this.OnNotifyUI);
		}

		public void SetCallbackUI(CallbackFunctionNotifyUI pcall)
		{
			
		}

		public virtual void OnNotifyUI(string message, int nParam, int nWaram)
		{
			// TODO: Implement
		}
		public IntPtr GetHandlePointer()
		{
			if (m_pBaseHandle is null)
				return IntPtr.Zero;

			return m_pBaseHandle.Pointer;
		}

		protected GCHandleProvider m_pBaseHandle = null;
		public CallbackFunctionNotifyUI m_pCallbackUI = null;
		public IntPtr m_pViewModelBase = IntPtr.Zero;
	}
}
