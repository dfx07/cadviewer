using CadViewer.UIControls;
using CadViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

			Loaded += (s, e) =>
			{
				if (IsOpen)
				{
					OpenHomeMenuAnimation(true);
				}
				else
				{
					OpenHomeMenuAnimation(false);
				}
			};
		}
		void OpenHomeMenuAnimation(bool bOpen)
		{
			if (bRunAnimation || ActualWidth == 0 || ActualHeight == 0)
				return;

			bRunAnimation = true;

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

		public static readonly DependencyProperty IsOpenProperty =
		DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(HomeMenuPanel), new PropertyMetadata(false, OnIsOpenChanged));

		public bool IsOpen
		{
			get => (bool)GetValue(IsOpenProperty);
			set => SetValue(IsOpenProperty, value);
		}

		private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is HomeMenuPanel hp)
			{
				hp.OpenHomeMenuAnimation((bool)e.NewValue);
			}
		}

		private bool bRunAnimation = false;
	}
}
