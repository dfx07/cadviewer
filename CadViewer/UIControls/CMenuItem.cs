using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using CadViewer.Animations;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Threading;
using CadViewer.Interfaces;
using System.Windows.Data;

namespace CadViewer.UIControls
{
	public class CSeparator : Separator
	{
		static CSeparator()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CSeparator),
				new FrameworkPropertyMetadata(typeof(CSeparator)));
		}

		public CSeparator()
		{
			this.Style = (Style)Application.Current.FindResource("CSeparatorStyle");
		}
	}

	public class CMenu : Menu
	{
		static CMenu()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CMenu),
				new FrameworkPropertyMetadata(typeof(CMenu)));
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (ItemsSource == null && DataContext is MenuItemData data)
			{
				ItemsSource = data.Children;
			}
		}

		protected override DependencyObject GetContainerForItemOverride() => new CMenuItem();
		protected override bool IsItemItsOwnContainerOverride(object item) => item is CMenuItem;
	}

	public class CMenuItem : MenuItem
	{
		static CMenuItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CMenuItem),
				new FrameworkPropertyMetadata(typeof(CMenuItem)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (ItemsSource == null && DataContext is MenuItemData data)
			{
				ItemsSource = data.Children;
			}
		}

		protected override DependencyObject GetContainerForItemOverride() => new CMenuItem();

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is CMenuItem || item is CSeparator;
		}
	}
}
