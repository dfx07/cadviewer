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

		public PropertyPaletteItemData()
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
