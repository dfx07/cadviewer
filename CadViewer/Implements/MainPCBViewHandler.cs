using CadViewer.API;
using CadViewer.Common;
using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

using _INT = System.Int32;
using _BOOL = System.Int32;
using _DOUBLE = System.Double;
using _FLOAT =System.Single;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace CadViewer.Implements
{
	public class MainPCBViewHandler : PCBViewHandler
	{
		public MainPCBViewHandler()
		{

		}

		public override void OnViewCreated(XHandleCreatedArgs createdArgs)
		{
			if(OnCreatePCBLogic(createdArgs.Handle, createdArgs.Size))
			{
				ContextConfig ctxConfig = new ContextConfig
				{
					m_bUseContextExt = BaseAPI.TRUE,
					m_nAntialiasingLevel = 8,
				};

				if (PCBViewerAPI.CreateContext(m_pHandler, ctxConfig) != BaseAPI.TRUE)
				{
					MessageBox.Show("Create OpenGL context FAIL");
				}
			}
			else
			{
				Logger.LogError("Create PCBViewer logic fail.");
			}
		}

		public override void OnMouseMove(XMouseEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_MOVE))
				return;

			string msg = "Mouse Move :" + e.Pt.ToString();

			PCBViewUIHandler.SendUIMessage(EnumPCBViewMsg.SET_TITLE_MSG, 0, 0, msg, null);

			PCBViewerAPI.OnMouseMove(m_pHandler, (_INT)e.Pt.X, (_INT)e.Pt.Y);
		}

		public override void OnMouseEnter(XMouseEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_ENTER))
				return;

			string msg = "Mouse Enter";
			PCBViewUIHandler.SendUIMessage(EnumPCBViewMsg.SET_TITLE_MSG, 0, 0, msg, null);

			PCBViewerAPI.OnMouseEnter(m_pHandler);
		}

		public override void OnMouseDragDrop(XMouseDragDropEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_DRAG))
				return;

			string msg = "Mouse Drag + Drop : " + e.Pt.ToString();
			PCBViewUIHandler.SendUIMessage(EnumPCBViewMsg.SET_TITLE_MSG, 0, 0, msg, null);

			PCBViewerAPI.OnMouseDragDrop(m_pHandler, (_INT)e.Pt.X, (_INT)e.Pt.Y, (_INT)e.Button, (_INT)e.State);
		}

		public override void OnMouseDown(XMouseButtonEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_DOWN))
				return;

			string msg = "Mouse Down : " + e.Pt.ToString();
			PCBViewUIHandler.SendUIMessage(EnumPCBViewMsg.SET_TITLE_MSG, 0, 0, msg, null);

			if(e.Button == MouseButton.Right)
			{
				var menuItems = CreatetMenuContext();

				PCBViewUIHandler.SendUIMessage(EnumPCBViewMsg.SHOW_MENU_CONTENT, 0, 0, msg, menuItems);
			}

			PCBViewerAPI.OnMouseDown(m_pHandler, (_INT)e.Pt.X, (_INT)e.Pt.Y, (_INT)e.Button);
		}

		public override void OnMouseUp(XMouseButtonEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_UP))
				return;

			string msg = "Mouse Up : " + e.Pt.ToString();
			PCBViewUIHandler.SendUIMessage(EnumPCBViewMsg.SET_TITLE_MSG, 0, 0, msg, null);

			PCBViewerAPI.OnMouseUp(m_pHandler, (_INT)e.Pt.X, (_INT)e.Pt.Y, (_INT)e.Button);
		}

		public override void OnMouseWheel(XMouseWheelEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_WHEEL))
				return;

			string msg = "Mouse wheel : " + e.Pt.ToString();
			PCBViewUIHandler.SendUIMessage(EnumPCBViewMsg.SET_TITLE_MSG, 0, 0, msg, null);

			PCBViewerAPI.OnMouseWheel(m_pHandler, (_INT)e.Pt.X, (_INT)e.Pt.Y, (_FLOAT)(e.Delta));
		}

		/*Keyboard events*/
		public override void OnKeyDown(XKeyEventArgs k)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.KEY_DOWN))
				return;

			Logger.LogInfo("Keydown : " + k.Key);

			PCBViewerAPI.OnKeyDown(m_pHandler, (_INT)k.Key, 0); // nFlags is set to 0 for simplicity
		}

		public override void OnKeyUp(XKeyEventArgs k)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.KEY_UP))
				return;

			Logger.LogInfo("Keyup : " + k.Key);

			PCBViewerAPI.OnKeyUp(m_pHandler, (_INT)k.Key, 0); // nFlags is set to 0 for simplicity
		}

		public override void OnViewSizeChanged(Size newSize)
		{
			PCBViewerAPI.OnSizeChanged(m_pHandler, (_INT)newSize.Width, (_INT)newSize.Height);
		}

		public override void OnViewUpdate()
		{
			PCBViewerAPI.OnPaint(m_pHandler);
		}

		public override void OnHandleNotify(string message, int nParam, int nWaram)
		{
			if (message == "SHOW_MENU_CONTENT")
			{
				PCBViewUIHandler.SendUIMessage(EnumPCBViewMsg.SHOW_MENU_CONTENT, 0, 0, "string", null);
			}
		}

		/*-----------------------------------------------------------------------------------*/
		ObservableCollection<MenuItemData> CreatetMenuContext()
		{
			ObservableCollection<MenuItemData> MenuItems = new ObservableCollection<MenuItemData>()
				{
					new MenuItemData
					{
						Header = "File",
						Children = new ObservableCollection<MenuItemData>
						{
							new MenuItemData
							{
								Header = "New",
								Command = new RelayCommand(OpenFile),
								Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/search26.png"))
							},
							new MenuItemData
							{
								IsSeparator = true
							},
							new MenuItemData
							{
								Header = "Open",
								Command = new RelayCommand(OpenFile),
								IsCheckable = true,
								IsChecked = true,
								IsEnabled = false,
							},
							new MenuItemData
							{
								Header = "Open 2",
								Command = new RelayCommand(OpenFile),
								IsEnabled = false
							}
						}
					},
					new MenuItemData
					{
						Header = "Edit",
					},
				};

			return MenuItems;
		}

		private void OpenFile()
		{
			MessageBox.Show("OpenFile được gọi!");
		}
	}
}
