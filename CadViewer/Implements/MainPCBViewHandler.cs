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

		}

		public override void OnMouseEnter()
		{

		}

		public override void OnMouseDown(MouseButton btn, Point pt)
		{
			Logger.LogInfo("Button down : " + btn.ToString() + " *** " + pt.ToString());

			PCBViewNotifier.SetTitle("Main PCB");
		}
		public override void OnMouseUp(MouseButton btn, Point pt)
		{

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
