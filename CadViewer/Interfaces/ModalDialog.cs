using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CadViewer.Interfaces
{
	public class ModalDialog : UserControl, IModalDialog
	{
		private IDialogService _dlgService = null;

		public ModalDialog(IDialogService dlgService)
		{
			_dlgService = dlgService;
		}
		public void OnClose()
		{
			if(_dlgService != null)
				_dlgService.CloseModal(this);
		}
		public void OnOpen()
		{
			throw new NotImplementedException();
		}
	}
}
