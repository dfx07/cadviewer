using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CadViewer.Interfaces
{
	public partial class ToastControl : UserControl, IToast
	{
		private IToastService _toastService = null;

		public ToastControl(IToastService toastService)
		{
			_toastService = toastService;
		}

		protected virtual bool OnCreateToast(ref ToastData _toastData)
		{
			// This method can be overridden in derived classes to provide custom behavior when the toast is shown.
			return true;
		}

		protected virtual void OnClickedToast()
		{
			// This method can be overridden in derived classes to provide custom behavior when the toast is shown.
			return;
		}

		public bool OnCreateToast(ToastData _tastData)
		{
			return OnCreateToast(ref _tastData);
		}

		public void OnCloseToast()
		{
			if (_toastService != null)
				_toastService.CloseToast(this);
		}

		public void OnClickedToBoard()
		{
			OnClickedToast();
		}
	}

	public delegate void ToastEventHandler(ToastData toast);

	public class ToastManager
	{
		private Queue<(ToastData Data, ToastEventHandler Callback)> _toastQueues = null;
		private DispatcherTimer _timer;
		public ToastManager()
		{
			_toastQueues = new Queue<(ToastData Data, ToastEventHandler Callback)>();

			_timer = new DispatcherTimer();
			_timer.Interval = TimeSpan.FromMilliseconds(100);
			_timer.Tick += InternalTimer_Tick;
		}

		public void Plush()
		{
			_timer.Stop();
			_timer.Tick -= InternalTimer_Tick;
		}

		private void InternalTimer_Tick(object sender, EventArgs e)
		{
			if (IsEmpty())
			{
				_timer.Stop();
				return;
			}

			var itToast = Pop();

			itToast.Callback?.Invoke(itToast.Data);
		}

		public bool IsEmpty()
		{
			return _toastQueues.Count == 0;
		}

		public void Push(ToastData data, ToastEventHandler callback)
		{
			_toastQueues.Enqueue((data, callback));
			if(!_timer.IsEnabled)
			{
				_timer.Start();
			}
		}

		public (ToastData Data, ToastEventHandler Callback) Pop()
		{
			if (_toastQueues.Count > 0)
			{
				return _toastQueues.Dequeue();
			}

			return (null, null);
		}
	};
}
