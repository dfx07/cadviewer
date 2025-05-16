using CadViewer.Interfaces;
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
	/// <summary>
	/// Interaction logic for DlgSetting.xaml
	/// </summary>
	public partial class DlgSetting : ModalDialog
	{
		public DlgSetting(IDialogService dlgService):
			base(dlgService)
		{
			InitializeComponent();
		}
	}
}
