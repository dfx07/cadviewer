using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace CadViewer.Converters
{
	public class CommonAddThicknessConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Thickness thickness && parameter is string paramStr && double.TryParse(paramStr, out double addValue))
			{
				return new Thickness(
					Math.Max(0, thickness.Left + addValue),
					Math.Max(0, thickness.Top + addValue),
					Math.Max(0, thickness.Right + addValue),
					Math.Max(0, thickness.Bottom + addValue)
				);
			}
			return new Thickness(0);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
	public class CommonAddRadiusConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is CornerRadius radius && parameter is string paramStr && double.TryParse(paramStr, out double addValue))
			{
				return new CornerRadius(
					Math.Max(0, radius.TopLeft + addValue),
					Math.Max(0, radius.TopRight + addValue),
					Math.Max(0, radius.BottomRight + addValue),
					Math.Max(0, radius.BottomLeft + addValue)
				);
			}
			return new CornerRadius(0);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}

	public class CommonColorKeyToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string colorKey && Application.Current.Resources[colorKey] is Color color)
			{
				return color;
			}
			return Colors.Transparent;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
	public class CommonSetDoubleDefaultConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			double dblValue = (double)values[0];
			double dblDefValue = (double)values[1];

			return !double.IsNaN(dblValue) ? dblValue : dblDefValue;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
