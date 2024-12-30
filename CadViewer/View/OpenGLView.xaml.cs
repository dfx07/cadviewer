using CadViewer.Components;
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

namespace CadViewer.View
{
	/// <summary>
	/// Interaction logic for OpenGLView.xaml
	/// </summary>
	public partial class OpenGLView : UserControl
	{
		public OpenGLView()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty OpenGLContentProperty =
			DependencyProperty.Register(nameof(OpenGLContent), typeof(OpenGLHost), typeof(OpenGLView), new PropertyMetadata(null));

		public OpenGLHost OpenGLContent
		{
			get => (OpenGLHost)GetValue(OpenGLContentProperty);
			set => SetValue(OpenGLContentProperty, value);
		}
	}
}
