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
		int ShowModal(IModalDialog dialog);
		void CloseModal();
	}
}
