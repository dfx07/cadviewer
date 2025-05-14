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
using System.IO;
using System.Xml;
using System.Windows.Markup;

namespace CadViewer.UIControls
{
	public class PopupAdorner : Adorner
	{
		private readonly VisualCollection _visuals;
		private UIElement _contentControl;
		private Point _offset;

		public PopupAdorner(UIElement adornedElement, UIElement content, Point offset)
			: base(adornedElement)
		{
			_visuals = new VisualCollection(this);
			_offset = offset;

			_contentControl = content;

			_visuals.Add(_contentControl);
		}

		protected override int VisualChildrenCount => _visuals.Count;
		protected override Visual GetVisualChild(int index) => _visuals[index];

		protected override Size MeasureOverride(Size constraint)
		{
			_contentControl.Measure(constraint);
			return _contentControl.DesiredSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			_contentControl.Arrange(new Rect(_offset, _contentControl.DesiredSize));
			return finalSize;
		}

		public void SetOffset(Point offset)
		{
			_offset = offset;
			InvalidateArrange();
		}

		public UIElement ContentControl => _contentControl;
	}
	public class CSoftPopup : ContentControl
	{
		private PopupAdorner _adorner;

		static CSoftPopup()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CSoftPopup), new FrameworkPropertyMetadata(typeof(CSoftPopup)));
		}

		public CSoftPopup()
		{
			Loaded += CSoftPopup_Loaded;
			Unloaded += CSoftPopup_Unloaded;
		}

		#region Dependency Properties

		public static readonly DependencyProperty IsOpenProperty =
			DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(CSoftPopup), new PropertyMetadata(false, OnIsOpenChanged));

		public bool IsOpen
		{
			get => (bool)GetValue(IsOpenProperty);
			set => SetValue(IsOpenProperty, value);
		}

		private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var popup = (CSoftPopup)d;
			popup.UpdatePopup();
		}

		public static readonly DependencyProperty PlacementTargetProperty =
			DependencyProperty.Register(nameof(PlacementTarget), typeof(UIElement), typeof(CSoftPopup), new PropertyMetadata(null));

		public UIElement PlacementTarget
		{
			get => (UIElement)GetValue(PlacementTargetProperty);
			set => SetValue(PlacementTargetProperty, value);
		}

		public static readonly DependencyProperty HorizontalOffsetProperty =
			DependencyProperty.Register(nameof(HorizontalOffset), typeof(double), typeof(CSoftPopup), new PropertyMetadata(0.0));

		public double HorizontalOffset
		{
			get => (double)GetValue(HorizontalOffsetProperty);
			set => SetValue(HorizontalOffsetProperty, value);
		}

		public static readonly DependencyProperty VerticalOffsetProperty =
			DependencyProperty.Register(nameof(VerticalOffset), typeof(double), typeof(CSoftPopup), new PropertyMetadata(0.0));

		public double VerticalOffset
		{
			get => (double)GetValue(VerticalOffsetProperty);
			set => SetValue(VerticalOffsetProperty, value);
		}

		#endregion

		private void CSoftPopup_Loaded(object sender, RoutedEventArgs e)
		{
			if (IsOpen)
			{
				ShowPopup();
			}
		}

		private void CSoftPopup_Unloaded(object sender, RoutedEventArgs e)
		{
			RemoveAdorner();
		}

		private void UpdatePopup()
		{
			if (!IsLoaded) return;

			if (IsOpen)
				ShowPopup();
			else
				RemoveAdorner();
		}

		private UIElement CloneCSoftPopup(CSoftPopup original)
		{
			string xaml = XamlWriter.Save(original);
			StringReader stringReader = new StringReader(xaml);
			XmlReader xmlReader = XmlReader.Create(stringReader);
			UIElement clonedElement = (UIElement)XamlReader.Load(xmlReader);
			return clonedElement;
		}
		private void ShowPopup()
		{
			RemoveAdorner();

			if (PlacementTarget == null || Content == null)
				return;

			var layer = AdornerLayer.GetAdornerLayer(PlacementTarget);
			if (layer == null)
				return;

			var clonedPopup = CloneCSoftPopup(this);

			var offset = new Point(HorizontalOffset, VerticalOffset);

			_adorner = new PopupAdorner(PlacementTarget, clonedPopup, offset);
			layer.Add(_adorner);
		}

		private void RemoveAdorner()
		{
			if (_adorner != null)
			{
				var layer = AdornerLayer.GetAdornerLayer(PlacementTarget);
				layer?.Remove(_adorner);
				_adorner = null;
			}
		}
	}
}
