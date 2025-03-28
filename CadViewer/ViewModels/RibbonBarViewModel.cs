using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.ViewModels
{
	public class RibbonBarViewModel : TabBarEventListener
	{
		public ObservableCollection<TabBarItemInfo> tabBarInfos;

		public RibbonBarViewModel()
		{
			// Initialize the RibbonBarViewModel
			//tabBarInfos.Add(new TabBarItemInfo { strTabName = "FILE", nTabIdx = 0, bVisible = true});
			//tabBarInfos.Add(new TabBarItemInfo { strTabName = "EDIT", nTabIdx = 1 });
			//tabBarInfos.Add(new TabBarItemInfo { strTabName = "VIEW", nTabIdx = 2 });
			//tabBarInfos.Add(new TabBarItemInfo { strTabName = "VIEW", nTabIdx = 3 });
		}

		public void OnInitModel()
		{
			tabBarInfos = new ObservableCollection<TabBarItemInfo>();

			// Initialize the RibbonBarViewModel
			tabBarInfos.Add(new TabBarItemInfo { strTabName = "FILE", nTabIdx = 0 });
			tabBarInfos.Add(new TabBarItemInfo { strTabName = "EDIT", nTabIdx = 1 });
			tabBarInfos.Add(new TabBarItemInfo { strTabName = "VIEW", nTabIdx = 2 });
			tabBarInfos.Add(new TabBarItemInfo { strTabName = "VIEW", nTabIdx = 3 });
		}

		public void TabBar_OnTabChanged(TabBarItemInfo _tabinfo)
		{
			//TODO : implement
			tabBarInfos.First(x => x.strTabName == _tabinfo.strTabName).bVisible = false;
		}
	}
}
