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
		private Point _dragStartPoint;
		private bool _isDragging = false;
		private DropShadowEffect _effectDlg = null;

		public TranslateTransform TranslateTransform = null;

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
					if (DialogListener != null)
					{
						DialogListener.OnClose();
					}
				};
			}

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty DialogListenerProperty =
		DependencyProperty.Register(nameof(DialogListener), typeof(IModalDialog), typeof(CToastContainer), new PropertyMetadata(null));

		public IModalDialog DialogListener
		{
			get => (IModalDialog)GetValue(DialogListenerProperty);
			set => SetValue(DialogListenerProperty, value);
		}

		public static readonly DependencyProperty TitleProperty =
		DependencyProperty.Register(nameof(Title), typeof(string), typeof(CToastContainer), new PropertyMetadata(""));

		public string Title
		{
			get => (string)GetValue(TitleProperty);
			set => SetValue(TitleProperty, value);
		}

		public static readonly DependencyProperty LeftContentProperty =
		DependencyProperty.Register(nameof(LeftContent), typeof(object), typeof(CToastContainer));
		public object LeftContent
		{
			get => GetValue(LeftContentProperty);
			set => SetValue(LeftContentProperty, value);
		}

		public static readonly DependencyProperty IsFreezeProperty =
		DependencyProperty.Register(nameof(IsFreeze), typeof(bool), typeof(CToastContainer), new PropertyMetadata(false));
		public bool IsFreeze
		{
			get => (bool)GetValue(IsFreezeProperty);
			set => SetValue(IsFreezeProperty, value);
		}
	}
}
