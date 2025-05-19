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
using System.Windows.Threading;
using CadViewer.Interfaces;
using System.Windows.Media.Effects;

namespace CadViewer.UIControls
{
	public class CToastContainer : ContentControl
	{
		public DropShadowEffect ToastDropShadowEffect = null;
		private bool _SmoothMove = false;

		static CToastContainer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CToastContainer),
				new FrameworkPropertyMetadata(typeof(CToastContainer)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (GetTemplateChild("PART_CloseBtn") is Button closeBtn)
			{
				closeBtn.Click += (s, e) =>
				{
					if (ToastListener != null)
					{
						ToastListener.OnCloseToast();
					}
				};
			}

			if (GetTemplateChild("PART_DropShadow") is DropShadowEffect sd)
			{
				ToastDropShadowEffect = sd;

				if (_SmoothMove)
					ToastDropShadowEffect.Opacity = 0.0;
			}

			Loaded += (s, e) =>
			{

			};
		}

		public void SmoothMove(bool bUse)
		{
			_SmoothMove = bUse;
		}

		public void HideDropShadow()
		{
			if(ToastDropShadowEffect != null)
				ToastDropShadowEffect.Opacity = 0.0;
		}

		public void RestoreDropShadow()
		{
			if (ToastDropShadowEffect != null)
				ToastDropShadowEffect.Opacity = 0.2;
		}

		public static readonly DependencyProperty ToastListenerProperty =
		DependencyProperty.Register(nameof(ToastListener), typeof(IToast), typeof(CToastContainer), new PropertyMetadata(null));

		public IToast ToastListener
		{
			get => (IToast)GetValue(ToastListenerProperty);
			set => SetValue(ToastListenerProperty, value);
		}

		public static readonly DependencyProperty TitleProperty =
		DependencyProperty.Register(nameof(Title), typeof(string), typeof(CToastContainer), new PropertyMetadata(""));

		public string Title
		{
			get => (string)GetValue(TitleProperty);
			set => SetValue(TitleProperty, value);
		}

		public static readonly DependencyProperty LeftContentProperty =
		DependencyProperty.Register(nameof(LeftContent), typeof(object), typeof(CToastContainer), null);

		public object LeftContent
		{
			get => GetValue(LeftContentProperty);
			set => SetValue(LeftContentProperty, value);
		}

		//public static readonly DependencyProperty ContentProperty =
		//DependencyProperty.Register(nameof(Content), typeof(object), typeof(CToastContainer), null);

		//public object Content
		//{
		//	get => GetValue(ContentProperty);
		//	set => SetValue(ContentProperty, value);
		//}

		public static readonly DependencyProperty IsFreezeProperty =
		DependencyProperty.Register(nameof(IsFreeze), typeof(bool), typeof(CToastContainer), new PropertyMetadata(false));
		public bool IsFreeze
		{
			get => (bool)GetValue(IsFreezeProperty);
			set => SetValue(IsFreezeProperty, value);
		}
	}
}
