using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
			group1.Items.Add(new PropertyPaletteItemColorData { Name = "Color", Value = Colors.Red });
			group1.Items.Add(new PropertyPaletteItemSelectData { Name = "Layer", Value = 12 });
			group1.Items.Add(new PropertyPaletteItemSelectData { Name = "Linetype", Value = 12 });
			group1.Items.Add(new PropertyPaletteItemIntegerData { Name = "Thickness", Value =1 });

			var group2 = new PropertyPaletteGroupData { Name = "Geometry" };
			group2.Items.Add(new PropertyPaletteItemIntegerData { Name = "Center X" , Value = 12});
			group2.Items.Add(new PropertyPaletteItemIntegerData { Name = "Center Y" , Value = 12 });
			group2.Items.Add(new PropertyPaletteItemIntegerData { Name = "Center Z" , Value = 15 });
			group2.Items.Add(new PropertyPaletteItemIntegerData { Name = "Radius control panle", Value = 20 });

			var group3 = new PropertyPaletteGroupData { Name = "General" };
			group3.Items.Add(new PropertyPaletteItemColorData { Name = "Color", Value = Colors.Red });
			group3.Items.Add(new PropertyPaletteItemSelectData { Name = "Layer", Value = 12 });
			group3.Items.Add(new PropertyPaletteItemSelectData { Name = "Linetype", Value = 12 });
			group3.Items.Add(new PropertyPaletteItemIntegerData { Name = "Thickness", Value = 1 });

			var group4 = new PropertyPaletteGroupData { Name = "Geometry" };
			group4.Items.Add(new PropertyPaletteItemIntegerData { Name = "Center X", Value = 12 });
			group4.Items.Add(new PropertyPaletteItemIntegerData { Name = "Center Y", Value = 12 });
			group4.Items.Add(new PropertyPaletteItemIntegerData { Name = "Center Z", Value = 15 });
			group4.Items.Add(new PropertyPaletteItemIntegerData { Name = "Radius control panle", Value = 20 });

			palette.Groups.Add(group1);
			palette.Groups.Add(group2);
			palette.Groups.Add(group3);
			palette.Groups.Add(group4);

			Propterties = palette;
		}

		public double Radius { get; set; }
		public double CenterX { get; set; }
		public double CenterY { get; set; }
	}
}
