using CadViewer.Interfaces;
using CadViewer.ViewModels.Properties;
using CadViewer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;

namespace CadViewer.ViewModels
{
	public class PropertiesPanelViewModel : ViewModelBase
	{
		public PropertiesPanelViewModel()
		: base()
		{
			CurrentPropertyView = new CirclePropertyViewModel();
		}

		public void SelectShape(Shape shape)
		{
			//if (shape is Circle circle)
			//	PropertiesPanelVM.CurrentPropertyViewModel = new CirclePropertyViewModel(circle);
			//else if (shape is Rect rect)
			//	PropertiesPanelVM.CurrentPropertyViewModel = new RectPropertyViewModel(rect);
		}

		private ViewModelBase m_currentPropertyView;
		public ViewModelBase CurrentPropertyView
		{
			get => m_currentPropertyView;
			set => SetProperty(ref m_currentPropertyView, value);
		}
	}
}