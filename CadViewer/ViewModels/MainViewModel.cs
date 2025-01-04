using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CadViewer.API;
using CadViewer.Common;

namespace CadViewer.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public PCBViewModel MainPCBViewModel { get; set; }

		public ICommand IBtnRegisterClickComand { get; }

		public MainViewModel()
		{
			MainPCBViewModel = new PCBViewModel();
			MainPCBViewModel.Name = "Main PCB";

			//IBtnRegisterClickComand = new RelayCommand(OnButtonRegisterClick);
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
