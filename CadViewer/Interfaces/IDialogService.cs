using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Interfaces
{
	public interface IModalDialog
	{

	}

	public interface IDialogService
	{
		int ShowModal(IModalDialog dialog);
		void CloseModal();
	}
}
