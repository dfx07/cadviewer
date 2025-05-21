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
			Loaded += (s, e) =>
			{
			};
		}
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

			Loaded += (s, e) =>
			{

			};
		}

		public static object CreateMenuItem(MenuItemData data, bool bCreateChild = true)
		{
			if (data.IsSeparator)
			{
				var separator = new CSeparator
				{
					Style = (Style)Application.Current.FindResource("CSeparatorStyle"),
				};

				return separator;
			}

			var menuItem = new CMenuItem
			{
				DataContext = data,
			};

			menuItem.SetBinding(CMenuItem.HeaderProperty, new Binding(nameof(MenuItemData.Header)));
			menuItem.SetBinding(CMenuItem.IconProperty, new Binding(nameof(MenuItemData.Icon)));
			menuItem.SetBinding(CMenuItem.IsEnabledProperty, new Binding(nameof(MenuItemData.IsEnabled)));
			menuItem.SetBinding(CMenuItem.IsCheckedProperty, new Binding(nameof(MenuItemData.IsChecked)));
			menuItem.SetBinding(CMenuItem.IsCheckableProperty, new Binding(nameof(MenuItemData.IsCheckable)));
			menuItem.SetBinding(CMenuItem.CommandProperty, new Binding(nameof(MenuItemData.Command)));
			menuItem.SetBinding(CMenuItem.CommandParameterProperty, new Binding(nameof(MenuItemData.CommandParameter)));
			menuItem.SetBinding(CMenuItem.VisibilityProperty, new Binding(nameof(MenuItemData.IsVisible))
			{
				Converter = new BooleanToVisibilityConverter()
			});

			if(bCreateChild)
			{
				if (data.Children != null && data.Children.Any())
				{
					foreach (var child in data.Children)
					{
						menuItem.Items.Add(CreateMenuItem(child));
					}
				}
			}

			return menuItem;
		}
	}
}
