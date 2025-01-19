using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CadViewer.API;
using CadViewer.Common;
using CadViewer.Implements;

namespace CadViewer.ViewModels
{
	public class MainViewModel : ViewModelCallback
	{
		public PCBViewModel MainPCBViewModel { get; set; }
		public MainPCBViewHandler mainPCBViewHandler { get; set; }

		public ICommand IBtnRegisterClickComand { get; }

		public MainViewModel()
		{
			MainPCBViewModel = new PCBViewModel();
			mainPCBViewHandler = new MainPCBViewHandler();

			MainPCBViewModel.SetHandler(mainPCBViewHandler);

			IBtnRegisterClickComand = new RelayCommand(OnButtonRegisterClick);
		}

		private void OnButtonRegisterClick()
		{
			mainPCBViewHandler.DrawLine();
		}
	}
}
