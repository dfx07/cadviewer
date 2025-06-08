using CadViewer.Common;
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
	public class PropertyPaletteBase : NotifyPropertyChanged
	{

	}

	public interface IPropertyPaletteItemValueChangedEvent
	{
		void PropertyPaletteItem_SelectionChanged(PropertyPaletteItemSelectData item, int nNewValue);
		void PropertyPaletteItem_TextChanged(PropertyPaletteItemStringData item, string strNewValue);
		void PropertyPaletteItem_IntChanged(PropertyPaletteItemIntegerData item, int nNewValue);
		void PropertyPaletteItem_DoubleChanged(PropertyPaletteItemDoubleData item, double dblNewValue);
		void PropertyPaletteItem_ColorChanged(PropertyPaletteItemColorData item, Color clNewValue);
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

		public PropertyPaletteData propertyPaletteData { get; set; } = null;

		public PropertyPaletteItemData()
		{

		}
	}

	/// <summary>
	/// Represents a string property item in the property palette.
	/// </summary>

	public class PropertyPaletteItemStringData : PropertyPaletteItemData
	{

		private string _value;

		public string Value
		{
			get => _value;
			set => SetProperty(ref _value, value, OnValueChanged);
		}

		private void OnValueChanged()
		{
			propertyPaletteData?.PropertyPaletteItem_TextChanged(this, Value);
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
			set => SetProperty(ref _value, value, OnValueChanged);
		}

		private void OnValueChanged()
		{
			propertyPaletteData?.PropertyPaletteItem_IntChanged(this, Value);
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
			set => SetProperty(ref _value, value, OnValueChanged);
		}

		private void OnValueChanged()
		{
			propertyPaletteData?.PropertyPaletteItem_DoubleChanged(this, Value);
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
			set => SetProperty(ref _value, value, OnValueChanged);
		}

		private void OnValueChanged()
		{
			propertyPaletteData?.PropertyPaletteItem_ColorChanged(this, Value);
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

			set => SetProperty(ref _value, value, OnValueChanged);
		}

		private void OnValueChanged()
		{
			propertyPaletteData?.PropertyPaletteItem_SelectionChanged(this, Value);
		}

		PropertyPaletteItemSelectItemData GetItem(int idx)
		{
			if (idx< 0 || idx >= Items.Count)
			{
				throw new ArgumentOutOfRangeException(nameof(idx), "Index is out of range.");
			}
			return Items[idx];
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

	public class PropertyPaletteData : PropertyPaletteBase, IPropertyPaletteItemValueChangedEvent
	{
		private string _name;

		public string Name
		{
			get => _name;
			set { _name = value; OnPropertyChanged(nameof(Name)); }
		}

		public ObservableCollection<PropertyPaletteGroupData> Groups { get; set; } = new ObservableCollection<PropertyPaletteGroupData>();

		public IPropertyPaletteItemValueChangedEvent CallBack { get; set; } = null;


		public void AddGroup(PropertyPaletteGroupData group)
		{
			foreach (var item in group.Items)
			{
				item.propertyPaletteData = this;
			}

			Groups.Add(group);
		}

		public void PropertyPaletteItem_SelectionChanged(PropertyPaletteItemSelectData item, int nNewValue)
		{
			CallBack?.PropertyPaletteItem_SelectionChanged(item, nNewValue);
		}
		public void PropertyPaletteItem_TextChanged(PropertyPaletteItemStringData item, string strNewValue)
		{
			CallBack?.PropertyPaletteItem_TextChanged(item, strNewValue);
		}
		public void PropertyPaletteItem_IntChanged(PropertyPaletteItemIntegerData item, int nNewValue)
		{
			CallBack?.PropertyPaletteItem_IntChanged(item, nNewValue);
		}

		public void PropertyPaletteItem_DoubleChanged(PropertyPaletteItemDoubleData item, double dblNewValue)
		{
			CallBack?.PropertyPaletteItem_DoubleChanged(item, dblNewValue);
		}
		public void PropertyPaletteItem_ColorChanged(PropertyPaletteItemColorData item, Color clNewValue)
		{
			CallBack?.PropertyPaletteItem_ColorChanged(item, clNewValue);
		}

		public PropertyPaletteData()
		{

		}
	}
}
