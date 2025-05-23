﻿using CadViewer.API;
using CadViewer.Common;
using CadViewer.Dialogs;
using CadViewer.Interfaces;
using CadViewer.UIControls;
using CadViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace CadViewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IDialogService, IToastService
	{
		DlgProgress dlgProgress = null;

		ToastManager toastManager = null;

		public MainWindow()
		{
			InitializeComponent();

			_MainViewModel = new MainViewModel();
			toastManager = new ToastManager();

			this.DataContext = _MainViewModel;
		}

		public MainViewModel _MainViewModel;

		private void ShowProgress_Click(object sender, RoutedEventArgs e)
		{
			//DlgSetting setting = new DlgSetting(this);
			//ShowModal(setting);

			ShowProgress("Loading...");
		}

		private void ShowWindow_Click(object sender, RoutedEventArgs e)
		{
			CWindow win = new CWindow();

			win.Width = 800;
			win.Width = 600;
			win.Owner = this;
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
				ShowToast(new ToastData
				{
					Title = "Warn",
					Message = "This is toast message",
					ToastType = EToastMessageType.Warn,
					Duration = TimeSpan.FromSeconds(10)
				});
			}

			else if (a % 3 == 0)
			{
				ShowToast(new ToastData
				{
					Title = "Success",
					Message = "This is toast message",
					ToastType = EToastMessageType.Success,
					Duration = TimeSpan.FromSeconds(10)
				});
			}
			else if (a % 2 == 0)
			{
				ShowToast(new ToastData
				{
					Title = "Hehe",
					Message = "This is toast message",
					ToastType = EToastMessageType.Error,
					Duration = TimeSpan.FromSeconds(10)
				});
			}
			else
			{
				ShowToast(new ToastData
				{
					Title = "Info",
					Message = "This is toast message",
					ToastType = EToastMessageType.Info,
					Duration = TimeSpan.FromSeconds(10)
				});
			}
		}

		private void OnToastClicked(IToast toast)
		{
			ToastMessage toastMessage = toast as ToastMessage;

			if (toastMessage == null)
				return;

			MessageBox.Show($"You clicked to toast: {toastMessage.Title}");
		}
		public void ShowToast(ToastData tData)
		{
			toastManager.Push(tData, (data) =>
			{
				if (data == null)
					return;

				PART_RightBottomToastStack.Visibility = Visibility.Visible;

				ToastMessage toastMessage = new ToastMessage(this);
				toastMessage.ToastClicked += OnToastClicked;

				toastMessage.OnCreateToast(data);

				PART_RightBottomToastStack.Children.Add(toastMessage);
			});
		}

		public void CloseToast(IToast toast)
		{
			ToastMessage toastMessage = toast as ToastMessage;

			if (toastMessage == null)
				return;

			PART_RightBottomToastStack.Children.Remove(toastMessage);

			if (PART_RightBottomToastStack.Children.Count == 0)
			{
				PART_RightBottomToastStack.Visibility = Visibility.Collapsed;
			}
		}

		public void ShowProgress(string progressMsg)
		{
			PART_ModalOverlay.Visibility = Visibility.Visible;

			if(dlgProgress == null)
			{
				dlgProgress = new DlgProgress(this);
			}

			dlgProgress.HorizontalAlignment = HorizontalAlignment.Center;
			dlgProgress.VerticalAlignment = VerticalAlignment.Center;

			dlgProgress.ProcessMessage = progressMsg;

			if (!PART_ModalOverlay.Children.Contains(dlgProgress))
			{
				PART_ModalOverlay.Children.Add(dlgProgress);
			}
		}

		public int ShowModal(IModalDialog dialog)
		{
			PART_ModalOverlay.Visibility = Visibility.Visible;

			UserControl dlg = dialog as UserControl;

			if (dlg is null)
				return -1;

			dlg.HorizontalAlignment = HorizontalAlignment.Center;
			dlg.VerticalAlignment = VerticalAlignment.Center;

			PART_ModalOverlay.Children.Add(dlg);

			return 1;
		}

		public void CloseModal(IModalDialog dialog)
		{
			PART_ModalOverlay.Visibility = Visibility.Collapsed;

			UserControl dlg = dialog as UserControl;

			if (dlg is null)
				return;

			PART_ModalOverlay.Children.Remove(dlg);

			if(dlgProgress == dlg)
			{
				dlgProgress = null;
			}
		}

		private void OpenFile()
		{
			MessageBox.Show("OpenFile được gọi!");
		}

		private void ShowMenu_Click(object sender, RoutedEventArgs e)
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

			var menu = new CContextMenu();

			foreach (var itemData in MenuItems)
			{
				CMenuItem items = CMenuItem.CreateMenuItem(itemData) as CMenuItem;

				menu.Items.Add(items);
			}

			menu.PlacementTarget = MenuButton;
			menu.Placement = PlacementMode.Bottom;
			menu.IsOpen = true;
		}
	}
}
