using CadViewer.API;
using CadViewer.Common;
using CadViewer.Components;
using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
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

			KeyDownCommand = new RelayCommand<KeyEventArgs>(OnKeyDown);
			KeyUpCommand = new RelayCommand<KeyEventArgs>(OnKeyUp);
		}

		~PCBViewModel()
		{
			PCBViewerAPI.DestroyPCBView(m_pViewModelBase);
		}

		public void InitContext()
		{
			if(OpenGLControl != null)
			{
				m_pViewModelBase = PCBViewerAPI.CreatePCBView(OpenGLControl.Hwnd);
			}

			PCBViewerAPI.SetCallbackFunctionNotifyUI(m_pViewModelBase, m_pCallbackUI);

			ContextConfig ctxConfig = new ContextConfig
			{
				m_bUseContextExt = BaseAPI.TRUE,
				m_nAntialiasingLevel = 8,
			};

			if(PCBViewerAPI.CreateContext(m_pViewModelBase, ctxConfig) != BaseAPI.TRUE)
			{
				MessageBox.Show("Create OpenGL context FAIL");
			}
		}

		public void OnViewChanged(int nWidth, int nHeight)
		{
			Width = nWidth;
			Height = nHeight;

			PCBViewerAPI.SetView(m_pViewModelBase, (int)Width, (int)Height);

			PCBViewerAPI.Clear(m_pViewModelBase);
			PCBViewerAPI.Draw(m_pViewModelBase);
		}

		private void OnMouseMove(MouseEventArgs e)
		{
			var position = e.GetPosition((UIElement)e.Source);
			if (_pCBViewHandler == null)
				return;

			_pCBViewHandler.OnMouseMove(position);
		}

		private void OnMouseEnter(MouseEventArgs e)
		{
			if (_pCBViewHandler == null)
				return;

			_pCBViewHandler.OnMouseEnter();
		}
		private void OnMouseDragDrop(MouseDragDropEventArgs e)
		{
			var position = e.GetPosition((UIElement)e.Source);

			if(e.State == MouseDragDropState.Drag)
			{
				Logger.LogInfo("[Drag&Drop] drag. " + position.ToString());
			}
			else if(e.State == MouseDragDropState.Move)
			{
				Logger.LogInfo("[Drag&Drop] Move. " + position.ToString());
			}
			else if(e.State == MouseDragDropState.Drop)
			{
				Logger.LogInfo("[Drag&Drop] drop. " + position.ToString());
			}
		}

		private void OnMouseDown(MouseButtonEventArgs e)
		{
			var position = e.GetPosition((UIElement)e.Source);

			if (_pCBViewHandler == null)
				return;

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
			var position = e.GetPosition((UIElement)e.Source);

			if (_pCBViewHandler == null)
				return;

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

		/*Keyboard events*/
		private void OnKeyDown(KeyEventArgs k)
		{
			// Kiểm tra nếu sự kiện chưa được xử lý
			if (!_isKeyDownHandled)
			{
				// Xử lý sự kiện tại đây
				Logger.LogInfo("Key down. " + k.Key);

				// Đánh dấu sự kiện là đã được xử lý
				_isKeyDownHandled = true;
			}
		}

		private void OnKeyUp(KeyEventArgs k)
		{
			Logger.LogInfo("Key up. " + k.Key);
			_isKeyDownHandled = false;
		}

		public void DrawLine()
		{
			PCBViewerAPI.SetView(m_pViewModelBase, (int)Width, (int)Height);
			PCBViewerAPI.Clear(m_pViewModelBase);
			PCBViewerAPI.Draw(m_pViewModelBase);
		}

		public override void OnNotifyUI(string message, int nParam, int nWaram)
		{
			switch (message)
			{
				case "DrawLine":
					MessageBox.Show("User set : " + nParam);

					break;

				default:
					break;
			}
		}

		/// <summary>
		/// Handle PCBview handler
		/// </summary>
		public void SetTitle(string strTitle)
		{
			Name = strTitle;
		}

		public void SetHandler(PCBViewHandler handler)
		{
			_pCBViewHandler = handler;
			handler.SetPCBViewNotifier(this);
		}

		public PCBViewHandler GetHandler()
		{
			return _pCBViewHandler;
		}

		private PCBViewHandler _pCBViewHandler { get; set; }

		public ICommand MouseMoveCommand { get; set; }
		public ICommand MouseEnterCommand { get; set; }
		public ICommand MouseDownCommand { get; set; }
		public ICommand MouseUpCommand { get; set; }
		public ICommand MouseDragDropCommand { get; set; }
		public ICommand KeyDownCommand { get; set; }
		public ICommand KeyUpCommand { get; set; }

		private bool _isKeyDownHandled = false;
		private OpenGLHost _openGLControl; public OpenGLHost OpenGLControl { get => _openGLControl; set => SetProperty(ref _openGLControl, value); }
		private string _name; public string Name { get => _name; set => SetProperty(ref _name, value); }
		private Visibility _titleVisibility; public Visibility TitleVisibility { get => _titleVisibility; set => SetProperty(ref _titleVisibility, value); }
		private double _width; public double Width { get => _width; set => SetProperty(ref _width, value); }
		private double _height; public double Height { get => _height; set => SetProperty(ref _height, value); }
	}
}
