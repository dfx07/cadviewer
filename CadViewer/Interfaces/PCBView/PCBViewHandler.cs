using CadViewer.API;
using CadViewer.Common;
using CadViewer.Components;
using CadViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CadViewer.Interfaces
{
	public abstract class PCBViewHandler : NotificationHandler
	{
		~PCBViewHandler()
		{
			PCBViewerAPI.DestroyPCBView(m_pHandler);
		}

		public void OnCreateContext(IntPtr pHandle)
		{
			m_pHandler = PCBViewerAPI.CreatePCBView(pHandle);

			PCBViewerAPI.SetCallbackFunctionNotifyUI(m_pHandler, m_pCallbackUI);

			CreateContext();
		}

		public virtual void OnViewUpdate()
		{
			//TODO: Implement
		}

		public virtual void CreateContext()
		{
			//TODO: Implement
		}

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
		public virtual void OnMouseWheel(float delta, Point pt)
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
		public virtual void OnViewChanged(int width, int height)
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
