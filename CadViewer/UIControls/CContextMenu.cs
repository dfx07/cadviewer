using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using CadViewer.Animations;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Threading;
using CadViewer.Interfaces;
using System.Windows.Data;

namespace CadViewer.UIControls
{
	public class CContextMenu : ContextMenu
	{
		static CContextMenu()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CContextMenu), new FrameworkPropertyMetadata(typeof(CContextMenu)));
		}
	}
}
