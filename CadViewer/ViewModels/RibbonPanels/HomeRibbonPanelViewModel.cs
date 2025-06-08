using CadViewer.Common;
using CadViewer.Interfaces;
using CadViewer.Services;
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

		private bool _isCircleActive;
		public bool IsCircleActive
		{
			get => _isCircleActive;
			set => SetProperty(ref _isCircleActive, value);
		}

		private bool _isRectActive;
		public bool IsRectActive
		{
			get => _isRectActive;
			set => SetProperty(ref _isRectActive, value);
		}

		private bool _isShowUIActive;
		public bool IsShowUIActive
		{
			get => _isShowUIActive;
			set => SetProperty(ref _isShowUIActive, value);
		}

		private bool _isViewerActive;
		public bool IsViewerActive
		{
			get => _isViewerActive;
			set => SetProperty(ref _isViewerActive, value);
		}

		public ICommand LineInputCommand { get; }
		public ICommand CircleInputCommand { get; }
		public ICommand RectInputCommand { get; }
		public ICommand ShowUIDesignCommand { get; }
		public ICommand ShowViewerCommand { get; }

		public HomeRibbonPanelViewModel()
			: base()
		{
			LineInputCommand = new RelayCommand(OnInputLineShape);
			CircleInputCommand = new RelayCommand(OnInputCircleShape);
			RectInputCommand = new RelayCommand(OnInputRectShape);
			ShowUIDesignCommand = new RelayCommand(OnShowUIDesign);
			ShowViewerCommand = new RelayCommand(OnShowViewer);
		}

		private void OnInputLineShape()
		{
			Messenger.Send(new ShapeActionMessageArgs
			{
				MessageID = "ShapeAction",
				Sender = this,
				IsActive = IsLineActive,
				ShapeType = "Line",
				ActionType = IsLineActive ? "Create" : "Deactivate"
			});
		}

		private void OnInputCircleShape()
		{
			Messenger.Send(new ShapeActionMessageArgs
			{
				MessageID = "ShapeAction",
				Sender = this,
				IsActive = IsCircleActive,
				ShapeType = "Circle",
				ActionType = IsCircleActive ? "Create" : "Deactivate"
			});
		}

		private void OnInputRectShape()
		{
			Messenger.Send(new ShapeActionMessageArgs
			{
				MessageID = "ShapeAction",
				Sender = this,
				IsActive = IsRectActive,
				ShapeType = "Rectangle",
				ActionType = IsRectActive ? "Create" : "Deactivate"
			});
		}

		private void OnShowUIDesign()
		{
			Messenger.Send(new MessageArgs
			{
				MessageID = IsShowUIActive ? "UIShow" : "UIHide",
				Sender = this,
			});
		}

		private void OnShowViewer()
		{
			Messenger.Send(new MessageArgs
			{
				MessageID = IsViewerActive ? "ViewShow" : "ViewHide",
				Sender = this,
			});
		}
	}
}
