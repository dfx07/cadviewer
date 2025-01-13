using CadViewer.Common;
using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CadViewer.Implements
{
	public class MainPCBViewHandler : PCBViewHandler
	{
		public override void OnMouseMove(Point pt)
		{
			Logger.LogInfo("Mouse Move :" + pt.ToString());
			PCBViewNotifier.SetTitle("Mouse Move :" + pt.ToString());
		}

		public override void OnMouseEnter()
		{
			Logger.LogInfo("Mouse Enter");
			PCBViewNotifier.SetTitle("Mouse Enter");
		}

		public override void OnMouseDown(MouseButton btn, Point pt)
		{
			Logger.LogInfo("Button Down : " + btn.ToString() + " *** " + pt.ToString());

			PCBViewNotifier.SetTitle("Mouse Down : " + pt.ToString());
		}
		public override void OnMouseUp(MouseButton btn, Point pt)
		{
			Logger.LogInfo("Button Up : " + btn.ToString() + " *** " + pt.ToString());

			PCBViewNotifier.SetTitle("Mouse Up : " + pt.ToString());
		}

		public override void OnMouseDragDrop(MouseDragDropState state, Point pt)
		{

		}

		public override void OnKeyDown(Key key)
		{

		}
		public override void OnKeyUp(Key key)
		{

		}
	}
}
