using CadViewer.Common;
using CadViewer.Components;
using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace CadViewer.View
{
	/// <summary>
	/// Interaction logic for OpenGLView.xaml
	/// </summary>
	public partial class OpenGLView : UserControl, IWinformViewCtrlEventListener
	{
		private bool _isDragging = false;


		public OpenGLView()
		{
			InitializeComponent();
		}

		public bool IsUseDragDropFun()
		{
			if (OpenGLMouseDragDropCommand != null &&
				OpenGLMouseDragDropCommand.CanExecute(null))
				return true;

			return false;
		}

		public static readonly DependencyProperty OpenGLContentProperty =
			DependencyProperty.Register(nameof(OpenGLContent), typeof(OpenGLHost), typeof(OpenGLView), new PropertyMetadata(null));

		public OpenGLHost OpenGLContent
		{
			get => (OpenGLHost)GetValue(OpenGLContentProperty);
			set => SetValue(OpenGLContentProperty, value);
		}


		public static readonly DependencyProperty OpenGLViewPanelProperty =
		DependencyProperty.Register(nameof(OpenGLViewPanel), typeof(ViewPanel), typeof(OpenGLView), new PropertyMetadata(null));

		public ViewPanel OpenGLViewPanel
		{
			get => (ViewPanel)GetValue(OpenGLViewPanelProperty);
			set => SetValue(OpenGLViewPanelProperty, value);
		}

		#region [Mouse Events]
		/// <summary>
		/// /////////////////////////////////////////////////////////////////////////////
		/// </summary>
		public static readonly DependencyProperty OpenGLMouseMoveCommandProperty =
		DependencyProperty.Register(nameof(OpenGLMouseMoveCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLMouseMoveCommand
		{
			get { return (ICommand)GetValue(OpenGLMouseMoveCommandProperty); }
			set { SetValue(OpenGLMouseMoveCommandProperty, value); }
		}

		public static readonly DependencyProperty OpenGLMouseEnterCommandProperty =
		DependencyProperty.Register(nameof(OpenGLMouseEnterCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLMouseEnterCommand
		{
			get { return (ICommand)GetValue(OpenGLMouseEnterCommandProperty); }
			set { SetValue(OpenGLMouseEnterCommandProperty, value); }
		}

		public static readonly DependencyProperty OpenGLMouseDownCommandProperty =
		DependencyProperty.Register(nameof(OpenGLMouseDownCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLMouseDownCommand
		{
			get { return (ICommand)GetValue(OpenGLMouseDownCommandProperty); }
			set { SetValue(OpenGLMouseDownCommandProperty, value); }
		}

		public static readonly DependencyProperty OpenGLMouseUpCommandProperty =
		DependencyProperty.Register(nameof(OpenGLMouseUpCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLMouseUpCommand
		{
			get { return (ICommand)GetValue(OpenGLMouseUpCommandProperty); }
			set { SetValue(OpenGLMouseUpCommandProperty, value); }
		}

		public static readonly DependencyProperty OpenGLMouseDragDropCommandProperty =
		DependencyProperty.Register(nameof(OpenGLMouseDragDropCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLMouseDragDropCommand
		{
			get { return (ICommand)GetValue(OpenGLMouseDragDropCommandProperty); }
			set { SetValue(OpenGLMouseDragDropCommandProperty, value); }
		}

		public static readonly DependencyProperty OpenGLMouseWheelCommandProperty =
		DependencyProperty.Register(nameof(OpenGLMouseWheelCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLMouseWheelCommand
		{
			get { return (ICommand)GetValue(OpenGLMouseWheelCommandProperty); }
			set { SetValue(OpenGLMouseWheelCommandProperty, value); }
		}

		//------------------------------------------------------------------------------/
		#endregion

		public static readonly DependencyProperty OpenGLKeyDownCommandProperty =
		DependencyProperty.Register(nameof(OpenGLKeyDownCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLKeyDownCommand
		{
			get { return (ICommand)GetValue(OpenGLKeyDownCommandProperty); }
			set { SetValue(OpenGLKeyDownCommandProperty, value); }
		}

		public static readonly DependencyProperty OpenGLKeyUpCommandProperty =
		DependencyProperty.Register(nameof(OpenGLKeyUpCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLKeyUpCommand
		{
			get { return (ICommand)GetValue(OpenGLKeyUpCommandProperty); }
			set { SetValue(OpenGLKeyUpCommandProperty, value); }
		}

		#region [Other events]
		/// /////////////////////////////////////////////////////////////////////////////

		public static readonly DependencyProperty OpenGLSizeChangedCommandProperty =
		DependencyProperty.Register(nameof(OpenGLSizeChangedCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLSizeChangedCommand
		{
			get => (ICommand)GetValue(OpenGLSizeChangedCommandProperty);
			set => SetValue(OpenGLSizeChangedCommandProperty, value);
		}

		public static readonly DependencyProperty OpenGLViewCreatedCommandProperty =
		DependencyProperty.Register(nameof(OpenGLViewCreatedCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLViewCreatedCommand
		{
			get { return (ICommand)GetValue(OpenGLViewCreatedCommandProperty); }
			set { SetValue(OpenGLViewCreatedCommandProperty, value); }
		}

		public static readonly DependencyProperty OpenGLViewUpdateCommandProperty =
		DependencyProperty.Register(nameof(OpenGLViewUpdateCommand), typeof(ICommand), typeof(OpenGLView), new PropertyMetadata(null));

		public ICommand OpenGLViewUpdateCommand
		{
			get { return (ICommand)GetValue(OpenGLViewUpdateCommandProperty); }
			set { SetValue(OpenGLViewUpdateCommandProperty, value); }
		}

		//------------------------------------------------------------------------------/
		#endregion


		public void WinformViewCtrl_OnCreated(object sender, IntPtr handle)
		{
			OpenGLViewCreatedCommand.Execute(handle);
		}

		public void WinformViewCtrl_OnViewUpdate(object sender)
		{
			OpenGLViewUpdateCommand.Execute(null);
		}

		public void WinformViewCtrl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (IsUseDragDropFun())
			{
				if (_isDragging)
				{
					var evtArgs = new XMouseDragDropEventArgs(new Point(e.X, e.Y), MouseDragDropState.Move);
					OpenGLMouseDragDropCommand.Execute(evtArgs);
				}
			}

			var evt = new XMouseEventArgs(new Point(e.X, e.Y));

			OpenGLMouseMoveCommand.Execute(evt);
		}

		public void WinformViewCtrl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (IsUseDragDropFun())
			{
				_isDragging = true;
				var evtArgs = new XMouseDragDropEventArgs(new Point(e.X, e.Y), MouseDragDropState.Drag);

				OpenGLMouseDragDropCommand.Execute(evtArgs);
			}

			var evt = new XMouseButtonEventArgs(new Point(e.X, e.Y),
				EventConverter.MouseWpf2WF(e.Button), MouseButtonState.Pressed);

			OpenGLMouseDownCommand.Execute(evt);
		}

		public void WinformViewCtrl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (IsUseDragDropFun())
			{
				if (_isDragging)
				{
					_isDragging = false;
					var evtArgs = new XMouseDragDropEventArgs(new Point(e.X, e.Y), MouseDragDropState.Drop);
					OpenGLMouseDragDropCommand.Execute(evtArgs);
					Mouse.Capture(null);
				}
			}

			var evt = new XMouseButtonEventArgs(new Point(e.X, e.Y),
				EventConverter.MouseWpf2WF(e.Button), MouseButtonState.Pressed);

			OpenGLMouseUpCommand.Execute(evt);
		}

		public void WinformViewCtrl_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			var evt = new XMouseWheelEventArgs(new Point(e.X, e.Y), e.Delta);

			OpenGLMouseWheelCommand.Execute(evt);
		}

		public void WinformViewCtrl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			try
			{
				Key key = EventConverter.KeyWF2Wpf(e.KeyCode);

				var evt = new XKeyEventArgs(key, KeyStates.Down);

				OpenGLKeyDownCommand.Execute(evt);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
		}

		public void WinformViewCtrl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			try
			{
				Key key = EventConverter.KeyWF2Wpf(e.KeyCode);

				var evt = new XKeyEventArgs(key, KeyStates.None);

				OpenGLKeyUpCommand.Execute(evt);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				Console.WriteLine(ex.Message);
				return;
			}
		}

		public void WinformViewCtrl_SizeChanged(object sender, System.Windows.Size newSize)
		{
			if (OpenGLSizeChangedCommand != null)
			{
				OpenGLSizeChangedCommand.Execute(newSize);
			}
		}


		private void xWindowsFormsHost_Loaded(object sender, RoutedEventArgs e)
		{
			if(OpenGLViewPanel != null)
			{
				OpenGLViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
				OpenGLViewPanel.ViewControl = this;
				xWindowsFormsHost.Child  = OpenGLViewPanel;
			}
		}

		/* Keyboard events*/

	}
}
