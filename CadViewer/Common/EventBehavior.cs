using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CadViewer.Common
{
	public class XMouseEventArgs
	{
		public XMouseEventArgs(Point _pt)
		{
			Pt = _pt;
		}
		public XMouseEventArgs()
		{
			Pt = new Point(0.0, 0.0);
		}

		public Point Pt { get; set; }
	}

	public class XMouseDragDropEventArgs : XMouseEventArgs
	{
		public XMouseDragDropEventArgs(Point _pt, MouseButton _btn, MouseDragDropState _state)
			: base(_pt)
		{
			State = _state;
			Button = _btn;
			Pt = _pt;
		}
		public MouseButton Button { get; set; }
		public MouseDragDropState State { get; set; }
	}

	public class XMouseWheelEventArgs : XMouseEventArgs
	{
		public XMouseWheelEventArgs(Point _pt, int _delta)
			: base(_pt)
		{
			Delta = _delta;
			Pt = _pt;
		}

		public int Delta { get; set; }
	}

	public class XMouseButtonEventArgs : XMouseEventArgs
	{
		public XMouseButtonEventArgs(Point _pt, MouseButton _btn, MouseButtonState _btnState)
			: base(_pt)
		{
			ButtonState = _btnState;
			Button = _btn;
			Pt = _pt;
		}
		public MouseButton Button { get; set; }
		public MouseButtonState ButtonState { get; set; }
	}

	public static class EventConverter
	{
		public static MouseButton MouseWpf2WF(System.Windows.Forms.MouseButtons btn)
		{
			switch (btn)
			{
				case System.Windows.Forms.MouseButtons.Left:
					return MouseButton.Left;
				case System.Windows.Forms.MouseButtons.Right:
					return MouseButton.Right;
				case System.Windows.Forms.MouseButtons.Middle:
					return MouseButton.Middle;
			}

			return MouseButton.Right;
		}

		public static Key KeyWF2Wpf(Keys keys)
		{
			switch (keys)
			{
				case Keys.A: return Key.A;
				case Keys.B: return Key.B;
				case Keys.C: return Key.C;
				case Keys.D: return Key.D;
				case Keys.E: return Key.E;
				case Keys.F: return Key.F;
				case Keys.G: return Key.G;
				case Keys.H: return Key.H;
				case Keys.I: return Key.I;
				case Keys.J: return Key.J;
				case Keys.K: return Key.K;
				case Keys.L: return Key.L;
				case Keys.M: return Key.M;
				case Keys.N: return Key.N;
				case Keys.O: return Key.O;
				case Keys.P: return Key.P;
				case Keys.Q: return Key.Q;
				case Keys.R: return Key.R;
				case Keys.S: return Key.S;
				case Keys.T: return Key.T;
				case Keys.U: return Key.U;
				case Keys.V: return Key.V;
				case Keys.W: return Key.W;
				case Keys.X: return Key.X;
				case Keys.Y: return Key.Y;
				case Keys.Z: return Key.Z;
				case Keys.Enter: return Key.Enter;
				case Keys.Space: return Key.Space;
				case Keys.Escape: return Key.Escape;
				case Keys.Tab: return Key.Tab;
				case Keys.Back: return Key.Back;
				case Keys.Shift: return Key.LeftShift;
				case Keys.Control: return Key.LeftCtrl;  // You may need to choose LeftCtrl or RightCtrl
				case Keys.Alt: return Key.LeftAlt;  // You may need to choose LeftAlt or RightAlt
				case Keys.Delete: return Key.Delete;
				case Keys.Left: return Key.Left;
				case Keys.Right: return Key.Right;
				case Keys.Up: return Key.Up;
				case Keys.Down: return Key.Down;

				// Handling some special keys
				case Keys.F1: return Key.F1;
				case Keys.F2: return Key.F2;
				case Keys.F3: return Key.F3;
				case Keys.F4: return Key.F4;
				case Keys.F5: return Key.F5;
				case Keys.F6: return Key.F6;
				case Keys.F7: return Key.F7;
				case Keys.F8: return Key.F8;
				case Keys.F9: return Key.F9;
				case Keys.F10: return Key.F10;
				case Keys.F11: return Key.F11;
				case Keys.F12: return Key.F12;

				case Keys.Home: return Key.Home;
				case Keys.End: return Key.End;
				case Keys.PageUp: return Key.PageUp;
				case Keys.PageDown: return Key.PageDown;
				case Keys.Insert: return Key.Insert;

				// Other keys like Windows key, Menu key, etc. might need custom handling
				case Keys.LWin: return Key.LWin;  // Left Windows key
				case Keys.RWin: return Key.RWin;  // Right Windows key
				case Keys.Menu: return Key.Apps;  // Menu key (usually the "right-click" key)

				default:
					throw new ArgumentOutOfRangeException($"Unsupported key: {keys}");
			}
		}
	}

	public class XKeyEventArgs
	{
		public XKeyEventArgs(Key _key, KeyStates _keyState)
		{
			KeyStates = _keyState;
			Key = _key;
		}
		public Key Key { get; set; }
		public KeyStates KeyStates { get; set; }
	}

	public class XHandleCreatedArgs
	{
		public XHandleCreatedArgs(IntPtr handle, Size size)
		{
			Size = size;
			Handle = handle;
		}

		public IntPtr Handle { get; set; }
		public Size Size { get; set; }
	}
}
