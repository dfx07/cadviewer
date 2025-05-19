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
		Warn,
		Error,
		Success,
	}
	public enum EToastMessagePosition
	{
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	public class ToastData
	{
		public string Title { get; set; }
		public string Message { get; set; }
		public EToastMessageType ToastType { get; set; } // Info, Success, Error, etc.
		public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(3);
	}

	public interface IToast
	{
		bool OnCreateToast(ToastData _tastData);
		void OnCloseToast();
	}

	public interface IModalDialog
	{
		void OnClose();
		void OnOpen();
	}

	public interface IToastService
	{
		void ShowToast(ToastData tData);
		void CloseToast(IToast toast);
	}

	public interface IDialogService
	{
		void ShowProgress(string progressMsg);
		int ShowModal(IModalDialog dialog);
		void CloseModal(IModalDialog dialog);
	}
}
