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
			double width = 110;

			if (width <= 0)
				width = 1;

			return (value / max) * width;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

}
