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
	public class PropertyPaletteBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}

	public class PropertyPaletteItemData : PropertyPaletteBase
	{
		private string _name = "";
		private bool _isEnabled = true;
		private bool _isVisible = true;
		private bool _isReadOnly = false;

		public string Name
		{
			get => _name;
			set { _name = value; OnPropertyChanged(nameof(Name)); }
		}

		public bool IsEnabled
		{
			get => _isEnabled;
			set { _isEnabled = value; OnPropertyChanged(nameof(IsEnabled)); }
		}
		public bool IsVisible
		{
			get => _isVisible;
			set { _isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
		}

		public bool IsReadOnly
		{
			get => _isReadOnly;
			set { _isReadOnly = value; OnPropertyChanged(nameof(IsReadOnly)); }
		}

		public PropertyPaletteItemData()
		{

		}
	}

	public class PropertyPaletteItemStringData : PropertyPaletteItemData
	{
		private string _value;

		public string Value
		{
			get => _value;
			set { _value = value; OnPropertyChanged(nameof(Value)); }
		}

		public PropertyPaletteItemStringData()
		{
		}
	}

	public class PropertyPaletteItemIntegerData : PropertyPaletteItemData
	{
		private int _value;

		public int Value
		{
			get => _value;
			set { _value = value; OnPropertyChanged(nameof(Value)); }
		}

		public PropertyPaletteItemIntegerData()
		{
		}
	}

	public class PropertyPaletteItemDoubleData : PropertyPaletteItemData
	{
		private double _value;

		public double Value
		{
			get => _value;
			set { _value = value; OnPropertyChanged(nameof(Value)); }
		}

		public PropertyPaletteItemDoubleData()
		{
		}
	}

	public class PropertyPaletteItemColorData : PropertyPaletteItemData
	{
		private Color _value;

		public Color Value
		{
			get => _value;
			set { _value = value; OnPropertyChanged(nameof(Value)); }
		}

		public PropertyPaletteItemColorData()
		{
		}
	}

	public class PropertyPaletteItemSelectItemData
	{
		public string Name { get; set; }
		public object Value { get; set; }
		public PropertyPaletteItemSelectItemData(string name, object value)
		{
			Name = name;
			Value = value;
		}

		public override string ToString() => Name;
	}

	public class PropertyPaletteItemSelectData : PropertyPaletteItemData
	{
		private ObservableCollection<PropertyPaletteItemSelectItemData> _items;
		public ObservableCollection<PropertyPaletteItemSelectItemData> Items
		{
			get => _items;
			set { _items = value; OnPropertyChanged(nameof(Items)); }
		}

		private int _value;

		public int Value
		{
			get => _value;
			set { _value = value; OnPropertyChanged(nameof(Value)); }
		}

		public PropertyPaletteItemSelectData()
		{

		}
	}

	public class PropertyPaletteGroupData : PropertyPaletteBase
	{
		private string _name;

		public string Name
		{
			get => _name;
			set { _name = value; OnPropertyChanged(nameof(Name)); }
		}

		private bool _isExpanded = false;
		public bool IsExpanded
		{
			get => _isExpanded;
			set { _isExpanded = value; OnPropertyChanged(nameof(IsExpanded)); }
		}

		public ObservableCollection<PropertyPaletteItemData> Items { get; } = new ObservableCollection<PropertyPaletteItemData>();

		public PropertyPaletteGroupData()
		{

		}
	}

	public class PropertyPaletteData : PropertyPaletteBase
	{
		private string _name;

		public string Name
		{
			get => _name;
			set { _name = value; OnPropertyChanged(nameof(Name)); }
		}

		public ObservableCollection<PropertyPaletteGroupData> Groups { get; } = new ObservableCollection<PropertyPaletteGroupData>();

		public PropertyPaletteData()
		{

		}
	}
}
