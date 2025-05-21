using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CadViewer.Interfaces
{
	public class MenuItemData
	{
		public string Header { get; set; }
		public ICommand Command { get; set; }
		public object CommandParameter { get; set; }

		public bool IsEnabled { get; set; } = true;
		public Visibility Visibility { get; set; } = Visibility.Visible;

		public bool IsCheckable { get; set; } = false;
		public bool IsChecked { get; set; } = false;

		public ImageSource Icon { get; set; }

		public List<MenuItemData> Children { get; set; } = new List<MenuItemData>();
	}
}
