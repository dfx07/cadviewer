using CadViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CadViewer.View
{
	/// <summary>
	/// Interaction logic for BibbonBar.xaml
	/// </summary>
	public partial class BibbonBar : UserControl
	{
		public BibbonBar()
		{
			InitializeComponent();

			Loaded += BibbonBar_Loaded;
		}
		private void BibbonBar_Loaded(object sender, RoutedEventArgs e)
		{
			if(ViewModel != null)
			{
				ViewModel.OnInitModel();

				xTabBar.SetTabInfos(ViewModel.tabBarInfos);

				xTabBar.SetListener(ViewModel);
			}
		}

		#region [Property]
		/// <summary> /////////////////////////////////////////////////////////////////////
		///  DependencyProperty for binding the model

		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(nameof(ViewModel), typeof(RibbonBarViewModel), typeof(BibbonBar), new PropertyMetadata(null));

		public RibbonBarViewModel ViewModel
		{
			get => (RibbonBarViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}
		//------------------------------------------------------------------------------/
		#endregion
	}
}
