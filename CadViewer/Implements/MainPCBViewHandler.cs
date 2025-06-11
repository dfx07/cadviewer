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

			PCBViewerAPI.OnMouseMove(m_pHandler, (BaseAPI._INT)pt.X, (BaseAPI._INT)pt.Y);
		}

		public override void OnMouseEnter()
		{
			PCBViewNotifier.SetTitle("Mouse Enter");
			PCBViewerAPI.OnMouseEnter(m_pHandler);
		}

		public override void OnMouseDown(MouseButton btn, Point pt)
		{
			PCBViewNotifier.SetTitle("Mouse Down : " + pt.ToString());

			PCBViewerAPI.OnMouseDown(m_pHandler, (BaseAPI._INT)pt.X, (BaseAPI._INT)pt.Y, (BaseAPI._INT)((int)btn));
		}
		public override void OnMouseUp(MouseButton btn, Point pt)
		{
			PCBViewNotifier.SetTitle("Mouse Up : " + pt.ToString());

			PCBViewerAPI.OnMouseUp(m_pHandler, (BaseAPI._INT)pt.X, (BaseAPI._INT)pt.Y, (BaseAPI._INT)((int)btn));
		}
		public override void OnMouseWheel(float delta, Point pt)
		{
			PCBViewNotifier.SetTitle("Mouse wheel: " + pt);

			PCBViewerAPI.OnMouseWheel(m_pHandler, (BaseAPI._INT)pt.X, (BaseAPI._INT)pt.Y, (BaseAPI._FLOAT)((float)delta));
		}

		public override void OnMouseDragDrop(MouseDragDropState state, Point pt)
		{
			PCBViewNotifier.SetTitle("Mouse Drag + Drop : " + pt);
		}

		public override void OnViewUpdate()
		{
			PCBViewerAPI.Clear(m_pHandler);
			PCBViewerAPI.Draw(m_pHandler);
		}

		public override void OnViewChanged(int width, int height)
		{
			PCBViewerAPI.SetView(m_pHandler, width, height);
		}

		public override void OnKeyDown(Key key)
		{
			Logger.LogInfo("Keydown : " + key);
		}
		public override void OnKeyUp(Key key)
		{
			Logger.LogInfo("Keyup : " + key);
		}

		public override void OnNotifyHandle(string message, int nParam, int nWaram)
		{
			if(message == "DrawLine")
			{
				PCBViewNotifier.SetTitle("DrawLine");
			}
		}

		/*-----------------------------------------------------------------------------------*/
		public void DrawLine()
		{
			PCBViewerAPI.Clear(m_pHandler);
			PCBViewerAPI.Draw(m_pHandler);
		}
	}
}
