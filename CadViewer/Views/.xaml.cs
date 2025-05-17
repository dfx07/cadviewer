using CadViewer.Dialogs;
using CadViewer.Interfaces;
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
	public partial class MainWindow : Window, IDialogService
	{
		DlgProgress dlgProgress = null;

		public MainWindow()
		{
			InitializeComponent();

			_MainViewModel = new MainViewModel();
			this.DataContext = _MainViewModel;
		}

		public MainViewModel _MainViewModel;


		private void ToggleButton_Checked(object sender, RoutedEventArgs e)
		{

		}

		private void ToggleBtn_Unchecked(object sender, RoutedEventArgs e)
		{

		}

		private void DoModal_Click(object sender, RoutedEventArgs e)
		{
			//DlgSetting setting = new DlgSetting(this);

			//ShowModal(setting);

			//ShowProgress("Loading...");

			CWindow win = new CWindow();


			win.Width = 800;
			win.Width = 600;
			win.Owner = this;
			win.Title = "Window";
			win.WindowStartupLocation = WindowStartupLocation.CenterOwner;

			win.ShowDialog();
		}

		public void ShowProgress(string progressMsg)
		{
			PART_ModalOverlay.Visibility = Visibility.Visible;

			if(dlgProgress == null)
			{
				dlgProgress = new DlgProgress(this);
			}

			dlgProgress.HorizontalAlignment = HorizontalAlignment.Center;
			dlgProgress.VerticalAlignment = VerticalAlignment.Center;

			dlgProgress.ProcessMessage = progressMsg;

			if (!PART_ModalOverlay.Children.Contains(dlgProgress))
			{
				PART_ModalOverlay.Children.Add(dlgProgress);
			}
		}

		public int ShowModal(IModalDialog dialog)
		{
			PART_ModalOverlay.Visibility = Visibility.Visible;

			UserControl dlg = dialog as UserControl;

			if (dlg is null)
				return -1;

			dlg.HorizontalAlignment = HorizontalAlignment.Center;
			dlg.VerticalAlignment = VerticalAlignment.Center;

			PART_ModalOverlay.Children.Add(dlg);

			return 1;
		}

		public void CloseModal(IModalDialog dialog)
		{
			PART_ModalOverlay.Visibility = Visibility.Collapsed;

			UserControl dlg = dialog as UserControl;

			if (dlg is null)
				return;

			PART_ModalOverlay.Children.Remove(dlg);

			if(dlgProgress == dlg)
			{
				dlgProgress = null;
			}
		}
	}
}
