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
	public class CDialogContainer : ContentControl
	{
		private Point _dragStartPoint;
		private bool _isDragging = false;
		private TranslateTransform _translateDlg = null;
		private DropShadowEffect _effectDlg = null;

		static CDialogContainer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CDialogContainer),
				new FrameworkPropertyMetadata(typeof(CDialogContainer)));
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

			if (GetTemplateChild("PART_Translate") is TranslateTransform translate)
			{
				_translateDlg = translate;
			}

			if (GetTemplateChild("PART_DropShadow") is DropShadowEffect effect)
			{
				_effectDlg = effect;
			}

			if (GetTemplateChild("PART_TitleDlg") is Border titleBorder)
			{
				titleBorder.MouseLeftButtonDown += (s, e) =>
				{
					if(IsFreeze)
						return;

					_dragStartPoint = e.GetPosition(null);
					_isDragging = true;
					_effectDlg.Opacity = 0;

					titleBorder.CaptureMouse();
				};

				titleBorder.MouseLeftButtonUp += (s, e) =>
				{
					if (IsFreeze)
						return;

					_effectDlg.Opacity = 0.2;
					_isDragging = false;
					titleBorder.ReleaseMouseCapture();
				};

				titleBorder.MouseMove += (s, e) =>
				{
					if (IsFreeze)
						return;

					if (_isDragging)
					{
						Point currentPosition = e.GetPosition(null);
						double offsetX = currentPosition.X - _dragStartPoint.X;
						double offsetY = currentPosition.Y - _dragStartPoint.Y;

						_translateDlg.X = Math.Round(_translateDlg.X + offsetX);
						_translateDlg.Y = Math.Round(_translateDlg.Y + offsetY);

						_dragStartPoint = currentPosition;
					}
				};
			}

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty DialogListenerProperty =
		DependencyProperty.Register(nameof(DialogListener), typeof(IModalDialog), typeof(CDialogContainer), new PropertyMetadata(null));

		public IModalDialog DialogListener
		{
			get => (IModalDialog)GetValue(DialogListenerProperty);
			set => SetValue(DialogListenerProperty, value);
		}

		public static readonly DependencyProperty TitleProperty =
		DependencyProperty.Register(nameof(Title), typeof(string), typeof(CDialogContainer), new PropertyMetadata(""));

		public string Title
		{
			get => (string)GetValue(TitleProperty);
			set => SetValue(TitleProperty, value);
		}

		public static readonly DependencyProperty IconSourceProperty =
		DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(CDialogContainer), new PropertyMetadata(null));

		public ImageSource IconSource
		{
			get => (ImageSource)GetValue(IconSourceProperty);
			set => SetValue(IconSourceProperty, value);
		}

		public static readonly DependencyProperty IsFreezeProperty =
		DependencyProperty.Register(nameof(IsFreeze), typeof(bool), typeof(CDialogContainer), new PropertyMetadata(false));
		public bool IsFreeze
		{
			get => (bool)GetValue(IsFreezeProperty);
			set => SetValue(IsFreezeProperty, value);
		}
	}
}
