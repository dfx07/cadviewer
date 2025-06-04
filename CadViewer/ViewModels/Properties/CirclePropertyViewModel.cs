using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.ViewModels.Properties
{
	public class CirclePropertyViewModel : ViewModelBase
	{
		public CirclePropertyViewModel()
			: base()
		{
		}

		public double Radius { get; set; }
		public double CenterX { get; set; }
		public double CenterY { get; set; }
	}
}
