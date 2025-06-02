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
	public class PropertyItemValueData
	{

	}

	public class PropertyItemData : INotifyPropertyChanged
	{
		private string _name;
		private bool _isEnabled = true;
		private bool _isChecked;
		private bool _isVisible = true;
		private bool _isCheckable;
		private object _commandParameter;
		private bool _isExpanded = false;
		private bool _isGroup = false;

		public int Level { get; set; } = 0; // Level in the tree, used for indentation

		public string Name
		{
			get => _name;
			set { _name = value; OnPropertyChanged(nameof(Name)); }
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

		public bool IsExpanded
		{
			get => _isExpanded;
			set { _isExpanded = value; OnPropertyChanged(nameof(IsExpanded)); }
		}
		public bool IsGroup
		{
			get => _isGroup;
			set { _isGroup = value; OnPropertyChanged(nameof(IsGroup)); }
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

		public ObservableCollection<PropertyItemData> Children { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}

	public class PropertyItemGroupData : PropertyItemData
	{
		public PropertyItemGroupData()
		{
			IsGroup = true;
		}
	}

	public class PropertyItemStringData : PropertyItemData
	{
		private string _value;

		public string Value
		{
			get => _value;
			set { _value = value; OnPropertyChanged(nameof(Value)); }
		}
		public PropertyItemStringData(string value)
		{
			Value = value;
		}

		public PropertyItemStringData()
		{
			Value = "";
		}
	}

	public class PropertyItemIntegerData : PropertyItemData
	{
		private int _value;

		public int Value
		{
			get => _value;
			set { _value = value; OnPropertyChanged(nameof(Value)); }
		}

		public PropertyItemIntegerData(int value)
		{
			Value = value;
		}
		public PropertyItemIntegerData()
		{
			Value = 0;
		}
	}
}
