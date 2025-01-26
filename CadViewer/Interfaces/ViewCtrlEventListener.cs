using CadViewer.Common;
using CadViewer.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CadViewer.Interfaces
{
	public interface IWinformViewCtrlEventListener
	{
		void WinformViewCtrl_OnCreated(object sender, IntPtr handle);
		void WinformViewCtrl_OnViewUpdate(object sender);
		void WinformViewCtrl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e);
		void WinformViewCtrl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e);
		void WinformViewCtrl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e);
		void WinformViewCtrl_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e);
		void WinformViewCtrl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e);
		void WinformViewCtrl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e);
		void WinformViewCtrl_SizeChanged(object sender, System.Windows.Size newSize);
	}
}
