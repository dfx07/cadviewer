using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CadViewer.Components
{
	public class CPanelControl : UserControl, INotifyPropertyChanged
	{
		public CPanelControl()
		{
			SetCurrentValue(PN_CornerRadiusProperty, new CornerRadius(15));
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
				return false;

			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}
		public T FindTemplateChild<T>(FrameworkElement parent, string name) where T : FrameworkElement
		{
			if (parent is Control control)
			{
				return control.Template.FindName(name, control) as T;
			}

			return null;
		}

		public CornerRadius PN_CornerRadius
		{
			get { return (CornerRadius)GetValue(PN_CornerRadiusProperty); }
			set { SetValue(PN_CornerRadiusProperty, value); }
		}

		public static readonly DependencyProperty PN_CornerRadiusProperty =
			DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CPanelControl),
				new PropertyMetadata());
	}
}
