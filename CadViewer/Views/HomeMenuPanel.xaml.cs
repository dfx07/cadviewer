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
			if (bRunAnimation)
				return;

			OpenHomeMenuAnimation((bool)(e.NewValue));
		}

		void OpenHomeMenuAnimation(bool bOpen)
		{
			bRunAnimation = true;

			Visibility = Visibility.Visible;

			var anim = new DoubleAnimation
			{
				From = bOpen ? -ActualWidth : 0,
				To = bOpen ? 0 : -ActualWidth,
				Duration = TimeSpan.FromMilliseconds(500),
				EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
			};

			if (bOpen)
			{
				anim.Completed += (s, e) =>
				{
					bRunAnimation = false;
				};
			}
			else
			{
				anim.Completed += (s, e) =>
				{
					Visibility = Visibility.Collapsed;
					bRunAnimation = false;
				};
			}

			SlideTransform.BeginAnimation(TranslateTransform.XProperty, anim);
		}

		private bool bRunAnimation = false;
	}
}
