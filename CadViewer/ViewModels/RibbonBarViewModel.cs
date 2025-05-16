using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.ViewModels
{
	public class RibbonBarViewModel : ViewModelBase
	{
		private ObservableCollection<TabBarItemInfo> _tabBarInfos;
		public ObservableCollection<TabBarItemInfo> TabBarInfos
		{
			get => _tabBarInfos;
			set
			{
				_tabBarInfos = value;
				OnPropertyChanged(nameof(TabBarInfos));
			}
		}

		private TabBarItemInfo _selectedTab;
		public TabBarItemInfo SelectedTab
		{
			get => _selectedTab;
			set
			{
				_selectedTab = value;
				OnPropertyChanged(nameof(SelectedTab));
			}
		}

		public RibbonBarViewModel()
		{
			TabBarInfos = new ObservableCollection<TabBarItemInfo>()
			{
				new TabBarItemInfo { strTabName = "FILE", nTabIdx = 0 },
				new TabBarItemInfo { strTabName = "EDIT", nTabIdx = 1 },
				new TabBarItemInfo { strTabName = "VIEW", nTabIdx = 2 },
				new TabBarItemInfo { strTabName = "PROJ", nTabIdx = 3 }
			};

			SelectedTab = TabBarInfos.FirstOrDefault();
		}
	}
}
