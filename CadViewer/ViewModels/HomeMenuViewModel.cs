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
using System.Windows.Input;
using System.Windows.Media;

namespace CadViewer.ViewModels
{
	public class HomeMenuViewModel : ViewModelBase
	{
		private IOverlayService m_overlayService = null;

		public HomeMenuViewModel(IOverlayService _overlayService)
		{
			m_overlayService = _overlayService;

			Messenger.Register<HomeMenuActionMessageArgs>(this, OnReceivedMessage);
			ClickedBackButtonCommand = new RelayCommand(OnClickedBackButton);
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

			m_overlayService.ShowOverlay(this);
		}

		void Hide()
		{
			IsVisible = false;
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
		public ICommand ClickedBackButtonCommand { get; set; }
	}
}
