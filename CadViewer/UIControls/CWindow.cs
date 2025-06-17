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
using CadViewer.Services;

namespace CadViewer.UIControls
{
	public class CWindow : Window
	{
		static CWindow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CWindow),
				new FrameworkPropertyMetadata(typeof(CWindow)));
		}

		public CWindow()
		{
			this.AllowsTransparency = true;
			this.WindowStyle = WindowStyle.None;
			this.Background = Brushes.Transparent;
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (GetTemplateChild("PART_CloseButton") is Button closeButton)
			{
				closeButton.Click += (s, e) =>
				{
					this.Close();
				};
			}

			if (GetTemplateChild("PART_MinimizeButton") is Button minimizeButton)
			{
				minimizeButton.Click += (s, e) =>
				{
					this.WindowState = WindowState.Minimized;
				};
			}

			if (GetTemplateChild("PART_MaximizeButton") is Button maximizeButton)
			{
				maximizeButton.Click += (s, e) =>
				{
					this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
				};
			}

			if (GetTemplateChild("PART_TitleBar") is UIElement titleBar)
				titleBar.MouseLeftButtonDown += (s, e) =>
				{
					if (e.ClickCount == 2)
						this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
					else
						this.DragMove();
				};

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty DialogListenerProperty =
		DependencyProperty.Register(nameof(DialogListener), typeof(IModalDialog), typeof(CWindow), new PropertyMetadata(null));

		public IModalDialog DialogListener
		{
			get => (IModalDialog)GetValue(DialogListenerProperty);
			set => SetValue(DialogListenerProperty, value);
		}

		public static readonly DependencyProperty IconSourceProperty =
		DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(CWindow), new PropertyMetadata(null));

		public ImageSource IconSource
		{
			get => (ImageSource)GetValue(IconSourceProperty);
			set => SetValue(IconSourceProperty, value);
		}

		public static readonly DependencyProperty IsFreezeProperty =
		DependencyProperty.Register(nameof(IsFreeze), typeof(bool), typeof(CWindow), new PropertyMetadata(false));
		public bool IsFreeze
		{
			get => (bool)GetValue(IsFreezeProperty);
			set => SetValue(IsFreezeProperty, value);
		}
	}
}
