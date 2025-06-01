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
	public enum CPropertyValueType
	{
		String,
		Integer,
		Double,
		Boolean,
		Color,
		Vector2D,
		Vector3D,
		Matrix3D,
		Point2D,
		Point3D,
		Rect2D,
		Rect3D,
		Image
	}

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

			Loaded += (s, e) =>
			{

			};
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new CPropertyTreeItem(); // đây là điều bắt buộc để WPF tạo đúng container
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is CPropertyTreeItem;
		}
	}
}
