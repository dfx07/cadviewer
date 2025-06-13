using CadViewer.Common;
using CadViewer.Interfaces;
using CadViewer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CadViewer.ViewModels
{
	public class HomeMenuViewModel : ViewModelBase
	{
		public HomeMenuViewModel()
		{
			Messenger.Register<HomeMenuActionMessageArgs>(this, OnReceivedMessage);


			ClickedBackButtonCommand = new RelayCommand(OnClickedBackButton);
		}

		private void OnClickedBackButton()
		{
			IsVisible = false;
		}

		private void OnReceivedMessage(HomeMenuActionMessageArgs args)
		{
			if (args is null || string.IsNullOrEmpty(args.MessageID))
				return;

			if (args.Action == "Show")
			{
				IsVisible = true;
			}
			else if(args.Action == "Hide")
			{
				IsVisible = false;
			}
		}

		private bool _isVisible = false;
		public bool IsVisible
		{
			get => _isVisible;
			set { _isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
		}

		public ICommand ClickedBackButtonCommand { get; set; }
	}
}
