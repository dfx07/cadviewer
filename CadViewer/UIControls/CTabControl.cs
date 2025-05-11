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
	public class CTabControl : TabControl
	{
		static CTabControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CTabControl),
				new FrameworkPropertyMetadata(typeof(CTabControl)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			SelectionChanged += CTabControl_SelectionChanged;
			SizeChanged += CTabControl_SizeChanged;

			Loaded += (s, e) =>
			{

			};
		}

		private void CTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CheckBordersInside();
		}

		private void CTabControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			CheckBordersInside();
		}

		private void CheckBordersInside()
		{
			//if (this.SelectedItem is TabItem selectedTab)
			//{
			//	selectedTab.ApplyTemplate();

			//	var borderBottom = selectedTab.Template.FindName("xBorderBottomSelected", selectedTab) as FrameworkElement;
			//	var borderMid = selectedTab.Template.FindName("xBorderBTMid", selectedTab) as Border;
			//	var borderRight = selectedTab.Template.FindName("xBorderBTRight", selectedTab) as FrameworkElement;
			//	var borderLeft = selectedTab.Template.FindName("xBorderBTLeft", selectedTab) as FrameworkElement;

			//	borderRight.Visibility = Visibility.Visible;
			//	borderLeft.Visibility = Visibility.Visible;

			//	if (borderRight == null || borderLeft == null)
			//		return;

			//	bool bRightVisible = IsElementInside(borderRight, this);
			//	bool bLeftVisible = IsElementInside(borderLeft, this);

			//	// Nếu cả hai đều nằm trong
			//	if (bRightVisible && bLeftVisible)
			//	{
			//		borderBottom.Margin = new Thickness(-6, 0, -8, 0);
			//		borderMid.BorderThickness = new Thickness(0);
			//	}
			//	else if (bRightVisible)
			//	{
			//		borderLeft.Visibility = Visibility.Collapsed;
			//		borderBottom.Margin = new Thickness(0, 0, -8, 0);
			//		borderMid.BorderThickness = new Thickness(1, 0, 0, 0);
			//	}
			//	else if (bLeftVisible)
			//	{
			//		borderRight.Visibility = Visibility.Collapsed;
			//		borderBottom.Margin = new Thickness(-6, 0, -3, 0);
			//		borderMid.BorderThickness = new Thickness(0, 0, 1, 0);
			//	}
			//	else
			//	{
			//		borderLeft.Visibility = Visibility.Collapsed;
			//		borderRight.Visibility = Visibility.Collapsed;
			//		borderBottom.Margin = new Thickness(-6, 0, -8,0);
			//		borderMid.BorderThickness = new Thickness(1, 0, 1, 0);
			//	}
			//}
		}


		private bool IsElementInside(UIElement child, FrameworkElement container)
		{
			// Lấy bounds của phần tử con (xBorderBTRight) so với container (TabControl)
			Rect childBounds = child.TransformToAncestor(container)
									 .TransformBounds(new Rect(0, 0, child.RenderSize.Width, child.RenderSize.Height));

			// Kích thước của container
			Rect containerBounds = new Rect(0, 0, container.ActualWidth, container.ActualHeight);

			// Kiểm tra xem childBounds có nằm trong containerBounds không
			return containerBounds.Contains(childBounds.TopLeft) && containerBounds.Contains(childBounds.BottomRight);
		}
	}
}
