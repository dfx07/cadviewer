﻿using CadViewer.ViewModels;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CadViewer.View
{
	/// <summary>
	/// Interaction logic for PCBViewer.xaml
	/// </summary>
	public partial class PCBViewer : UserControl
	{
		public PCBViewer()
		{
			InitializeComponent();

			Loaded += OnPCBViewer_Loaded;
		}

		private void OnPCBViewer_Loaded(object sender, RoutedEventArgs e)
		{
			PCBViewModel.InitContext();
		}
		private void OnPCBViewer_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (PCBViewModel != null)
			{
				PCBViewModel.OnViewChanged((int)e.NewSize.Width, (int)e.NewSize.Height);
			}
		}

		// DependencyProperty for binding the model
		public static readonly DependencyProperty PCBViewModelProperty =
			DependencyProperty.Register(nameof(PCBViewModel), typeof(PCBViewModel), typeof(PCBViewer), new PropertyMetadata(null));

		public PCBViewModel PCBViewModel
		{
			get => (PCBViewModel)GetValue(PCBViewModelProperty);
			set => SetValue(PCBViewModelProperty, value);
		}
	}
}
