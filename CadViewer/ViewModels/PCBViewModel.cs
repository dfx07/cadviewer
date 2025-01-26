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
			OpenGLViewPanel = new ViewPanel();

			MouseMoveCommand = new RelayCommand<XMouseEventArgs>(OnMouseMove);
			MouseEnterCommand = new RelayCommand<XMouseEventArgs>(OnMouseEnter);
			MouseDragDropCommand = new RelayCommand<XMouseDragDropEventArgs>(OnMouseDragDrop);
			MouseDownCommand = new RelayCommand<XMouseButtonEventArgs>(OnMouseDown);
			MouseUpCommand = new RelayCommand<XMouseButtonEventArgs>(OnMouseUp);
			MouseWheelCommand = new RelayCommand<XMouseWheelEventArgs>(OnMouseWheel);

			KeyDownCommand = new RelayCommand<XKeyEventArgs>(OnKeyDown);
			KeyUpCommand = new RelayCommand<XKeyEventArgs>(OnKeyUp);

			ViewSizeChangedCommand = new RelayCommand<Size>(OnViewSizeChanged);
			ViewCreatedCommand = new RelayCommand<IntPtr>(OnViewCreated);
			ViewUpdateCommand = new RelayCommand(OnViewUpdate);
		}

		~PCBViewModel()
		{

		}

		public void OnInitModel()
		{
			//TODO: Implement
		}

		private void OnMouseMove(XMouseEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_MOVE))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			_pCBViewHandler.OnMouseMove(e.Pt);
		}

		private void OnMouseEnter(XMouseEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_ENTER))
				return;

			if (_pCBViewHandler is null)
				return;

			_pCBViewHandler.OnMouseEnter();
		}
		private void OnMouseDragDrop(XMouseDragDropEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_DRAG))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			_pCBViewHandler.OnMouseDragDrop(e.State, e.Pt);
		}

		private void OnMouseDown(XMouseButtonEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_DOWN))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			if(e.Button == MouseButton.Left)
			{
				_pCBViewHandler.OnMouseDown(MouseButton.Left, e.Pt);
			}
			else if(e.Button == MouseButton.Right)
			{
				_pCBViewHandler.OnMouseDown(MouseButton.Right, e.Pt);
			}
			else if(e.Button == MouseButton.Middle)
			{
				_pCBViewHandler.OnMouseDown(MouseButton.Middle, e.Pt);
			}
		}
		private void OnMouseUp(XMouseButtonEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_UP))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			if (e.Button == MouseButton.Left)
			{
				_pCBViewHandler.OnMouseUp(MouseButton.Left, e.Pt);
			}
			else if (e.Button == MouseButton.Right)
			{
				_pCBViewHandler.OnMouseUp(MouseButton.Right, e.Pt);
			}
			else if (e.Button == MouseButton.Middle)
			{
				_pCBViewHandler.OnMouseUp(MouseButton.Middle, e.Pt);
			}
		}
		private void OnMouseWheel(XMouseWheelEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_WHEEL))
				return;

			if (e is null || _pCBViewHandler is null)
				return;

			_pCBViewHandler.OnMouseWheel(e.Delta, e.Pt);
		}

		/*Keyboard events*/
		private void OnKeyDown(XKeyEventArgs k)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.KEY_DOWN))
				return;

			if (!_isKeyDownHandled)
			{
				_pCBViewHandler.OnKeyDown(k.Key);
				_isKeyDownHandled = true;
			}
		}
		private void OnKeyUp(XKeyEventArgs k)
		{
			_isKeyDownHandled = false;

			if (!CanExecuteEvents(EnumPCBViewEvent.KEY_UP))
				return;

			_pCBViewHandler.OnKeyUp(k.Key);
		}

		private void OnViewSizeChanged(Size newSize)
		{
			Width = newSize.Width;
			Height = newSize.Height;

			if (_pCBViewHandler is null)
				return;

			_pCBViewHandler.OnViewChanged((int)newSize.Width, (int)newSize.Height);
		}

		private void OnViewCreated(IntPtr handle)
		{
			if (_pCBViewHandler is null)
				return;

			_pCBViewHandler.OnCreateContext(handle);
		}

		private void OnViewUpdate()
		{
			if (_pCBViewHandler is null)
				return;

			_pCBViewHandler.OnViewUpdate();
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
		public ICommand ViewSizeChangedCommand { get; set; }
		public ICommand ViewCreatedCommand { get; set; }
		public ICommand ViewUpdateCommand { get; set; }

		private bool _isKeyDownHandled = false;
		private ViewPanel _openGLViewPanel; public ViewPanel OpenGLViewPanel { get => _openGLViewPanel; set => SetProperty(ref _openGLViewPanel, value); }
		private string _name; public string Name { get => _name; set => SetProperty(ref _name, value); }
		private Visibility _titleVisibility; public Visibility TitleVisibility { get => _titleVisibility; set => SetProperty(ref _titleVisibility, value); }
		private double _width; public double Width { get => _width; set => SetProperty(ref _width, value); }
		private double _height; public double Height { get => _height; set => SetProperty(ref _height, value); }
	}
}
