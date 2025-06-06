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
using System.ComponentModel;

namespace CadViewer.UIControls
{

	public class CPropertyTreeItem : ItemsControl
	{
		static CPropertyTreeItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CPropertyTreeItem),
				new FrameworkPropertyMetadata(typeof(CPropertyTreeItem)));
		}
		public CPropertyTreeItem()
		{
			Loaded += CPropertyTreeItem_Loaded;
		}

		private void CPropertyTreeItem_Loaded(object sender, RoutedEventArgs e)
		{
			if (DataContext is PropertyItemData itemData && itemData.Children != null)
			{
				ItemsSource = itemData.Children;
			}
		}

		static CPropertyTreeItem CreatePropertyItems(PropertyItemData itemData, bool bCreateChild = true)
		{
			var proItem = new CPropertyTreeItem
			{
				DataContext = itemData,
			};

			proItem.SetBinding(CPropertyTreeItem.NameProperty, new Binding(nameof(PropertyItemData.Name)));
			proItem.SetBinding(CPropertyTreeItem.ValueItemProperty, new Binding(nameof(PropertyItemStringData.Value)));

			if (bCreateChild && itemData.Children != null)
			{
				foreach (var child in itemData.Children)
				{
					var childItem = CreatePropertyItems(child);
					proItem.Items.Add(childItem);
				}
			}

			return proItem;
		}

		public static readonly DependencyProperty ValueItemProperty =
		DependencyProperty.Register(nameof(ValueItem), typeof(object), typeof(CPropertyTreeItem));
		public object ValueItem
		{
			get => GetValue(ValueItemProperty);
			set => SetValue(ValueItemProperty, value);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			Loaded += (s, e) =>
			{
				// Initialize or bind properties here if needed
			};
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is CPropertyTreeItem;
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new CPropertyTreeItem();
		}
	};

	public class CPropertyTree : ItemsControl
	{
		static CPropertyTree()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CPropertyTree),
				new FrameworkPropertyMetadata(typeof(CPropertyTree)));
		}

		public CPropertyTree()
		{

		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			var splitter = GetTemplateChild("PART_Splitter") as GridSplitter;
			var nameColumn = GetTemplateChild("PART_NameColumn") as ColumnDefinition;

			if (splitter != null && nameColumn != null)
			{
				splitter.DragDelta += (s, e) =>
				{
					SetCurrentValue(NameWidthProperty, nameColumn.ActualWidth);
				};

				splitter.DragCompleted += (s, e) =>
				{
					SetCurrentValue(NameWidthProperty, nameColumn.ActualWidth);
				};
			}

			Loaded += (s, e) =>
			{

			};
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new CPropertyTreeItem();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is CPropertyTreeItem;
		}

		public static readonly DependencyProperty NameWidthProperty =
			DependencyProperty.Register(nameof(NameWidth), typeof(double), typeof(CPropertyTree),
				new FrameworkPropertyMetadata(150.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public double NameWidth
		{
			get => (double)GetValue(NameWidthProperty);
			set => SetValue(NameWidthProperty, value);
		}
	}
}
