﻿using CadViewer.Common;
using CadViewer.Interfaces;
using CadViewer.Views;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;

namespace CadViewer.Components
{
	public class ViewPanel : Control
	{
		private IntPtr _deviceContext = IntPtr.Zero;
		private IntPtr _renderContext = IntPtr.Zero;

		public IWinformViewCtrlEventListener ViewControl { get; set; } = null;

		public ViewPanel()
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint |
						  ControlStyles.Opaque |
						  ControlStyles.UserPaint, true);

			this.SetStyle(ControlStyles.Selectable, true);
			this.TabStop = true;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_OnCreated(this, this.Handle);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_OnViewUpdate(this);
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_MouseMove(this, e);
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_MouseDown(this, e);
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_MouseUp(this, e);
		}

		protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_MouseWheel(this, e);
		}

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_KeyDown(this, e);
		}

		protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_KeyUp(this, e);
		}
		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged(e);

			if (ViewControl is null)
				return;

			ViewControl.WinformViewCtrl_SizeChanged(this, new System.Windows.Size(ClientSize.Width, ClientSize.Height));
		}
	}
}
