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
		public virtual void OnMouseMove(Point pt)
		{
			//TODO: Implement
		}
		public virtual void OnMouseEnter()
		{
			//TODO: Implement
		}
		public virtual void OnMouseDown(MouseButton btn, Point pt)
		{
			//TODO: Implement
		}
		public virtual void OnMouseUp(MouseButton btn, Point pt)
		{
			//TODO: Implement
		}
		public virtual void OnMouseDragDrop(MouseDragDropState state, Point pt)
		{
			//TODO: Implement
		}
		public virtual void OnKeyDown(Key key)
		{
			//TODO: Implement
		}
		public virtual void OnKeyUp(Key key)
		{
			//TODO: Implement
		}

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
