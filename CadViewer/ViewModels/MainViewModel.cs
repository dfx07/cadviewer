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
					Value = 111,
					Level = 0
				},
				new PropertyItemGroupData
				{
					Name = "Layer",
					Children = new ObservableCollection<PropertyItemData>
					{
						new PropertyItemStringData
						{
							Name = "Thi1.1",
							Value = "Thi",
							Level = 1
						},
						new PropertyItemIntegerData
						{
							Name = "Thi1.2",
							Value = 1234,
							Level = 1
						}
					},
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemIntegerData
				{
					Name = "LineType",
					Value = 222,
					Level = 0
				},
				new PropertyItemGroupData
				{
					Name = "Layer",
					Children = new ObservableCollection<PropertyItemData>
					{
						new PropertyItemStringData
						{
							Name = "Thi1.1",
							Value = "Thi",
							Level = 1
						},
						new PropertyItemIntegerData
						{
							Name = "Thi1.2",
							Value = 1234,
							Level = 1
						}
					},
					Level = 0
				},
				new PropertyItemGroupData
				{
					Name = "Layer",
					Children = new ObservableCollection<PropertyItemData>
					{
						new PropertyItemStringData
						{
							Name = "Thi1.1",
							Value = "Thi",
							Level = 1
						},
						new PropertyItemIntegerData
						{
							Name = "Thi1.2",
							Value = 1234,
							Level = 1
						}
					},
					Level = 0
				},
				new PropertyItemGroupData
				{
					Name = "Layer",
					Children = new ObservableCollection<PropertyItemData>
					{
						new PropertyItemStringData
						{
							Name = "Thi1.1",
							Value = "Thi",
							Level = 1
						},
						new PropertyItemIntegerData
						{
							Name = "Thi1.2",
							Value = 1234,
							Level = 1
						}
					},
					Level = 0
				},
				new PropertyItemGroupData
				{
					Name = "Layer",
					Children = new ObservableCollection<PropertyItemData>
					{
						new PropertyItemStringData
						{
							Name = "Thi1.1",
							Value = "Thi",
							Level = 1
						},
						new PropertyItemIntegerData
						{
							Name = "Thi1.2",
							Value = 1234,
							Level = 1
						}
					},
					Level = 0
				},
				new PropertyItemGroupData
				{
					Name = "Layer",
					Children = new ObservableCollection<PropertyItemData>
					{
						new PropertyItemStringData
						{
							Name = "Thi1.1",
							Value = "Thi",
							Level = 1
						},
						new PropertyItemIntegerData
						{
							Name = "Thi1.2",
							Value = 1234,
							Level = 1
						},
						new PropertyItemStringData
						{
							Name = "Layer",
							Value = "Thi",
							Children = new ObservableCollection<PropertyItemData>
							{
								new PropertyItemStringData
								{
									Name = "Thi1.1",
									Value = "Thi",
									Level = 2
								},
								new PropertyItemIntegerData
								{
									Name = "Thi1.2",
									Value = 1234,
									Level = 2
								}
							},
							Level = 1
						}
					},
					Level = 0
				},
			};
		}

		private void OnButtonRegisterClick()
		{
			mainPCBViewHandler.DrawLine();
		}
	}
}
