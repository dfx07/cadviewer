﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using CadViewer.Animations;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Threading;

namespace CadViewer.UIControls
{
	public class CGroupBox : GroupBox
	{
		static CGroupBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CGroupBox),
				new FrameworkPropertyMetadata(typeof(CGroupBox)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}

		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CGroupBox), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImagePlacementProperty =
		DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CGroupBox), new PropertyMetadata(ImagePlacement.Left));

		public ImagePlacement ImagePlacement
		{
			get => (ImagePlacement)GetValue(ImagePlacementProperty);
			set => SetValue(ImagePlacementProperty, value);
		}

		public static readonly DependencyProperty HeaderHeightProperty =
		DependencyProperty.Register(nameof(HeaderHeight), typeof(double), typeof(CGroupBox), new PropertyMetadata(20.0));

		public double HeaderHeight
		{
			get => (double)GetValue(HeaderHeightProperty);
			set => SetValue(HeaderHeightProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CGroupBox), new PropertyMetadata(double.NaN));
		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty ShowHeaderProperty =
		DependencyProperty.Register(nameof(ShowHeader), typeof(bool), typeof(CGroupBox), new PropertyMetadata(true));

		public bool ShowHeader
		{
			get => (bool)GetValue(ShowHeaderProperty);
			set => SetValue(ShowHeaderProperty, value);
		}
	}
}
