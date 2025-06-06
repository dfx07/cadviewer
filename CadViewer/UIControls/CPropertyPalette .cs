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
