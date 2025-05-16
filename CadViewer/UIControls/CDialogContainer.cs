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

namespace CadViewer.UIControls
{
	public class CDialogContainer : ContentControl
	{
		static CDialogContainer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CDialogContainer),
				new FrameworkPropertyMetadata(typeof(CDialogContainer)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty CloseHandlerProperty =
		DependencyProperty.Register(nameof(CloseHandler), typeof(Action<object, RoutedEventArgs>), typeof(CDialogContainer), new PropertyMetadata(null));

		public Action<object, RoutedEventArgs>? CloseHandler
		{
			get => (Action<object, RoutedEventArgs>?)GetValue(CloseHandlerProperty);
			set => SetValue(CloseHandlerProperty, value);
		}
	}
}
