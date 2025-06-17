using CadViewer.Interfaces;
using CadViewer.Services;
using CadViewer.UIControls;
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

namespace CadViewer.Dialogs
{
	public partial class DlgProgress : ModalDialog
	{
		public DlgProgress(IDialogService dlgService):
			base(dlgService)
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty ProcessMessageProperty =
		DependencyProperty.Register(nameof(ProcessMessage), typeof(string), typeof(DlgProgress), new PropertyMetadata("..."));

		public string ProcessMessage
		{
			get => (string)GetValue(ProcessMessageProperty);
			set => SetValue(ProcessMessageProperty, value);
		}
	}
}
