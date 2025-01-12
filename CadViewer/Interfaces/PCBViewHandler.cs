using CadViewer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CadViewer.Interfaces
{
	public abstract class PCBViewHandler
	{
		public virtual void OnMouseMove(Point pt) { }
		public virtual void OnMouseEnter() { }

		public virtual void OnMouseDown(MouseButton btn, Point pt) { }
		public virtual void OnMouseUp(MouseButton btn, Point pt) { }
		public virtual void OnMouseDragDrop(MouseDragDropState state, Point pt) { }

		public virtual void OnKeyDown(Key key) { }
		public virtual void OnKeyUp(Key key) { }

		public void SetPCBViewNotifier(PCBViewNotify pCBViewNofifier)
		{
			PCBViewNotifier = pCBViewNofifier;
		}

		public PCBViewNotify GetPCBViewNotifier()
		{
			return PCBViewNotifier;
		}

		protected PCBViewNotify PCBViewNotifier = null;
	}
}
