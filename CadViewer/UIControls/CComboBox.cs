using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using CadViewer.Animations;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Collections.ObjectModel;

namespace CadViewer.UIControls
{
	public struct CComboBoxItemData
	{
		public string Name { get; set; }
		public object Value { get; set; }
		public string ImagePath { get; set; }

		// Constructor
		public CComboBoxItemData(string name, object value, string imagepath)
		{
			Name = name;
			Value = value;
			ImagePath = imagepath;
		}

		// Override ToString() để hiển thị thông tin dễ dàng
		public override string ToString()
		{
			return $"{Name}";
		}
	}
	public class CComboBox : ComboBox
	{
		static CComboBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CComboBox),
				new FrameworkPropertyMetadata(typeof(CComboBox)));
		}
		public CComboBox()
		{
			if (ItemsSource == null)
			{
				ItemsSource = new ObservableCollection<CComboBoxItemData>
				{
					new CComboBoxItemData("Apple", null, "Assets/Images/item26.png"),
					new CComboBoxItemData("Banana", null,"Assets/Images/search26.png"),
					new CComboBoxItemData("Cherry", null, "None"),
					new CComboBoxItemData("Date", null, null),
				};
			}

			PreviewMouseDown += CComboBox_PreviewMouseDown;
		}
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{
				ListBox dropdownListBox = (ListBox)Template.FindName("PART_DropdownList", this);
				if (dropdownListBox != null)
				{
					dropdownListBox.PreviewMouseLeftButtonUp -= PART_DropdownList_PreviewMouseLeftButtonUp;
					dropdownListBox.PreviewMouseLeftButtonUp += PART_DropdownList_PreviewMouseLeftButtonUp;
				}
			};
		}

		private void CComboBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (!this.IsDropDownOpen)
			{
				this.IsDropDownOpen = true;
			}
			else
			{
				this.IsDropDownOpen = false;
			}
		}

		private T FindParent<T>(DependencyObject child) where T : DependencyObject
		{
			DependencyObject parent = VisualTreeHelper.GetParent(child);
			while (parent != null && !(parent is T))
				parent = VisualTreeHelper.GetParent(parent);
			return parent as T;
		}

		private void PART_DropdownList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			var originalSource = e.OriginalSource as DependencyObject;

			ListBoxItem clickedItem = FindParent<ListBoxItem>(originalSource);

			if (clickedItem != null)
			{
				SelectedItem = clickedItem.Content;
				IsDropDownOpen = false;
			}
		}

		public static readonly DependencyProperty IsOpenDropDownProperty =
		DependencyProperty.Register(nameof(IsOpenDropDown), typeof(bool), typeof(CComboBox), new PropertyMetadata(false));
		public bool IsOpenDropDown
		{
			get => (bool)GetValue(IsOpenDropDownProperty);
			set => SetValue(IsOpenDropDownProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CComboBox), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty ItemHeightProperty =
		DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(CComboBox), new PropertyMetadata(double.NaN));

		public double ItemHeight
		{
			get => (double)GetValue(ItemHeightProperty);
			set => SetValue(ItemHeightProperty, value);
		}
	}
}
