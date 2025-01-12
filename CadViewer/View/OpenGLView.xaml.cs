using CadViewer.Common;
using CadViewer.Components;
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

namespace CadViewer.View
{
	/// <summary>
	/// Interaction logic for OpenGLView.xaml
	/// </summary>
	public partial class OpenGLView : UserControl
	{
		private bool _isDragging = false;

		public OpenGLView()
		{
			InitializeComponent();
		}

		private void OpenGLView_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if(IsUseDragDropFun())
			{
				_isDragging = true;
				MouseDragDropEventArgs dragDropEventArgs = new MouseDragDropEventArgs(e, MouseDragDropState.Drag);
				OpenGLMouseDragDropCommand.Execute(dragDropEventArgs);
				Mouse.Capture(this);
			}
		}

		private void OpenGLView_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (IsUseDragDropFun())
			{
				if (_isDragging)
				{
					_isDragging = false;
					MouseDragDropEventArgs dragDropEventArgs = new MouseDragDropEventArgs(e, MouseDragDropState.Drop);
					OpenGLMouseDragDropCommand.Execute(dragDropEventArgs);
				}

				Mouse.Capture(null);
			}
		}

		private void OpenGLView_MouseMove(object sender, MouseEventArgs e)
		{
			if (IsUseDragDropFun())
			{
				if (_isDragging)
				{
					MouseDragDropEventArgs dragDropEventArgs = new MouseDragDropEventArgs(e, MouseDragDropState.Move);
					OpenGLMouseDragDropCommand.Execute(dragDropEventArgs);
				}
			}
		}

		public bool IsUseDragDropFun()
		{
			if (OpenGLMouseDragDropCommand != null && OpenGLMouseDragDropCommand.CanExecute(null))
			{
				return true;
			}

			return false;
		}

		public void SendEventDragDrop(MouseDragDropEventArgs dragDropEventArgs)
		{
			if (OpenGLMouseDragDropCommand != null && OpenGLMouseDragDropCommand.CanExecute(null))
			{
				
			}
		}

		public static readonly DependencyProperty OpenGLContentProperty =
			DependencyProperty.Register(nameof(OpenGLContent), typeof(OpenGLHost), typeof(OpenGLView), new PropertyMetadata(null));

		public OpenGLHost OpenGLContent
		{
			get => (OpenGLHost)GetValue(OpenGLContentProperty);
			set => SetValue(OpenGLContentProperty, value);
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

		/* Keyboard events*/

	}
}
