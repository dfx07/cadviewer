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

namespace CadViewer.ViewModels
{
	public class ViewModelBase : NotifyPropertyChanged
	{
		public ViewModelBase()
		{
			m_pViewModelPtr = new GCHandleProvider(this);
		}

		public IntPtr GetPointer()
		{
			if (m_pViewModelPtr is null)
				return IntPtr.Zero;

			return m_pViewModelPtr.Pointer;
		}

		protected GCHandleProvider m_pViewModelPtr = null;
	}
}
