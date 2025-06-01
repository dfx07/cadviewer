using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CadViewer.API;
using CadViewer.Common;
using CadViewer.Implements;
using CadViewer.Interfaces;
using CadViewer.UIControls;

namespace CadViewer.ViewModels
{
	public class MainViewModel : ViewModelCallback
	{
		public ObservableCollection<PropertyItemData> PropertyDataItems { get; set; }

		public PCBViewModel MainPCBViewVM { get; set; }
		public RibbonBarViewModel RibbonBarVM { get; set; }
		public MainPCBViewHandler mainPCBViewHandler { get; set; }

		public ICommand IBtnRegisterClickComand { get; }

		public MainViewModel()
		{
			MainPCBViewVM = new PCBViewModel();

			RibbonBarVM = new RibbonBarViewModel();

			mainPCBViewHandler = new MainPCBViewHandler();

			MainPCBViewVM.SetHandler(mainPCBViewHandler);

			IBtnRegisterClickComand = new RelayCommand(OnButtonRegisterClick);

			PropertyDataItems = new ObservableCollection<PropertyItemData>()
			{
				new PropertyItemIntegerData
				{
					Name = "Color",
					Value = 111
				},
				new PropertyItemStringData
				{
					Name = "Layer",
					Value = "Thi"
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222
				}
			};
		}

		private void OnButtonRegisterClick()
		{
			mainPCBViewHandler.DrawLine();
		}
	}
}
