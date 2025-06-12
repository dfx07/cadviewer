using CadViewer.API;
using CadViewer.Common;
using CadViewer.Components;
using CadViewer.Interfaces;
using CadViewer.UIControls;
using CadViewer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace CadViewer.ViewModels
{
	public class PCBViewModel : ViewModelBase, IPCBViewHandlerListener
	{
		public PCBViewModel(PCBViewHandler pCBViewHandler = null)
		{
			MouseMoveCommand = new RelayCommand<XMouseEventArgs>(pCBViewHandler.OnMouseMove);
			MouseEnterCommand = new RelayCommand<XMouseEventArgs>(pCBViewHandler.OnMouseEnter);
			MouseDragDropCommand = new RelayCommand<XMouseDragDropEventArgs>(pCBViewHandler.OnMouseDragDrop);
			MouseDownCommand = new RelayCommand<XMouseButtonEventArgs>(pCBViewHandler.OnMouseDown);
			MouseUpCommand = new RelayCommand<XMouseButtonEventArgs>(pCBViewHandler.OnMouseUp);
			MouseWheelCommand = new RelayCommand<XMouseWheelEventArgs>(pCBViewHandler.OnMouseWheel);

			KeyDownCommand = new RelayCommand<XKeyEventArgs>(pCBViewHandler.OnKeyDown);
			KeyUpCommand = new RelayCommand<XKeyEventArgs>(pCBViewHandler.OnKeyUp);

			ViewSizeChangedCommand = new RelayCommand<Size>(pCBViewHandler.OnViewSizeChanged);
			ViewCreatedCommand = new RelayCommand<XHandleCreatedArgs>(pCBViewHandler.OnViewCreated);
			ViewUpdateCommand = new RelayCommand(pCBViewHandler.OnViewUpdate);

			SetHandler(pCBViewHandler);
		}

		~PCBViewModel()
		{

		}

		bool GetCtrlKeyState(Key key)
		{
			if(Keyboard.IsKeyDown(key))
			{
				return true;
			}
			return false;
		}

		#region [Handle PCBViewNotifier]
		// ------------------------------------------------------------------------------//
		public void SetTitle(string strTitle)
		{
			ViewTitle = strTitle;
		}

		public void SetVisibleTitle(bool bShow)
		{
			TitleVisibility = bShow ? Visibility.Visible : Visibility.Collapsed;
		}

		public int SendUIMessage(EnumPCBViewMsg msg, int nData, float fData, string strData, object pObject)
		{
			switch (msg)
			{
				case EnumPCBViewMsg.SET_TITLE_MSG:
					{
						SetTitle(strData);
						break;
					}
				case EnumPCBViewMsg.SHOW_MENU_CONTENT:
					{
						var menuItems = pObject as ObservableCollection<MenuItemData>;
						//ShowMenuContent(menuItems);

						ContextMenuItems = menuItems;
						IsContextMenuVisible = true;

						break;
					}
				default:
					break;
			}

			return 1;
		}

		public void ShowMenuContent(ObservableCollection<MenuItemData> menuItems)
		{
			//if (menuItems is null)
			//	return;

			//var contextMenu = new CContextMenu();

			//foreach (var itemData in menuItems)
			//{
			//	CMenuItem items = CMenuItem.CreateMenuItem(itemData) as CMenuItem;

			//	contextMenu.Items.Add(items);
			//}

			//contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;

			//contextMenu.IsOpen = true;
		}

		public int GetIntUIData(EnumPCBViewMsg msg, int lParam, int wParam)
		{
			switch(msg)
			{
				case EnumPCBViewMsg.GET_CTRL_KEY_STATE:
					return GetCtrlKeyState((Key)lParam) ? 1 : 0;

				default:
					break;
			}
			return 1;
		}

		public string GetStringUIData(EnumPCBViewMsg msg, int lParam, int wParam)
		{
			return "";
		}

		#endregion


		public void SetHandler(PCBViewHandler handler)
		{
			_pCBViewHandler = handler;
			handler?.SetPCBViewHandleListener(this);
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
		public ICommand MouseWheelCommand { get; set; }
		public ICommand ViewSizeChangedCommand { get; set; }
		public ICommand ViewCreatedCommand { get; set; }
		public ICommand ViewUpdateCommand { get; set; }

		private string _title;
		public string ViewTitle { get => _title; set => SetProperty(ref _title, value); }

		private string _status;
		public string ViewStatus { get => _status; set => SetProperty(ref _status, value); }

		private Visibility _titleVisibility;
		public Visibility TitleVisibility { get => _titleVisibility; set => SetProperty(ref _titleVisibility, value); }

		private double _width;
		public double Width { get => _width; set => SetProperty(ref _width, value); }

		private double _height;
		public double Height { get => _height; set => SetProperty(ref _height, value); }

		/// <summary>
		/// ************************ Menu-Contenxt ************************
		/// </summary>
		public ObservableCollection<MenuItemData> ContextMenuItems { get; set; }

		private bool _isContextMenuVisible;
		public bool IsContextMenuVisible
		{
			get => _isContextMenuVisible;
			set => SetProperty(ref _isContextMenuVisible, value);
		}

		private Point _menuPosition;
		public Point MenuPosition
		{
			get => _menuPosition;
			set => SetProperty(ref _menuPosition, value);
		}
	}
}
