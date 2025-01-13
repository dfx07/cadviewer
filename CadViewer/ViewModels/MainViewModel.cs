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
	public class MainViewModel : ViewModelBase
	{
		public PCBViewModel MainPCBViewModel { get; set; }
		public MainPCBViewHandler mainPCBViewHandler { get; set; }

		public ICommand IBtnRegisterClickComand { get; }

		public MainViewModel()
		{
			mainPCBViewHandler = new MainPCBViewHandler();

			MainPCBViewModel = new PCBViewModel();
			MainPCBViewModel.Name = "Start";
			MainPCBViewModel.SetHandler(mainPCBViewHandler);

			IBtnRegisterClickComand = new RelayCommand(OnButtonRegisterClick);
		}
		public override void OnNotifyUI(string message, int nParam, int nWaram)
		{
			return;
		}

		private void OnButtonRegisterClick()
		{
			MainPCBViewModel.DrawLine();
		}
	}
}
