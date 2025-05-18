using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CadViewer.Interfaces
{
	public class ToastControl : UserControl, IToast
	{
		private IDialogService _dlgService = null;

		public ToastControl(IDialogService dlgService)
		{
			_dlgService = dlgService;
		}

		protected virtual bool OnShowToast(string title, string msg, EToastMessageType type, EToastMessagePosition position, int duration)
		{
			// This method can be overridden in derived classes to provide custom behavior when the toast is shown.
			return true;
		}

		public void ShowToast(string title, string msg, EToastMessageType type, EToastMessagePosition position, int duration)
		{
			if(OnShowToast(title, msg, type, position, duration) == false)
				return;

			if (_dlgService != null)
				_dlgService.ShowToast(this);
		}

		public void CloseToast()
		{
			if (_dlgService != null)
				_dlgService.CloseToast(this);
		}
	}

	public delegate void ToastEventHandler(IToast toast);

	public class ToastManager
	{
		private readonly Queue<IToast> _toastQueues = new Queue<IToast>();
		private readonly DispatcherTimer _timer;
		public event ToastEventHandler _beginToastHandler;
		public event ToastEventHandler _endToastHandler;
		public ToastManager()
		{
			_timer = new DispatcherTimer();
			_timer.Interval = TimeSpan.FromMilliseconds(300);
			_timer.Tick += InternalTimer_Tick;
		}

		public void BeginToastEvent(ToastEventHandler _event) { _beginToastHandler = _event; }
		private void InternalTimer_Tick(object sender, EventArgs e)
		{
			if (IsEmpty())
				return;

			IToast toast = Pop();

			_beginToastHandler?.Invoke(toast);
		}

		public bool IsEmpty()
		{
			return _toastQueues.Count == 0;
		}

		public void Push(IToast toast)
		{
			_toastQueues.Enqueue(toast);
			if(!_timer.IsEnabled)
			{
				_timer.Start();
			}
		}

		public IToast Pop()
		{
			if (_toastQueues.Count > 0)
			{
				return _toastQueues.Dequeue();
			}

			return null;
		}
	};
}
