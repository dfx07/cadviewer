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
	public class ToggleButtonHeightToCornerRadiusConverter : IValueConverter
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

	/**
	 * Convert height to a circle radius for ToggleButton
	 * */
	public class ToggleButtonHeightToThumbWidthConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double height)
			{
				return (height + 4) / 2;
			}
			return 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}
	public class ToggleButtonThumbXPositionConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			double totalWidth = (double)values[0]; // ToggleButton.ActualWidth
			double thumbWidth = (double)values[1]; // Ellipse.Width
			double margin = 4; // tổng margin trái + phải (2 mỗi bên)

			return totalWidth - thumbWidth - margin;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class ToggleButtonExpandWidthConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double height)
				return height + 8;

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class ToggleButtonThumbMoveConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length >= 2 &&
				values[0] is double totalWidth &&
				values[1] is Thickness margin)
			{
				totalWidth = (double)values[0]; // ToggleButton.ActualWidth
				double thumbWidth = (double)values[1]; // Ellipse.Width
				double marg = margin.Right; // tổng margin trái + phải (2 mỗi bên)

				return totalWidth - thumbWidth - marg;
			}

			return 0;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class ToggleButtonCheckNSelectColorConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length < 3) return DependencyProperty.UnsetValue;

			bool isChecked = values[0] as bool? == true;
			string onKey = values[1] as string;
			string offKey = values[2] as string;

			string selectedKey = isChecked ? onKey : offKey;

			if (!string.IsNullOrEmpty(selectedKey) &&
				Application.Current.Resources[selectedKey] is Color color)
			{
				return color;
			}

			return Colors.Transparent;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
