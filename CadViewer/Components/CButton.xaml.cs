using System;
using System.Collections.Generic;
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

namespace CadViewer.Components
{
	/// <summary>
	/// Interaction logic for CButton.xaml
	/// </summary>
	public partial class CButton : CPanelControl
	{
		public CButton()
		{
			InitializeComponent();

		}

		/// <summary>
		/// ////////////////////////////////////////////////////////////////////////////////////
		/// </summary>
		//public CornerRadius TCornerRadius
		//{
		//	get { return (CornerRadius)GetValue(TCornerRadiusProperty); }
		//	set { SetValue(TCornerRadiusProperty, value); }
		//}

		//public static readonly DependencyProperty TCornerRadiusProperty =
		//	DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(TButton),
		//		new PropertyMetadata());

		//public string TContent
		//{
		//	get { return (string)GetValue(TContentProperty); }
		//	set { SetValue(TContentProperty, value); }
		//}

		//public static readonly DependencyProperty TContentProperty =
		//	DependencyProperty.Register(nameof(TContent), typeof(string), typeof(TButton),
		//		new PropertyMetadata("Button"));

		//public Thickness TBorderThickness
		//{
		//	get { return (Thickness)GetValue(TBorderThicknessProperty); }
		//	set { SetValue(TBorderThicknessProperty, value); }
		//}

		//public static readonly DependencyProperty TBorderThicknessProperty =
		//	DependencyProperty.Register(nameof(TBorderThickness), typeof(Thickness), typeof(TButton),
		//		new PropertyMetadata());

		//public Brush TColor
		//{
		//	get { return (Brush)GetValue(TColorProperty); }
		//	set { SetValue(TColorProperty, value); }
		//}

		//public static readonly DependencyProperty TColorProperty =
		//	DependencyProperty.Register(nameof(TColor), typeof(Brush), typeof(TButton),
		//		new PropertyMetadata());

		//public Brush TBackground
		//{
		//	get { return (Brush)GetValue(TBackgroundProperty); }
		//	set { SetValue(TBackgroundProperty, value); }
		//}

		//public static readonly DependencyProperty TBackgroundProperty =
		//	DependencyProperty.Register(nameof(TBackground), typeof(Brush), typeof(TButton),
		//		new PropertyMetadata());
	}
}
