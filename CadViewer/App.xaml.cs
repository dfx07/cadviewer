using CadViewer.Services;
using CadViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CadViewer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var mainWindow = new MainWindow();

			// Initialize services
			ServiceLocator.OverlayService = new OverlayService(mainWindow);

			mainWindow.DataContext = new MainViewModel();

			mainWindow.Show();
		}
	}
}
