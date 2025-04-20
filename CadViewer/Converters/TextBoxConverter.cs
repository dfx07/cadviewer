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
	public class TextBoxFocusMarginConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values[0] is Thickness margin && values[1] is bool isFocused)
			{
				return new Thickness(
					margin.Left,
					margin.Top + (isFocused ? -1 : 0),
					margin.Right,
					margin.Bottom
				);
			}
			return values[0];
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}
}
