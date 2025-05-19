using CadViewer.Interfaces;
using CadViewer.UIControls;
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

namespace CadViewer.Dialogs
{
	public partial class ToastMessage : ToastControl
	{
		public ToastMessage(IToastService toastService):
			base(toastService)
		{
			InitializeComponent();
		}

		protected override bool OnCreateToast(ref ToastData _toastData)
		{
			Message = _toastData.Message;
			Title = _toastData.Title;
			ToastType = _toastData.ToastType;

			var storyboard = new Storyboard();

			var fadeOutAnim = new DoubleAnimation
			{
				From = 1.0,
				To = 0.0,
				Duration = _toastData.Duration,
				BeginTime = TimeSpan.FromMilliseconds(100),
				EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseIn }
			};

			Storyboard.SetTarget(fadeOutAnim, this);
			Storyboard.SetTargetProperty(fadeOutAnim, new PropertyPath("Opacity"));

			storyboard.Children.Add(fadeOutAnim);

			storyboard.Completed += (s, e) =>
			{
				OnCloseToast();
			};
			storyboard.Begin();

			var transAnim = new DoubleAnimation
			{
				From = 300,
				To = 0,
				Duration = TimeSpan.FromSeconds(0.9),
				EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
			};

			transAnim.Completed += (s, e) =>
			{

			};

			PART_Translate.BeginAnimation(TranslateTransform.XProperty, transAnim);
			return true;
		}

		public static readonly DependencyProperty MessageProperty =
		DependencyProperty.Register(nameof(Message), typeof(string), typeof(ToastMessage), new PropertyMetadata("..."));

		public string Message
		{
			get => (string)GetValue(MessageProperty);
			set => SetValue(MessageProperty, value);
		}

		public static readonly DependencyProperty TitleProperty =
		DependencyProperty.Register(nameof(Title), typeof(string), typeof(ToastMessage), new PropertyMetadata(""));

		public string Title
		{
			get => (string)GetValue(TitleProperty);
			set => SetValue(TitleProperty, value);
		}

		public static readonly DependencyProperty IconSourceProperty =
		DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(ToastMessage), new PropertyMetadata(null));

		public ImageSource IconSource
		{
			get => (ImageSource)GetValue(IconSourceProperty);
			set => SetValue(IconSourceProperty, value);
		}

		public static readonly DependencyProperty ToastTypeProperty =
		DependencyProperty.Register(nameof(ToastType), typeof(EToastMessageType), typeof(ToastMessage), new PropertyMetadata(EToastMessageType.Info));

		public EToastMessageType ToastType
		{
			get => (EToastMessageType)GetValue(ToastTypeProperty);
			set => SetValue(ToastTypeProperty, value);
		}
	}
}
