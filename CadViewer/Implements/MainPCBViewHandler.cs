using CadViewer.API;
using CadViewer.Common;
using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

using _INT = System.Int32;
using _BOOL = System.Int32;
using _DOUBLE = System.Double;
using _FLOAT =System.Single;

namespace CadViewer.Implements
{
	public class MainPCBViewHandler : PCBViewHandler
	{
		public MainPCBViewHandler()
		{

		}
		public override void CreateContext()
		{
			ContextConfig ctxConfig = new ContextConfig
			{
				m_bUseContextExt = BaseAPI.TRUE,
				m_nAntialiasingLevel = 8,
			};

			if (PCBViewerAPI.CreateContext(m_pHandler, ctxConfig) != BaseAPI.TRUE)
			{
				MessageBox.Show("Create OpenGL context FAIL");
			}
		}

		public override void OnMouseMove(Point pt)
		{
			PCBViewNotifier.SetTitle("Mouse Move :" + pt.ToString());

			PCBViewerAPI.OnMouseMove(m_pHandler, (_INT)pt.X, (_INT)pt.Y);
		}

		public override void OnMouseEnter()
		{
			PCBViewNotifier.SetTitle("Mouse Enter");
			PCBViewerAPI.OnMouseEnter(m_pHandler);
		}

		public override void OnMouseDown(MouseButton btn, Point pt)
		{
			PCBViewNotifier.SetTitle("Mouse Down : " + pt.ToString());

			PCBViewerAPI.OnMouseDown(m_pHandler, (_INT)pt.X, (_INT)pt.Y, (_INT)btn);
		}
		public override void OnMouseUp(MouseButton btn, Point pt)
		{
			PCBViewNotifier.SetTitle("Mouse Up : " + pt.ToString());

			PCBViewerAPI.OnMouseUp(m_pHandler, (_INT)pt.X, (_INT)pt.Y, (_INT)btn);
		}
		public override void OnMouseWheel(float delta, Point pt)
		{
			PCBViewNotifier.SetTitle("Mouse wheel: " + pt);

			PCBViewerAPI.OnMouseWheel(m_pHandler, (_INT)pt.X, (_INT)pt.Y, (_FLOAT)(delta));
		}

		public override void OnMouseDragDrop(MouseDragDropState state, MouseButton btn, Point pt)
		{
			PCBViewNotifier.SetTitle("Mouse Drag + Drop : " + pt);
			PCBViewerAPI.OnMouseDragDrop(m_pHandler, (_INT)pt.X, (_INT)pt.Y, (_INT)btn, (_INT)state);
		}

		public override void OnViewUpdate()
		{
			PCBViewerAPI.OnPaint(m_pHandler);
		}

		public override void OnViewChanged(int width, int height)
		{
			PCBViewerAPI.OnSizeChanged(m_pHandler, width, height);
		}

		public override void OnKeyDown(Key key)
		{
			Logger.LogInfo("Keydown : " + key);

			PCBViewerAPI.OnKeyDown(m_pHandler, (_INT)key, 0); // nFlags is set to 0 for simplicity
		}
		public override void OnKeyUp(Key key)
		{
			Logger.LogInfo("Keyup : " + key);

			PCBViewerAPI.OnKeyUp(m_pHandler, (_INT)key, 0); // nFlags is set to 0 for simplicity
		}

		public override void OnNotifyHandle(string message, int nParam, int nWaram)
		{
			if(message == "DrawLine")
			{
				PCBViewNotifier.SetTitle("DrawLine");
			}
			else if(message == "GetCtrlKeyPressed")
			{
				PCBViewNotifier.SetTitle("DrawText");
			}
		}

		/*-----------------------------------------------------------------------------------*/
	}
}
