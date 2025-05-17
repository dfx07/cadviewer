using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CadViewer.Interfaces
{
	public interface IModalDialog
	{
		void OnClose();
		void OnOpen();
	}

	public interface IDialogService
	{
		void ShowProgress(string progressMsg);
		int ShowModal(IModalDialog dialog);
		void CloseModal(IModalDialog dialog);
	}
}
