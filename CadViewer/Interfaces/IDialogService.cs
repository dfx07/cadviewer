using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CadViewer.Interfaces
{
	public enum EToastMessageType
	{
		None,
		Info,
		Warning,
		Error
	}
	public enum EToastMessagePosition
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	public interface IToast
	{
		void ShowToast(string title, string msg, EToastMessageType type, EToastMessagePosition position, int duration = 3000);
		void CloseToast();
	}

	public interface IModalDialog
	{
		void OnClose();
		void OnOpen();
	}

	public interface IDialogService
	{
		void ShowToast(IToast toast);
		void CloseToast(IToast toast);
		void ShowProgress(string progressMsg);
		int ShowModal(IModalDialog dialog);
		void CloseModal(IModalDialog dialog);
	}
}
