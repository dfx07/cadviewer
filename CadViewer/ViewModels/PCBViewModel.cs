using CadViewer.API;
using CadViewer.Common;
using CadViewer.Components;
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
	public class PCBViewModel : ViewModelBase
	{
		public PCBViewModel()
		{
			OpenGLControl = new OpenGLHost();
			MouseMoveCommand = new RelayCommand<MouseEventArgs>(OnMouseMove);
			MouseEnterCommand = new RelayCommand<MouseEventArgs>(OnMouseEnter);
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
		private void OnMouseMove(object parameter)
		{

		}

		private void OnMouseEnter(object parameter)
		{

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
		public ICommand MouseMoveCommand { get; }
		public ICommand MouseEnterCommand { get; }
		private OpenGLHost _openGLControl; public OpenGLHost OpenGLControl { get => _openGLControl; set => SetProperty(ref _openGLControl, value); }
		private string _name; public string Name { get => _name; set => SetProperty(ref _name, value); }
		private double _width; public double Width { get => _width; set => SetProperty(ref _width, value); }
		private double _height; public double Height { get => _height; set => SetProperty(ref _height, value); }
	}
}
