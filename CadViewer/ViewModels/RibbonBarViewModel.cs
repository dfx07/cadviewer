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
		private readonly Dictionary<string, object> _ribbonContentTabs = new Dictionary<string, object>();

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
				OnTabChanged(_selectedTab);
			}
		}

		private object _ribbonContent;
		public object RibbonContent
		{
			get => _ribbonContent;
			set
			{
				_ribbonContent = value;
				OnPropertyChanged(nameof(RibbonContent));
			}
		}

		public RibbonBarViewModel()
		{
			TabBarInfos = new ObservableCollection<TabBarItemInfo>()
			{
				new TabBarItemInfo { strTabName = "HOME", nTabIdx = 0 },
				new TabBarItemInfo { strTabName = "EDIT", nTabIdx = 1 },
				new TabBarItemInfo { strTabName = "VIEW", nTabIdx = 2 },
				new TabBarItemInfo { strTabName = "PROJ", nTabIdx = 3 }
			};

			SelectedTab = TabBarInfos.FirstOrDefault();
		}
		private void LoadRibbonContentForTab(TabBarItemInfo tab)
		{
			if (tab == null)
				return;

			object NewViewModel = null;

			if (_ribbonContentTabs.ContainsKey(tab.strTabName))
			{
				if (_ribbonContentTabs.TryGetValue(tab.strTabName, out var curTab))
				{
					NewViewModel = curTab;
				}
			}
			else
			{
				switch (tab.strTabName.ToUpper())
				{
					case "HOME":
						NewViewModel = new HomeBarViewModel();
						break;
					case "EDIT":
						NewViewModel = new EditBarViewModel();
						break;
					default:
						break;
				}

				if (NewViewModel != null)
					_ribbonContentTabs[tab.strTabName] = NewViewModel;
			}

			RibbonContent = NewViewModel;
		}

		private void OnTabChanged(TabBarItemInfo tabinfo)
		{
			LoadRibbonContentForTab(tabinfo);
		}
	}
}
