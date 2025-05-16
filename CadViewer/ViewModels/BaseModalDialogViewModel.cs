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
	public class BaseModalDialogViewModel : NotifyPropertyChanged
	{
		public BaseModalDialogViewModel()
		{
			
		}

		public void DoModal()
		{

		}

		virtual protected void OnDoModal()
		{

		}
	}
}
