using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;


namespace CadViewer.UIControls
{
	public partial class CFloatingWinFormsWindow : Window
	{
		static CFloatingWinFormsWindow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CWindow),
				new FrameworkPropertyMetadata(typeof(CWindow)));
		}

		public CFloatingWinFormsWindow()
		{
			this.AllowsTransparency = true;
			this.WindowStyle = WindowStyle.None;
			this.Background = Brushes.Transparent;
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}
	}
}
