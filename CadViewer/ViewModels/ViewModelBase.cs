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
using CadViewer.Interfaces;
using CadViewer.Services;

namespace CadViewer.ViewModels
{
	public abstract class ViewModelBase : NotifyPropertyChanged
	{
		public ViewModelBase()
		{
			Messenger = VmMessenger.Instance;
			m_pViewModelPtr = new GCHandleProvider(this);
		}

		public IntPtr GetPointer()
		{
			if (m_pViewModelPtr is null)
				return IntPtr.Zero;

			return m_pViewModelPtr.Pointer;
		}

		protected GCHandleProvider m_pViewModelPtr = null;
		protected IMessenger Messenger { get; } = null;
	}
}
