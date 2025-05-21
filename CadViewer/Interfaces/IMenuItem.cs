using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
	public class MenuItemData : INotifyPropertyChanged
	{
		private string _header;
		private ImageSource _icon;
		private bool _isEnabled = true;
		private bool _isChecked;
		private bool _isVisible = true;
		private bool _isCheckable;
		private object _commandParameter;
		public bool IsSeparator { get; set; } = false;

		public string Header
		{
			get => _header;
			set { _header = value; OnPropertyChanged(nameof(Header)); }
		}

		public ImageSource Icon
		{
			get => _icon;
			set { _icon = value; OnPropertyChanged(nameof(Icon)); }
		}

		public ICommand Command { get; set; }

		public object CommandParameter
		{
			get => _commandParameter;
			set { _commandParameter = value; OnPropertyChanged(nameof(CommandParameter)); }
		}

		public bool IsEnabled
		{
			get => _isEnabled;
			set { _isEnabled = value; OnPropertyChanged(nameof(IsEnabled)); }
		}

		public bool IsChecked
		{
			get => _isChecked;
			set { _isChecked = value; OnPropertyChanged(nameof(IsChecked)); }
		}

		public bool IsCheckable
		{
			get => _isCheckable;
			set { _isCheckable = value; OnPropertyChanged(nameof(IsCheckable)); }
		}

		public bool IsVisible
		{
			get => _isVisible;
			set { _isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
		}

		public ObservableCollection<MenuItemData> Children { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
