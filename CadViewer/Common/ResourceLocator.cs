using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CadViewer.Common
{
	[MarkupExtensionReturnType(typeof(ImageSource))]
	public class AssetImage : MarkupExtension
	{
		public string Path { get; set; }

		public AssetImage(string path)
		{
			Path = path;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new BitmapImage(new Uri($"/Assets/{Path}", UriKind.Relative));
		}
	}

}
