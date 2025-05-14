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
using System.Windows.Documents;

namespace CadViewer.UIControls
{
	public class PopupAdorner : Adorner
	{
		private readonly VisualCollection _visuals;
		private readonly UIElement _child;

		public PopupAdorner(UIElement adornedElement, UIElement popupContent)
			: base(adornedElement)
		{
			_visuals = new VisualCollection(this);
			_child = popupContent;
			_visuals.Add(_child);
		}

		protected override int VisualChildrenCount => 1;
		protected override Visual GetVisualChild(int index) => _child;

		protected override Size MeasureOverride(Size constraint)
		{
			_child.Measure(constraint);
			return _child.DesiredSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			var offset = new Point(0, AdornedElement.RenderSize.Height);
			_child.Arrange(new Rect(offset, _child.DesiredSize));
			return finalSize;
		}
	}

	public class CSoftPopup : ContentControl
	{
		private AdornerLayer _adornerLayer;
		private PopupAdorner _adorner;

		static CSoftPopup()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CSoftPopup),
				new FrameworkPropertyMetadata(typeof(CSoftPopup)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{
				if (IsOpen)
				{
					Show();
				}
			};
		}

		public UIElement PlacementTarget
		{
			get => (UIElement)GetValue(PlacementTargetProperty);
			set => SetValue(PlacementTargetProperty, value);
		}

		public static readonly DependencyProperty PlacementTargetProperty =
			DependencyProperty.Register(nameof(PlacementTarget), typeof(UIElement), typeof(CSoftPopup),
				new FrameworkPropertyMetadata(null));

		public bool IsOpen
		{
			get => (bool)GetValue(IsOpenProperty);
			set => SetValue(IsOpenProperty, value);
		}

		public static readonly DependencyProperty IsOpenProperty =
			DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(CSoftPopup),
				new FrameworkPropertyMetadata(false, OnIsOpenChanged));

		private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var popup = (CSoftPopup)d;
			if ((bool)e.NewValue)
				popup.Show();
			else
				popup.Close();
		}

		private void Show()
		{
			if (PlacementTarget == null || Content == null)
				return;

			if (_adornerLayer != null)
				return;

			if (PlacementTarget is FrameworkElement fe && !fe.IsLoaded)
			{
				fe.Loaded += (_, __) => Show(); // Delay hiển thị cho đến khi đã render xong
				return;
			}

			_adornerLayer = AdornerLayer.GetAdornerLayer(PlacementTarget);
			if (_adornerLayer == null)
				return;

			if (Content is UIElement contentElement)
			{
				_adorner = new PopupAdorner(PlacementTarget, contentElement);
				_adornerLayer.Add(_adorner);
			}
		}

		private void Close()
		{
			if (_adorner != null && _adornerLayer != null)
			{
				_adornerLayer.Remove(_adorner);
				_adorner = null;
				_adornerLayer = null;
			}
		}
	}
}
