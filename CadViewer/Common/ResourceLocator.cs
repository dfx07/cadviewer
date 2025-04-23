using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace CadViewer.Common
{
	[MarkupExtensionReturnType(typeof(ImageSource))]
	public class PackImage : MarkupExtension
	{
		public string Path { get; set; }

		public PackImage() { }
		public PackImage(string path)
		{
			Path = path;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			if (string.IsNullOrWhiteSpace(Path)) return null;
			var fullUri = $"pack://application:,,,/{Path}";
			return new System.Windows.Media.Imaging.BitmapImage(new Uri(fullUri));
		}
	}
}
