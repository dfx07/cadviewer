using CadViewer.Interfaces;
using CadViewer.UIControls;
using CadViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CadViewer.Services
{
	public interface IOverlayService
	{
		void ShowOverlayTransparent(object vm, Brush backgroundColor = null);
		void ShowOverlay(object vm, Brush backgroundColor = null);
		void HideOverlay();
	}

	public class OverlayService : IOverlayService
	{
		private OverlayWindow _overlayWindow;
		private OverlayWindowTransparent _overlayWindowTrans;

		private readonly Window _owner;

		public OverlayService(Window owner)
		{
			_owner = owner;
		}

		private OverlayWindow GetOverlayWindow(bool bNew)
		{
			if (_overlayWindow == null || bNew)
				_overlayWindow = new OverlayWindow(_owner);
			return _overlayWindow;
		}
		private OverlayWindow GetOverlayWindowTrans(bool bNew)
		{
			if (_overlayWindowTrans == null || bNew)
				_overlayWindowTrans = new OverlayWindowTransparent(_owner);
			return _overlayWindowTrans;
		}

		public void ShowOverlayTransparent(object content, Brush backgroundColor = null)
		{
			var overlayWindow = GetOverlayWindowTrans(false);

			SetOverlayContent(overlayWindow, content, backgroundColor);
		}

		public void ShowOverlay(object content, Brush backgroundColor = null)
		{
			var overlayWindow = GetOverlayWindow(false);

			SetOverlayContent(overlayWindow, content, backgroundColor);
		}

		private void SetOverlayContent(OverlayWindow overlay, object content, Brush backgroundColor)
		{
			var contentControl = new ContentControl { Content = content };

			if (backgroundColor != null)
				overlay.SetOverlayBackground(backgroundColor);

			overlay.SetOverlayContent(contentControl);

			overlay.Show();
		}

		public void HideOverlay()
		{
			if (_overlayWindow != null)
				_overlayWindow?.Hide();

			if (_overlayWindowTrans != null)
				_overlayWindowTrans?.Hide();
		}
	}
}
