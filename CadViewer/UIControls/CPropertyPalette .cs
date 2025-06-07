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
using System.Runtime.Remoting.Contexts;
using System.Xml.Linq;

namespace CadViewer.UIControls
{
	public class CPropertyPaletteItem : Control
	{
		static CPropertyPaletteItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CPropertyPaletteItem),
				new FrameworkPropertyMetadata(typeof(CPropertyPaletteItem)));
		}

		public CPropertyPaletteItem()
		{
			Loaded += CPropertyPaletteItem_Loaded;
		}

		private void CPropertyPaletteItem_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}

	public class CPropertyPaletteGroup : ItemsControl
	{
		static CPropertyPaletteGroup()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CPropertyPaletteGroup),
				new FrameworkPropertyMetadata(typeof(CPropertyPaletteGroup)));
		}

		public CPropertyPaletteGroup()
		{

		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}

		public static readonly DependencyProperty IsExpandedProperty =
		DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(CPropertyPaletteGroup), new PropertyMetadata(true));

		public bool IsExpanded
		{
			get => (bool)GetValue(IsExpandedProperty);
			set => SetValue(IsExpandedProperty, value);
		}

		private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is CPropertyPaletteGroup group)
			{

			}
		}
	}

	public class CPropertyPalette : ItemsControl
	{
		static CPropertyPalette()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CPropertyPalette),
				new FrameworkPropertyMetadata(typeof(CPropertyPalette)));
		}

		public CPropertyPalette()
		{

		}

		public void ExpandAllGroups()
		{
			//foreach (var item in Items)
			//{
			//	if (item is CPropertyPaletteGroup group)
			//	{
			//		group.IsExpanded = true;
			//		foreach (var child in group.Items)
			//		{
			//			if (child is CPropertyPaletteGroup childGroup)
			//			{
			//				childGroup.IsExpanded = true;
			//			}
			//		}
			//	}
			//}
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
			return new CPropertyPaletteGroup();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is CPropertyPaletteGroup;
		}
	}
}
