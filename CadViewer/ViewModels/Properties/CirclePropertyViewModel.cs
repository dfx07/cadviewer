using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.ViewModels
{
	public class CirclePropertyViewModel : ViewModelBase
	{
		public PropertyPaletteData Propterties { get; }

		public CirclePropertyViewModel()
			: base()
		{

			var palette = new PropertyPaletteData { Name = "Circle Property" };
			var group1 = new PropertyPaletteGroupData { Name = "General" };
			group1.Items.Add(new PropertyPaletteItemData { Name = "Color" });
			group1.Items.Add(new PropertyPaletteItemData { Name = "Layer" });
			group1.Items.Add(new PropertyPaletteItemData { Name = "Linetype" });
			group1.Items.Add(new PropertyPaletteItemData { Name = "Thickness" });

			var group2 = new PropertyPaletteGroupData { Name = "Geometry" };
			group2.Items.Add(new PropertyPaletteItemData { Name = "Center X" });
			group2.Items.Add(new PropertyPaletteItemData { Name = "Center Y" });
			group2.Items.Add(new PropertyPaletteItemData { Name = "Center Z" });
			group2.Items.Add(new PropertyPaletteItemData { Name = "Radius" });

			palette.Groups.Add(group1);
			palette.Groups.Add(group2);

			Propterties = palette;
		}

		public double Radius { get; set; }
		public double CenterX { get; set; }
		public double CenterY { get; set; }
	}
}
