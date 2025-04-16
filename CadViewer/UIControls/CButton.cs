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

namespace CadViewer.UIControls
{
	public class CButton : Button
	{
		static CButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CButton),
				new FrameworkPropertyMetadata(typeof(CButton)));
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
