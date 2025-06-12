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

using _INT  = System.Int32;
using _BOOL = System.Int32;
using _DOUBLE = System.Double;

namespace CadViewer.Interfaces
{
	public abstract class PCBViewHandler : NotificationHandler
	{
		~PCBViewHandler()
		{
			PCBViewerAPI.DestroyPCBView(m_pHandler);
		}

		public virtual bool OnCreatePCBLogic(IntPtr pHandle, Size size)
		{
			m_pHandler = PCBViewerAPI.CreatePCBView(pHandle, (_INT)size.Width, (_INT)size.Height);

			PCBViewerAPI.SetCallbackFunctionNotifyUI(m_pHandler, m_pCallbackUI);

			return true;
		}

		protected bool CanExecuteEvents(EnumPCBViewEvent e)
		{
			if (_excludeEvents == 0)
				return true;

			if ((_excludeEvents & (Int64)e) > 0L)
				return false;

			return true;
		}

		protected void SetDisableEvents(Int64 events)
		{
			_excludeEvents &= events;
		}

		protected void SetEnableEvents(Int64 events)
		{
			_excludeEvents |= ~events;
		}

		// Implement
		public virtual void OnMouseMove(XMouseEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_MOVE))
				return;

			// TODO : Implement
		}

		public virtual void OnMouseEnter(XMouseEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_ENTER))
				return;

			// TODO : Implement
		}

		public virtual void OnMouseDragDrop(XMouseDragDropEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_DRAG))
				return;

			// TODO : Implement
		}

		public virtual void OnMouseDown(XMouseButtonEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_DOWN))
				return;

			// TODO : Implement
		}

		public virtual void OnMouseUp(XMouseButtonEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_UP))
				return;

			// TODO : Implement
		}

		public virtual void OnMouseWheel(XMouseWheelEventArgs e)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.MOUSE_WHEEL))
				return;

			// TODO : Implement
		}

		/*Keyboard events*/
		public virtual void OnKeyDown(XKeyEventArgs k)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.KEY_DOWN))
				return;

			// TODO : Implement
		}
		public virtual void OnKeyUp(XKeyEventArgs k)
		{
			if (!CanExecuteEvents(EnumPCBViewEvent.KEY_UP))
				return;

			// TODO : Implement
		}

		public virtual void OnViewSizeChanged(Size newSize)
		{
			// TODO : Implement
		}

		public virtual void OnViewCreated(XHandleCreatedArgs createdArgs)
		{
			// TODO : Implement
		}

		public virtual void OnViewUpdate()
		{
			// TODO : Implement
		}

		public virtual void SetPCBViewHandleListener(IPCBViewHandlerListener pCBViewHandlerListener)
		{
			PCBViewUIHandler = pCBViewHandlerListener;
		}

		public IPCBViewHandlerListener GetPCBViewHandleListener()
		{
			return PCBViewUIHandler;
		}

		protected Int64 _excludeEvents = 0;
		protected IPCBViewHandlerListener PCBViewUIHandler = null;
	}
}
