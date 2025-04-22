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

		// Constructor
		public CComboBoxItemData(string name, object value)
		{
			Name = name;
			Value = value;
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
					new CComboBoxItemData("Apple", null),
					new CComboBoxItemData("Banana", null),
					new CComboBoxItemData("Cherry", null),
					new CComboBoxItemData("Date", null),
					new CComboBoxItemData("Grape", null),
					new CComboBoxItemData("Mango", null)
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
			// Kiểm tra nếu ComboBox không đang mở dropdown
			if (!this.IsDropDownOpen)
			{
				this.IsDropDownOpen = true;  // Mở dropdown
			}
			else
			{
				this.IsDropDownOpen = false; // Đóng dropdown
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
	}
}
