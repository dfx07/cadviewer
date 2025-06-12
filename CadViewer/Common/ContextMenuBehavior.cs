using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors; // Sử dụng nếu bạn cài Microsoft.Xaml.Behaviors.Wpf

namespace CadViewer.Common
{
	public class ContextMenuBehavior : Behavior<FrameworkElement>
	{
		public static readonly DependencyProperty IsContextMenuVisibleProperty =
			DependencyProperty.Register(
				nameof(IsContextMenuVisible),
				typeof(bool),
				typeof(ContextMenuBehavior),
				new PropertyMetadata(false, OnIsContextMenuVisibleChanged));

		public bool IsContextMenuVisible
		{
			get => (bool)GetValue(IsContextMenuVisibleProperty);
			set => SetValue(IsContextMenuVisibleProperty, value);
		}

		private static void OnIsContextMenuVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var behavior = (ContextMenuBehavior)d;
			var target = behavior.AssociatedObject;
			if (target == null) return;

			var cm = target.ContextMenu;
			if (cm == null) return;

			if ((bool)e.NewValue)
			{
				// Đặt PlacementTarget để hiển thị tại đúng vị trí
				cm.PlacementTarget = target;

				// Mở ContextMenu
				cm.IsOpen = true;

				// Gắn xử lý khi đóng lại (một lần duy nhất)
				cm.Closed -= behavior.ContextMenu_Closed;
				cm.Closed += behavior.ContextMenu_Closed;
			}
		}

		private void ContextMenu_Closed(object sender, RoutedEventArgs e)
		{
			// Khi menu đóng lại, gán lại IsContextMenuVisible = false
			this.IsContextMenuVisible = false;

			// Bỏ đăng ký sự kiện
			if (sender is ContextMenu cm)
			{
				cm.Closed -= ContextMenu_Closed;
			}
		}
	}
}