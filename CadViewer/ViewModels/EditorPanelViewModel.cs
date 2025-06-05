using CadViewer.Interfaces;
using CadViewer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.ViewModels
{
	public class EditorPanelViewModel : ViewModelBase
	{
		public EditorPanelViewModel()
		{
			Messenger.Register<MessageArgs>(this, OnReceivedMessage);
		}

		private void OnReceivedMessage(MessageArgs args)
		{
			if(args is null || string.IsNullOrEmpty(args.MessageID))
				return;

			if(args.MessageID == "Show")
			{
				CurrentPanelViewModel = new UIControlViewerViewModel();
			}
			else
			{
				CurrentPanelViewModel = null;
			}
		}

		private ViewModelBase _currentPanelViewModel;
		public ViewModelBase CurrentPanelViewModel
		{
			get => _currentPanelViewModel;
			set => SetProperty(ref _currentPanelViewModel, value);
		}
	}
}