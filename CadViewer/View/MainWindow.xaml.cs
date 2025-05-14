using CadViewer.UIControls;
using CadViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace CadViewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			_MainViewModel = new MainViewModel();
			this.DataContext = _MainViewModel;
		}

		public MainViewModel _MainViewModel;
		private Adorner oldAdo;
		private void ToggleButton_Checked(object sender, RoutedEventArgs e)
		{
			var myTargetControl = sender as UIElement;
			if (myTargetControl == null)
				return;

			//var popupContent = CloneVisual(PopupContent);
			//if (popupContent == null)
			//	return;

			// Tạo Adorner tại vị trí (10, -40) so với target
			//oldAdo = new PopupAdorner(myTargetControl, popupContent, new Point(10, -40));



			// Gắn vào adorner layer
			var adornerLayer = AdornerLayer.GetAdornerLayer(myTargetControl);
			adornerLayer?.Add(oldAdo);
		}

		private UIElement CloneVisual(UIElement original)
		{
			if (original == null) return null;

			// Chuyển đổi thành XAML và đọc lại XAML để tạo bản sao
			string xaml = XamlWriter.Save(original);
			StringReader stringReader = new StringReader(xaml);
			XmlReader xmlReader = XmlReader.Create(stringReader);
			return (UIElement)XamlReader.Load(xmlReader);
		}

		private void ToggleBtn_Unchecked(object sender, RoutedEventArgs e)
		{
			var myTargetControl = sender as UIElement;
			if (myTargetControl == null)
				return;

			var adornerLayer = AdornerLayer.GetAdornerLayer(myTargetControl);
			adornerLayer?.Remove(oldAdo);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int c = 10;
		}
	}
}
