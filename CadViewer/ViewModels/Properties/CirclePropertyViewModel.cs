using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CadViewer.ViewModels
{
	public class CirclePropertyViewModel : ViewModelBase, IPropertyPaletteItemValueChangedEvent
	{
		public PropertyPaletteData Propterties { get; }

		public CirclePropertyViewModel()
			: base()
		{

			var palette = new PropertyPaletteData { Name = "Circle Property" };
			palette.CallBack = this;

			var group1 = new PropertyPaletteGroupData { Name = "General" };
			group1.Items.Add(new PropertyPaletteItemColorData { Name = "Color", Value = Colors.Red });
			group1.Items.Add(new PropertyPaletteItemSelectData { Name = "Layer", Value = 12 });
			group1.Items.Add(new PropertyPaletteItemSelectData { Name = "Linetype", Value = 12 });
			group1.Items.Add(new PropertyPaletteItemIntegerData { Name = "Thickness", Value =1 });

			var group2 = new PropertyPaletteGroupData { Name = "Geometry" };
			group2.Items.Add(new PropertyPaletteItemDoubleData { Name = "Center X" , Value = 12.4});
			group2.Items.Add(new PropertyPaletteItemDoubleData { Name = "Center Y" , Value = 12.5 });
			group2.Items.Add(new PropertyPaletteItemDoubleData { Name = "Center Z" , Value = 15.6 });
			group2.Items.Add(new PropertyPaletteItemStringData { Name = "Radius control panle", Value = "Nhap vao" });

			var group3 = new PropertyPaletteGroupData { Name = "General" };
			group3.Items.Add(new PropertyPaletteItemColorData { Name = "Color", Value = Colors.Red });
			group3.Items.Add(new PropertyPaletteItemSelectData { 
				Name = "Layer",
				Items = new ObservableCollection<PropertyPaletteItemSelectItemData>
				{
					new PropertyPaletteItemSelectItemData("Layer 1", 1),
					new PropertyPaletteItemSelectItemData("Layer 2", 2),
					new PropertyPaletteItemSelectItemData("Layer 3", 3)
				},
				Value = 1
			});
			group3.Items.Add(new PropertyPaletteItemSelectData { Name = "Linetype", Value = 12 });
			group3.Items.Add(new PropertyPaletteItemIntegerData { Name = "Thickness", Value = 1 });

			var group4 = new PropertyPaletteGroupData { Name = "Geometry" };
			group4.Items.Add(new PropertyPaletteItemIntegerData { Name = "Center X", Value = 12 });
			group4.Items.Add(new PropertyPaletteItemIntegerData { Name = "Center Y", Value = 12 });
			group4.Items.Add(new PropertyPaletteItemIntegerData { Name = "Center Z", Value = 15 });
			group4.Items.Add(new PropertyPaletteItemIntegerData { Name = "Radius control panle", Value = 20 });
			group4.Items.Add(new PropertyPaletteItemCheckData { Name = "Radius control panle", Value = true });

			palette.AddGroup(group1);
			palette.AddGroup(group2);
			palette.AddGroup(group3);
			palette.AddGroup(group4);

			Propterties = palette;
		}

		public void PropertyPaletteItem_SelectionChanged(PropertyPaletteItemSelectData item, int nNewValue)
		{
			Console.WriteLine($"Selected: {item.Name} ({item.Value})");
		}

		public void PropertyPaletteItem_TextChanged(PropertyPaletteItemStringData item, string strNewValue)
		{
			Console.WriteLine($"Text changed: {item.Name} ({item.Value})");
		}

		public void PropertyPaletteItem_IntChanged(PropertyPaletteItemIntegerData item, int strNewValue)
		{
			Console.WriteLine($"Int changed: {item.Name} ({item.Value})");
		}

		public void PropertyPaletteItem_DoubleChanged(PropertyPaletteItemDoubleData item, double strNewValue)
		{
			Console.WriteLine($"Double changed: {item.Name} ({item.Value})");
		}

		public void PropertyPaletteItem_ColorChanged(PropertyPaletteItemColorData item, Color clNewValue)
		{
			Console.WriteLine($"Color changed: {item.Name} ({item.Value})");
		}

		public void PropertyPaletteItem_CheckChanged(PropertyPaletteItemCheckData item, bool bNewValue)
		{
			Console.WriteLine($"Check changed: {item.Name} ({item.Value})");
		}

		public double Radius { get; set; }
		public double CenterX { get; set; }
		public double CenterY { get; set; }
	}
}
