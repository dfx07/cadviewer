using CadViewer.Implements;
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
		public MainPCBViewHandler mainPCBViewHandler { get; set; }

		public EditorPanelViewModel()
		{
			Messenger.Register<MessageArgs>(this, OnReceivedMessage);
		}

		private void OnReceivedMessage(MessageArgs args)
		{
			if(args is null || string.IsNullOrEmpty(args.MessageID))
				return;

			if(args.MessageID == "UIShow")
			{
				CurrentPanelViewModel = new UIControlViewerViewModel();
			}
			else if(args.MessageID == "ViewShow")
			{
				mainPCBViewHandler = new MainPCBViewHandler();
				var pPCBViewModel = new PCBViewModel(mainPCBViewHandler);

				CurrentPanelViewModel = pPCBViewModel;
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

		private void OnButtonRegisterClick()
		{
			mainPCBViewHandler.DrawLine();
		}
	}
}