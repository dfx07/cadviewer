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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace CadViewer.UIControls
{
	public class CExpander : Expander
	{
		static CExpander()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CExpander),
				new FrameworkPropertyMetadata(typeof(CExpander)));
		}

		private Border _HeaderBorder = null;
		private ToggleButton _ToogleButton = null;

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{
				_HeaderBorder = GetTemplateChild("xExpanderHeaderBorder") as Border;
				_ToogleButton = GetTemplateChild("HeaderSite") as ToggleButton;

				_HeaderBorder.MouseLeftButtonUp += (sender, args) =>
				{
					IsExpanded = !IsExpanded;
					e.Handled = true;
				};
			};

			UpdateExpansionState(false);
		}

		private void UpdateExpansionState(bool bUseTransitions)
		{
			var state = "";

			if (!IsExpanded)
			{
				state = ExpandDirection == ExpandDirection.Up ? "CollapsedUp" : "CollapsedDown";
			}
			else
			{
				state = ExpandDirection == ExpandDirection.Up ? "ExpandedUp" : "ExpandedDown";
			}

			if(state != string.Empty)
				VisualStateManager.GoToState(this, state, bUseTransitions);
		}

		protected override void OnExpanded()
		{
			base.OnExpanded();
			UpdateExpansionState(true);
		}

		protected override void OnCollapsed()
		{
			base.OnCollapsed();
			UpdateExpansionState(true);
		}

		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CExpander), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImagePlacementProperty =
		DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CExpander), new PropertyMetadata(ImagePlacement.Left));

		public ImagePlacement ImagePlacement
		{
			get => (ImagePlacement)GetValue(ImagePlacementProperty);
			set => SetValue(ImagePlacementProperty, value);
		}

		public static readonly DependencyProperty HeaderHeightProperty =
		DependencyProperty.Register(nameof(HeaderHeight), typeof(double), typeof(CExpander), new PropertyMetadata(20.0));

		public double HeaderHeight
		{
			get => (double)GetValue(HeaderHeightProperty);
			set => SetValue(HeaderHeightProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CExpander), new PropertyMetadata(double.NaN));
		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}
	}
}
