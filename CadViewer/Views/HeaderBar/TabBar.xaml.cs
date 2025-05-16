using CadViewer.Common;
using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CadViewer.View
{
	/// <summary>
	/// Interaction logic for TabBar.xaml
	/// </summary>
	public partial class TabBar : UserControlBase
	{
		public TabBarEventListener Listener = null;


		public void SetListener(TabBarEventListener listener)
		{
			Listener = listener;
		}

		public TabBar()
		{
			InitializeComponent();
		}

		private void TabItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				TabBarItemInfo tabBarItemInfo = e.AddedItems[0] as TabBarItemInfo;
				if (tabBarItemInfo == null)
				{
					return;
				}

				// Notify the listener
				Listener?.TabBar_OnTabChanged(tabBarItemInfo);
			}
		}

		public static readonly DependencyProperty RefTabBarInfosProperty = 
			DependencyProperty.Register(nameof(RefTabBarInfos), typeof(ObservableCollection<TabBarItemInfo>), typeof(TabBar), new PropertyMetadata(null));

		public ObservableCollection<TabBarItemInfo> RefTabBarInfos
		{
			get => (ObservableCollection<TabBarItemInfo>)GetValue(RefTabBarInfosProperty);
			set => SetValue(RefTabBarInfosProperty, value);
		}

		public static readonly DependencyProperty SelectedTabBarInfoProperty =
		DependencyProperty.Register(nameof(SelectedTabBarInfo), typeof(TabBarItemInfo), typeof(TabBar), new PropertyMetadata(null));

		public TabBarItemInfo SelectedTabBarInfo
		{
			get => (TabBarItemInfo)GetValue(SelectedTabBarInfoProperty);
			set => SetValue(SelectedTabBarInfoProperty, value);
		}
	}
}
