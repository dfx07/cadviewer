using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections;

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

	public class CommonBooleanNegationConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> !(bool)(value ?? false);

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> !(bool)(value ?? false);
	}

	public class CommonBoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is bool bVisible)
			{
				return bVisible ? Visibility.Visible : Visibility.Collapsed;
			}
			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class CommonListToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IEnumerable children)
			{
				foreach (var item in children)
					return Visibility.Visible;
			}
			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class CommonCountToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ICollection collection)
			{
				return collection.Count > 0;
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}

	public class CommonAddValueDoubleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is double dblValue && parameter is string paramStr && double.TryParse(paramStr, out double addValue))
			{
				return dblValue + addValue;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class CommonDivValueDoubleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is double dblValue && parameter is string paramStr && double.TryParse(paramStr, out double divValue))
			{
				if(divValue == 0)
					return dblValue;

				return dblValue / divValue;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class CommonHeightToCornerRadiusConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double height)
			{
				return new CornerRadius((height) / 2);
			}
			return new CornerRadius(0);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}

	public class CommonIsDoubleBoolCheckConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length < 2)
				return false;

			bool bCheck1 = values[0] as bool? ?? false;
			bool bCheck2 = values[0] as bool? ?? false;

			return bCheck1 && bCheck2;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class CommonRelativeImagePathConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string relativePath = value as string;
			if (string.IsNullOrEmpty(relativePath)) return null;

			string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
			return new BitmapImage(new Uri(fullPath));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	public class CommonPackUriConverter : IValueConverter
	{
		private const string Prefix = "pack://application:,,,/";

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string relativePath = value as string;
			if (string.IsNullOrEmpty(relativePath)) return null;

			if(relativePath == "None")
				return null;

			return $"{Prefix}{relativePath}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}

	public class CommonNumberToStringWithFormatConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double && parameter is string format)
			{
				return string.Format(format, value);
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
