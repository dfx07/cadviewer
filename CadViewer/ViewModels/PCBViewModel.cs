using CadViewer.API;
using CadViewer.Common;
using CadViewer.Components;
using CadViewer.Interfaces;
using CadViewer.View;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace CadViewer.ViewModels
{
	public class PCBViewModel : ViewModelBase, PCBViewNotify
	{
		public PCBViewModel()
		{
			OpenGLControl = new OpenGLHost();

			MouseMoveCommand = new RelayCommand<MouseEventArgs>(OnMouseMove);
			MouseEnterCommand = new RelayCommand<MouseEventArgs>(OnMouseEnter);
			MouseDragDropCommand = new RelayCommand<MouseDragDropEventArgs>(OnMouseDragDrop);
			MouseDownCommand = new RelayCommand<MouseButtonEventArgs>(OnMouseDown);
			MouseUpCommand = new RelayCommand<MouseButtonEventArgs>(OnMouseUp);
			MouseWheelCommand = new RelayCommand<MouseWheelEventArgs>(OnMouseWheel);

			KeyDownCommand = new RelayCommand<KeyEventArgs>(OnKeyDown);
			KeyUpCommand = new RelayCommand<KeyEventArgs>(OnKeyUp);
		}

		~PCBViewModel()
		{

		}

		public void InitContext()
		{
			if (_pCBViewHandler == null || OpenGLControl.Hwnd == IntPtr.Zero)
				return;

			_pCBViewHandler.OnCreateContext(OpenGLControl);
		}

		public void OnViewChanged(int nWidth, int nHeight)
		{
			if (_pCBViewHandler is null)
				return;

			if(OpenGLControl != null)
			{
				OpenGLControl.Width = nWidth;
				OpenGLControl.Height = nHeight;

				OpenGLControl.UpdateLayout();
			}

			_pCBViewHandler.OnViewChanged(nWidth, nHeight);

			Width = nWidth;
			Height = nHeight;
		}

		private void OnMouseMove(MouseEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_MOVE))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			var position = e.GetPosition((UIElement)e.Source);
			_pCBViewHandler.OnMouseMove(position);
		}

		private void OnMouseEnter(MouseEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_ENTER))
				return;

			if (_pCBViewHandler is null)
				return;

			_pCBViewHandler.OnMouseEnter();
		}
		private void OnMouseDragDrop(MouseDragDropEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_DRAG))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			var position = e.GetPosition((UIElement)e.Source);

			_pCBViewHandler.OnMouseDragDrop(e.State, position);
		}

		private void OnMouseDown(MouseButtonEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_DOWN))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			var position = e.GetPosition((UIElement)e.Source);

			if(e.ChangedButton == MouseButton.Left)
			{
				_pCBViewHandler.OnMouseDown(MouseButton.Left, position);
			}
			else if(e.ChangedButton == MouseButton.Right)
			{
				_pCBViewHandler.OnMouseDown(MouseButton.Right, position);
			}
			else if(e.ChangedButton == MouseButton.Middle)
			{
				_pCBViewHandler.OnMouseDown(MouseButton.Middle, position);
			}
		}

		private void OnMouseUp(MouseButtonEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_UP))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			var position = e.GetPosition((UIElement)e.Source);

			if (e.ChangedButton == MouseButton.Left)
			{
				_pCBViewHandler.OnMouseUp(MouseButton.Left, position);
			}
			else if (e.ChangedButton == MouseButton.Right)
			{
				_pCBViewHandler.OnMouseUp(MouseButton.Right, position);
			}
			else if (e.ChangedButton == MouseButton.Middle)
			{
				_pCBViewHandler.OnMouseUp(MouseButton.Middle, position);
			}
		}

		private void OnMouseWheel(MouseWheelEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_WHEEL))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			var position = e.GetPosition((UIElement)e.Source);

			_pCBViewHandler.OnMouseWheel(e.Delta, position);
		}

		/*Keyboard events*/
		private void OnKeyDown(KeyEventArgs k)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.KEY_DOWN))
				return;

			if (!_isKeyDownHandled)
			{
				_pCBViewHandler.OnKeyDown(k.Key);
				_isKeyDownHandled = true;
			}
		}
		private void OnKeyUp(KeyEventArgs k)
		{
			_isKeyDownHandled = false;

			if (!CanExecuteEvents(EnumPCBViewEvent.KEY_UP))
				return;

			_pCBViewHandler.OnKeyUp(k.Key);
		}

		#region /// [Internal handle]
		// ------------------------------------------------------------------------------//

		private bool CanExecuteEvents(EnumPCBViewEvent e)
		{
			if(_excludeEvents == 0)
				return true;

			if ((_excludeEvents & (Int64)e) > 0L)
				return false;

			return true;
		}

		private void SetDisableEvents(Int64 events)
		{
			_excludeEvents &= events;
		}
		private void SetEnableEvents(Int64 events)
		{
			_excludeEvents |= ~events;
		}

		#endregion

		#region [Handle PCBViewNotifier]
		// ------------------------------------------------------------------------------//
		public void SetTitle(string strTitle)
		{
			Name = strTitle;
		}

		public void SetVisibleTitle(bool bShow)
		{
			TitleVisibility = bShow ? Visibility.Visible : Visibility.Collapsed;
		}
		public int SendToUI(EnumPCBViewMsg msg, int lParam, int wParam)
		{
			switch(msg)
			{
				case EnumPCBViewMsg.SET_TITLE_MSG:
					SetTitle(lParam.ToString());
					break;

				case EnumPCBViewMsg.DISABLE_EVENTS:
					SetDisableEvents(lParam);
					break;

				case EnumPCBViewMsg.ENABLE_EVENTS:
					SetEnableEvents(lParam);
					break;
			}

			return 1;
		}

		#endregion

		#region [Internal handle]
		// ------------------------------------------------------------------------------//
		public void SetHandler(PCBViewHandler handler)
		{
			_pCBViewHandler = handler;
			handler.SetPCBViewNotifier(this);
		}

		public PCBViewHandler GetHandler()
		{
			return _pCBViewHandler;
		}

		#endregion

		private Int64 _excludeEvents = 0;

		private PCBViewHandler _pCBViewHandler { get; set; }

		public ICommand MouseMoveCommand { get; set; }
		public ICommand MouseEnterCommand { get; set; }
		public ICommand MouseDownCommand { get; set; }
		public ICommand MouseUpCommand { get; set; }
		public ICommand MouseDragDropCommand { get; set; }
		public ICommand KeyDownCommand { get; set; }
		public ICommand KeyUpCommand { get; set; }
		public ICommand MouseWheelCommand { get; set; }

		private bool _isKeyDownHandled = false;
		private OpenGLHost _openGLControl; public OpenGLHost OpenGLControl { get => _openGLControl; set => SetProperty(ref _openGLControl, value); }
		private string _name; public string Name { get => _name; set => SetProperty(ref _name, value); }
		private Visibility _titleVisibility; public Visibility TitleVisibility { get => _titleVisibility; set => SetProperty(ref _titleVisibility, value); }
		private double _width; public double Width { get => _width; set => SetProperty(ref _width, value); }
		private double _height; public double Height { get => _height; set => SetProperty(ref _height, value); }
	}
}
