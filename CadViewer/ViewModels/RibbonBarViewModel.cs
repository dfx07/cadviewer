using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CadViewer.Services;

namespace CadViewer.ViewModels
{
	public class RibbonBarViewModel : ViewModelBase
	{
		private readonly Dictionary<string, object> _ribbonContentTabs = new Dictionary<string, object>();

		private ObservableCollection<TabBarItemInfo> _tabBarInfos;
		public ObservableCollection<TabBarItemInfo> TabBarInfos
		{
			get => _tabBarInfos;
			set => SetProperty(ref _tabBarInfos, value);
		}

		private TabBarItemInfo _selectedTab;
		public TabBarItemInfo SelectedTab
		{
			get => _selectedTab;
			set => SetProperty(ref _selectedTab, value, () => OnTabChanged(_selectedTab));
		}

		private object _currentRibbonPanelViewModel;
		public object CurrentRibbonPanelViewModel
		{
			get => _currentRibbonPanelViewModel;
			set => SetProperty(ref _currentRibbonPanelViewModel, value);
		}

		private bool _isCheckedHomeBtn = false;
		public bool IsCheckedHomeBtn
		{
			get => _isCheckedHomeBtn;
			set => SetProperty(ref _isCheckedHomeBtn, value, () => OnCheckedHome(_isCheckedHomeBtn));
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
		private void LoadRibbonPanelForTab(TabBarItemInfo tab)
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
						NewViewModel = new HomeRibbonPanelViewModel();
						break;
					case "EDIT":
						NewViewModel = new EditRibbonPanelViewModel();
						break;
					default:
						break;
				}

				if (NewViewModel != null)
					_ribbonContentTabs[tab.strTabName] = NewViewModel;
			}

			CurrentRibbonPanelViewModel = NewViewModel;
		}

		private void OnTabChanged(TabBarItemInfo tabinfo)
		{
			LoadRibbonPanelForTab(tabinfo);
		}

		public void OnCheckedHome(bool bChecked)
		{
			Messenger.Send(new HomeMenuActionMessageArgs
			{
				MessageID = "RibbonAction",
				Sender = this,
				Action= bChecked ? "Show" : "Hide"
			});
		}
	}
}
