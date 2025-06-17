using CadViewer.Common;
using CadViewer.Interfaces;
using CadViewer.Services;
using CadViewer.UIControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CadViewer.Views
{
	/// <summary>
	/// Interaction logic for UIControl.xaml
	/// </summary>
	public partial class UIControlViewer : UserControl
	{
		private IToastService toastService;
		private IDialogService dialogService;

		public UIControlViewer()
		{
			InitializeComponent();
		}

		private void ShowProgress_Click(object sender, RoutedEventArgs e)
		{
			//DlgSetting setting = new DlgSetting(this);
			//ShowModal(setting);

			dialogService.ShowProgress("Loading...");
		}

		private void ShowWindow_Click(object sender, RoutedEventArgs e)
		{
			CWindow win = new CWindow();

			win.Width = 800;
			win.Width = 600;
			win.Owner = Application.Current.MainWindow;
			win.Title = "Window";
			win.WindowStartupLocation = WindowStartupLocation.CenterOwner;

			win.ShowDialog();
		}

		int a = 0;
		private void ShowToast_Click(object sender, RoutedEventArgs e)
		{
			a++;
			if (a % 4 == 0)
			{
				toastService.ShowToast(new ToastData
				{
					Title = "Warn",
					Message = "This is toast message",
					ToastType = EToastMessageType.Warn,
					Duration = TimeSpan.FromSeconds(10)
				});
			}

			else if (a % 3 == 0)
			{
				toastService.ShowToast(new ToastData
				{
					Title = "Success",
					Message = "This is toast message",
					ToastType = EToastMessageType.Success,
					Duration = TimeSpan.FromSeconds(10)
				});
			}
			else if (a % 2 == 0)
			{
				toastService.ShowToast(new ToastData
				{
					Title = "Hehe",
					Message = "This is toast message",
					ToastType = EToastMessageType.Error,
					Duration = TimeSpan.FromSeconds(10)
				});
			}
			else
			{
				toastService.ShowToast(new ToastData
				{
					Title = "Info",
					Message = "This is toast message",
					ToastType = EToastMessageType.Info,
					Duration = TimeSpan.FromSeconds(10)
				});
			}
		}

		private void OpenFile()
		{
			MessageBox.Show("OpenFile được gọi!");
		}

		private void ShowMenu_Click(object sender, RoutedEventArgs e)
		{
			//ObservableCollection<MenuItemData> MenuItems = new ObservableCollection<MenuItemData>()
			//{
			//	new MenuItemData
			//	{
			//		Header = "File",
			//		IsChecked= true,
			//		IsCheckable = true,
			//		Children = new ObservableCollection<MenuItemData>
			//		{
			//			new MenuItemData
			//			{
			//				Header = "New",
			//				Command = new RelayCommand(OpenFile),
			//				Icon = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/search26.png"))
			//			},
			//			new MenuItemData
			//			{
			//				IsSeparator = true
			//			},
			//			new MenuItemData
			//			{
			//				Header = "Open",
			//				Command = new RelayCommand(OpenFile),
			//				IsCheckable = true,
			//				IsChecked = true,
			//				IsEnabled = false,
			//			},
			//			new MenuItemData
			//			{
			//				Header = "Open 2",
			//				Command = new RelayCommand(OpenFile),
			//				IsEnabled = false
			//			}
			//		}
			//	},
			//	new MenuItemData
			//	{
			//		Header = "Edit",
			//	},
			//};

			//var menu = new CContextMenu();

			//foreach (var itemData in MenuItems)
			//{
			//	//CMenuItem items = CMenuItem.CreateMenuItem(itemData) as CMenuItem;

			//	//menu.Items.Add(items);
			//}

			//menu.PlacementTarget = MenuButton;
			//menu.Placement = PlacementMode.Bottom;
			//menu.IsOpen = true;
		}
	}
}
