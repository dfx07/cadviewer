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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CadViewer.Views
{
	/// <summary>
	/// Interaction logic for HomeMenuPanel.xaml
	/// </summary>
	public partial class HomeMenuPanel : UserControl
	{
		public HomeMenuPanel()
		{
			InitializeComponent();

			this.IsVisibleChanged += HomeMenuPanel_IsVisibleChanged;
		}

		private void HomeMenuPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			OpenHomeMenu((bool)(e.NewValue));
		}

		void OpenHomeMenu(bool bOpen)
		{
			if (bOpen)
			{
				var expandAnim = new DoubleAnimation
				{
					From = -ActualWidth,
					To = 0,
					Duration = TimeSpan.FromMilliseconds(500),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
				};
				SlideTransform.BeginAnimation(TranslateTransform.XProperty, expandAnim);
			}
			else
			{
				var expandAnim = new DoubleAnimation
				{
					To = -ActualWidth,
					Duration = TimeSpan.FromMilliseconds(500),
					EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
				};

				expandAnim.Completed += (s, e) =>
				{
					int a = 10;
				};

				SlideTransform.BeginAnimation(TranslateTransform.XProperty, expandAnim);
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			OpenHomeMenu(false);
		}
	}
}
