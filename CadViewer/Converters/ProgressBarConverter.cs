using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace CadViewer.Converters
{
	public class ProgressBarWidthConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			double value = System.Convert.ToDouble(values[0]);
			double max = System.Convert.ToDouble(values[1]);
			double trackWidth = System.Convert.ToDouble(values[2]);

			if (trackWidth <= 0)
				trackWidth = 1;

			double ratio = Math.Min(value / max, 1.0); // hạn chế tối đa 100%
			return ratio * trackWidth;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class ProgressBarDisplayConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length < 3 || values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
				return "";

			double val = System.Convert.ToDouble(values[0]);
			double min = System.Convert.ToDouble(values[1]);
			double max = System.Convert.ToDouble(values[2]);
			string format = values[3]?.ToString() ?? "_val_";

			return format
						.Replace("_val_", val.ToString("F0", culture))
						.Replace("_min_", min.ToString("F0", culture))
						.Replace("_max_", max.ToString("F0", culture));
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
			throw new NotImplementedException();
	}
}
