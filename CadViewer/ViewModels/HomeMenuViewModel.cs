using CadViewer.Common;
using CadViewer.Interfaces;
using CadViewer.Services;
using CadViewer.UIControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CadViewer.ViewModels
{
	public class HomeMenuItem : NotifyPropertyChanged
	{
		public string Header { get; set; }

		private bool _isSelected;
		public bool IsSelected
		{
			get => _isSelected;
			set => SetProperty(ref _isSelected, value);
		}

		// parameter to create the ViewModel.
		public Type ViewModelType { get; set; }
	}

	public class HomeMenuViewModel : ViewModelBase
	{
		private IOverlayService m_overlayService = null;

		/* Command to handle the events. */
		public ICommand ClickedBackButtonCommand { get; set; } = null;
		public ICommand SelectMenuItemCommand { get; set; } = null;

		private TypeObjectFactoryCache m_ViewModelFactory;


		/* Data to binding. */
		public ObservableCollection<HomeMenuItem> MenuItems { get; }

		private HomeMenuItem _selectedMenuItem;
		public HomeMenuItem SelectedMenuItem
		{
			get => _selectedMenuItem;
			set
			{
				if (_selectedMenuItem != value)
				{
					if (_selectedMenuItem != null)
						_selectedMenuItem.IsSelected = false;

					_selectedMenuItem = value;

					if (_selectedMenuItem != null)
						_selectedMenuItem.IsSelected = true;
				}
			}
		}

		private object _currentMenuItemViewModel;
		public object CurrentMenuItemViewModel
		{
			get => _currentMenuItemViewModel;
			set => SetProperty(ref _currentMenuItemViewModel, value);
		}

		public HomeMenuViewModel(IOverlayService _overlayService)
		{
			m_overlayService = _overlayService;

			m_ViewModelFactory = new TypeObjectFactoryCache();

			Messenger.Register<HomeMenuActionMessageArgs>(this, OnReceivedMessage);
			ClickedBackButtonCommand = new RelayCommand(OnClickedBackButton);

			MenuItems = new ObservableCollection<HomeMenuItem>
			{
				new HomeMenuItem { Header = "Open", ViewModelType = typeof(BackstageOpenViewModel) },
				new HomeMenuItem { Header = "Save", ViewModelType = typeof(BackstageSaveViewModel) },
				new HomeMenuItem { Header = "Option", ViewModelType = typeof(BackstageOptionViewModel) },
			};

			SelectMenuItemCommand = new RelayCommand<HomeMenuItem>(OnSelectedMenuItemChanged);
		}

		private void OnSelectedMenuItemChanged(HomeMenuItem item)
		{
			SelectedMenuItem = item;

			var backstageViewModel = m_ViewModelFactory.GetOrCreate(item.ViewModelType);

			if (backstageViewModel is null)
				return;

			CurrentMenuItemViewModel = backstageViewModel;
		}

		private void OnClickedBackButton()
		{
			Hide();
		}

		private void OnReceivedMessage(HomeMenuActionMessageArgs args)
		{
			if (args is null || string.IsNullOrEmpty(args.MessageID))
				return;

			if (args.Action == "Show")
			{
				Show();
			}
		}

		void Show()
		{
			if(IsVisible)
				return;

			if (m_overlayService == null)
				return;

			IsVisible = true;

			m_overlayService.ShowOverlay(this, OverlayWindowType.Backdrop);

			OnSelectedMenuItemChanged(MenuItems.First());
		}

		void Hide()
		{
			IsVisible = false;

			m_overlayService.UpdateOverlay();
		}

		private bool _isVisible = false;
		public bool IsVisible
		{
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		private bool _IsViewClosed = false;
		public bool IsViewClosed
		{
			get => _IsViewClosed;
			set => SetProperty(ref _IsViewClosed, value, IsViewClosedChanged);
		}

		private void IsViewClosedChanged()
		{
			if(!IsViewClosed)
			{
				m_overlayService.HideOverlay();
			}
		}
	}
}
