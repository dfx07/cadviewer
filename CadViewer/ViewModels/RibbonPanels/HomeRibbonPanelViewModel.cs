using CadViewer.Common;
using CadViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CadViewer.ViewModels
{
	public class HomeRibbonPanelViewModel : ViewModelBase
	{
		private bool _isLineActive;
		public bool IsLineActive
		{
			get => _isLineActive;
			set => SetProperty(ref _isLineActive, value);
		}

		public ICommand LineInputCommand { get; }

		public HomeRibbonPanelViewModel()
			: base()
		{
			LineInputCommand = new RelayCommand(OnLineInput);
		}

		private void OnLineInput()
		{
			if (IsLineActive)
				Debug.WriteLine("LineInput is ON");
			else
				Debug.WriteLine("LineInput is OFF");
		}
	}
}
