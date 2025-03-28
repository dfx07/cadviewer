using System;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CadViewer.Components
{
	public class BorderThicknessConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is double width)
			{
				int add = 0;

				if(width < Math.Abs(width) + 1)
				{
					add = 1;
				}

				double thickness = 2 + add; // Tỷ lệ 0.5% của Window Width
				return new Thickness(thickness);
			}
			return new Thickness(2);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}

	public class CornerRadiusConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is CornerRadius radius && parameter is string paramStr && double.TryParse(paramStr, out double subtractValue))
			{
				return new CornerRadius(
					Math.Max(0, radius.TopLeft - subtractValue),
					Math.Max(0, radius.TopRight - subtractValue),
					Math.Max(0, radius.BottomRight - subtractValue),
					Math.Max(0, radius.BottomLeft - subtractValue)
				);
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}

	public class InvertConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Thickness thickness)
			{
				return new Thickness(-thickness.Left, -thickness.Top, -thickness.Right, -thickness.Bottom);
			}
			return new Thickness(0);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}
