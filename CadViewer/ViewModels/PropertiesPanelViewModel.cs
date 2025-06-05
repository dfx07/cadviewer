using CadViewer.Interfaces;
using CadViewer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using CadViewer.Services;
using System.Diagnostics;

namespace CadViewer.ViewModels
{
	public class PropertiesPanelViewModel : ViewModelBase
	{
		public PropertiesPanelViewModel()
			: base()
		{
			Messenger.Register<ShapeActionMessageArgs>(this, OnReceivedShapeActionMessage);
		}

		private void OnReceivedShapeActionMessage(ShapeActionMessageArgs args)
		{
			if(args is null || string.IsNullOrEmpty(args.ShapeType))
				return;

			if(args.ActionType == "Deactivate")
			{
				CurrentPropertyViewModel = null;
				return;
			}

			if (args.ShapeType == "Circle")
			{
				CurrentPropertyViewModel = new CirclePropertyViewModel();
			}
			else if (args.ShapeType == "Line")
			{
				CurrentPropertyViewModel = new LinePropertyViewModel();
			}
			else if (args.ShapeType == "Rectangle")
			{
				CurrentPropertyViewModel = new RectPropertyViewModel();
			}
			else
			{
				CurrentPropertyViewModel = null;
			}
		}

		private ViewModelBase m_currentPropertyViewModel;
		public ViewModelBase CurrentPropertyViewModel
		{
			get => m_currentPropertyViewModel;
			set => SetProperty(ref m_currentPropertyViewModel, value);
		}
	}
}