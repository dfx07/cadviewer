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

		private ObservableCollection<TabBarItemInfo> _refTabBarInfos;
		public ObservableCollection<TabBarItemInfo> RefTabBarInfos
		{
			get { return _refTabBarInfos; }
			set
			{
				_refTabBarInfos = value;
				base.OnPropertyChanged(nameof(RefTabBarInfos));
			}
		}

		public void SetListener(TabBarEventListener listener)
		{
			Listener = listener;
		}

		public void SetTabInfos(ObservableCollection<TabBarItemInfo> tabInfos)
		{
			RefTabBarInfos = tabInfos;
		}

		public TabBar()
		{
			InitializeComponent();

			base.DataContext = this;
		}

		private T FindChild<T>(DependencyObject parent) where T : DependencyObject
		{
			// Check if the parent is of the requested type
			if (parent is T)
				return (T)parent;

			// Otherwise, recursively search through the visual tree
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				T result = FindChild<T>(child);
				if (result != null)
					return result;
			}
			return null;
		}

		private Point GetElementLocation(Visual element)
		{
			// Transform the element into the coordinate system of the ListBox
			GeneralTransform transform = element.TransformToAncestor(xTabItems);
			// Get the top-left corner position of the element
			return transform.Transform(new Point(0, 0));
		}
		private void TabItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				TabBarItemInfo tabBarItemInfo = e.AddedItems[0] as TabBarItemInfo;
				if(tabBarItemInfo == null)
				{
					return;
				}

				ListBoxItem tabItem = xTabItems.ItemContainerGenerator.ContainerFromItem(tabBarItemInfo) as ListBoxItem;
				if (tabItem == null)
				{
					return;
				}

				Border border = FindChild<Border>(tabItem);
				Line line = FindChild<Line>(xTabItems);

				// Animate the selection line
				if (border != null)
				{
					Point borderLocation = GetElementLocation(border);

					// Calculate the position of the line
					double lineStartX = borderLocation.X;
					double lineStartY = borderLocation.Y + border.RenderSize.Height;
					double lineEndX = borderLocation.X + border.RenderSize.Width;
					double lineEndY = lineStartY;

					line.X1 = lineStartX + 1;
					line.Y1 = lineStartY;
					line.X2 = lineEndX - 1;
					line.Y2 = lineEndY;

					line.Visibility = Visibility.Visible;
				}
				// Notify the listener
				Listener?.TabBar_OnTabChanged(tabBarItemInfo);
			}
		}
	}
}
