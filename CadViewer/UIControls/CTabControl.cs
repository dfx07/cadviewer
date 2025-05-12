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
	public class CTabItem : TabItem
	{
		static CTabItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CTabItem),
				new FrameworkPropertyMetadata(typeof(CTabItem)));
		}
	}

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
			if (this.SelectedItem is TabItem selectedTab)
			{
				selectedTab.ApplyTemplate();

				var borderSel = selectedTab.Template.FindName("xBorderBottomSelected", selectedTab) as FrameworkElement;
				var borderM = selectedTab.Template.FindName("xBorderBTMid", selectedTab) as Border;
				var borderR = selectedTab.Template.FindName("xBorderBTRight", selectedTab) as FrameworkElement;
				var borderL = selectedTab.Template.FindName("xBorderBTLeft", selectedTab) as FrameworkElement;
				var borderRight = selectedTab.Template.FindName("xBorderBTCheck_Right", selectedTab) as FrameworkElement;
				var borderLeft = selectedTab.Template.FindName("xBorderBTCheck_Left", selectedTab) as FrameworkElement;

				if (borderRight == null || borderLeft == null)
					return;

				bool bRightVisible = IsElementInside(borderRight, this);
				bool bLeftVisible = IsElementInside(borderLeft, this);

				bool bOldLeftVib = borderL.Visibility == Visibility.Visible;
				bool bOldRightVib = borderR.Visibility == Visibility.Visible;

				if (bLeftVisible && bRightVisible)
				{
					borderSel.Margin = new Thickness(-6, 0, -8, -1);
					borderM.BorderThickness = new Thickness(0);
					borderM.Margin = new Thickness(-1, 0, -1, -1);

					if (!bOldLeftVib)
						borderL.Visibility = Visibility.Visible;
					if (!bOldRightVib)
						borderR.Visibility = Visibility.Visible;
				}
				else if(bLeftVisible)
				{
					if (!bOldLeftVib)
						borderL.Visibility = Visibility.Visible;

					if(bOldRightVib)
						borderR.Visibility = Visibility.Collapsed;

					borderSel.Margin = new Thickness(-6, 0, -2, -1);
					borderM.BorderThickness = new Thickness(0, 0, 1, 0);
					borderM.Margin = new Thickness(-1, 0, 0, -1);
				}
				else if (bRightVisible)
				{
					if (!bOldRightVib)
						borderR.Visibility = Visibility.Visible;

					if(bOldLeftVib)
						borderL.Visibility = Visibility.Collapsed;

					borderSel.Margin = new Thickness(0, 0, -8, -1);
					borderM.BorderThickness = new Thickness(1, 0, 0, 0);
					borderM.Margin = new Thickness(0, 0, -1, -2);
				}
				else
				{
					borderSel.Margin = new Thickness(0, 0, -2, -1);
					borderM.Margin = new Thickness(0, 0, 0, 0);
					borderM.BorderThickness = new Thickness(1, 0, 1, 0);

					if (bOldLeftVib)
						borderL.Visibility = Visibility.Collapsed;
					if (bOldRightVib)
						borderR.Visibility = Visibility.Collapsed;
				}
			}
		}


		private bool IsElementInside(UIElement child, FrameworkElement container)
		{
			if (!child.IsVisible || child.RenderSize.Width == 0 || child.RenderSize.Height == 0)
				return false;

			Rect childBounds = child.TransformToAncestor(container)
						.TransformBounds(new Rect(0, 0, child.RenderSize.Width, child.RenderSize.Height));
			Rect containerBounds = new Rect(0, 0, container.ActualWidth, container.ActualHeight);

			return containerBounds.Contains(childBounds.TopLeft) && containerBounds.Contains(childBounds.BottomRight);
		}
	}
}
