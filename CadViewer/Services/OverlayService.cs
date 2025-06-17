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
		void ShowOverlay(object content, OverlayWindowType type, Brush backgroundColor = null);

		void UpdateOverlay();
		void HideOverlay();
	}

	public class OverlayService : IOverlayService
	{
		private OverlayWindow _overlayWindow = null;
		private OverlayWindow _overlayWindowTrans = null;
		private OverlayWindow _overlayWindowBackdrop = null;

		private readonly Window _owner;

		public OverlayService(Window owner)
		{
			_owner = owner;
		}

		private OverlayWindow GetOverlayWindow(OverlayWindowType type, bool bFoceNew = false)
		{
			if(type == OverlayWindowType.Backdrop)
			{
				if (_overlayWindowBackdrop == null || bFoceNew)
					_overlayWindowBackdrop = new OverlayWindow(_owner, type);
				return _overlayWindowBackdrop;
			}
			else if(type == OverlayWindowType.Transparent)
			{
				if (_overlayWindowTrans == null || bFoceNew)
					_overlayWindowTrans = new OverlayWindow(_owner, type);
				return _overlayWindowTrans;
			}
			else
			{
				if (_overlayWindow == null || bFoceNew)
					_overlayWindow = new OverlayWindow(_owner, type);
			}
			return _overlayWindow;
		}

		public void ShowOverlay(object content, OverlayWindowType type, Brush backgroundColor = null)
		{
			var overlayWindow = GetOverlayWindow(type);

			if (backgroundColor != null)
				overlayWindow.SetOverlayBackground(backgroundColor);

			overlayWindow.SetOverlayContent(content);

			overlayWindow.DoShow();
		}

		public void UpdateOverlay()
		{
			if(_overlayWindowBackdrop != null)
			{
				_overlayWindowBackdrop.UpdateBackdropImage();
			}
		}

		public void HideOverlay()
		{
			if (_overlayWindow != null)
				_overlayWindow?.DoHide();

			if (_overlayWindowTrans != null)
				_overlayWindowTrans?.DoHide();

			if (_overlayWindowBackdrop != null)
				_overlayWindowBackdrop?.DoHide();
		}
	}
}
