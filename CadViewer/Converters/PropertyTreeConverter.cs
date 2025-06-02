using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace CadViewer.Converters
{
	public class LevelToWidthMultiConverter : IMultiValueConverter
	{
		public double Step { get; set; } = 20;

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values[0] is int level && values[1] is double maxWidth)
			{
				return Math.Max(0, maxWidth - level * Step);
			}
			return 100.0;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}
}
