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

namespace CadViewer.UIControls
{
	public enum CProgressBarStyle
	{
		Line,
		Circle
	}

	public class CProgressBar : ProgressBar
	{
		static CProgressBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CProgressBar),
				new FrameworkPropertyMetadata(typeof(CProgressBar)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}
	}
}
