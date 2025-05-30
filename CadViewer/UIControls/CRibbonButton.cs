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
using CadViewer.Dialogs;
using CadViewer.Interfaces;

namespace CadViewer.UIControls
{
	public enum ERibbonButtonDropDownDirection
	{
		Bottom,
		Right
	}

	public class CRibbonToggleButton : ToggleButton
	{
		static CRibbonToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CRibbonToggleButton), new FrameworkPropertyMetadata(typeof(CRibbonToggleButton)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}

		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CRibbonToggleButton), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CRibbonToggleButton), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty OnTextProperty =
		DependencyProperty.Register(nameof(OnText), typeof(string), typeof(CRibbonToggleButton), new PropertyMetadata("On"));

		public string OnText
		{
			get => (string)GetValue(OnTextProperty);
			set => SetValue(OnTextProperty, value);
		}

		public static readonly DependencyProperty OffTextProperty =
		DependencyProperty.Register(nameof(OffText), typeof(string), typeof(CRibbonToggleButton), new PropertyMetadata("Off"));

		public string OffText
		{
			get => (string)GetValue(OffTextProperty);
			set => SetValue(OffTextProperty, value);
		}

		public static readonly DependencyProperty IsOpenDropDownProperty =
		DependencyProperty.Register(nameof(IsOpenDropDown), typeof(bool), typeof(CRibbonToggleButton), new PropertyMetadata(false));
		public bool IsOpenDropDown
		{
			get => (bool)GetValue(IsOpenDropDownProperty);
			set => SetValue(IsOpenDropDownProperty, value);
		}

		public static readonly DependencyProperty DropDownContentProperty =
		DependencyProperty.Register(nameof(DropDownContent), typeof(object), typeof(CRibbonToggleButton));
		public object DropDownContent
		{
			get => GetValue(DropDownContentProperty);
			set => SetValue(DropDownContentProperty, value);
		}

		public static readonly DependencyProperty DropDownDirectionProperty =
		DependencyProperty.Register(nameof(DropDownDirection), typeof(ERibbonButtonDropDownDirection), 
		typeof(CRibbonToggleButton), new PropertyMetadata(ERibbonButtonDropDownDirection.Bottom));

		public ERibbonButtonDropDownDirection DropDownDirection
		{
			get => (ERibbonButtonDropDownDirection)GetValue(DropDownDirectionProperty);
			set => SetValue(DropDownDirectionProperty, value);
		}
	}

	public class CRibbonButton : Button
	{
		static CRibbonButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CRibbonButton), new FrameworkPropertyMetadata(typeof(CRibbonButton)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
		}

		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CRibbonButton), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CRibbonButton), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty OnTextProperty =
		DependencyProperty.Register(nameof(OnText), typeof(string), typeof(CRibbonButton), new PropertyMetadata("On"));

		public string OnText
		{
			get => (string)GetValue(OnTextProperty);
			set => SetValue(OnTextProperty, value);
		}

		public static readonly DependencyProperty OffTextProperty =
		DependencyProperty.Register(nameof(OffText), typeof(string), typeof(CRibbonButton), new PropertyMetadata("Off"));

		public string OffText
		{
			get => (string)GetValue(OffTextProperty);
			set => SetValue(OffTextProperty, value);
		}

		public static readonly DependencyProperty IsOpenDropDownProperty =
		DependencyProperty.Register(nameof(IsOpenDropDown), typeof(bool), typeof(CRibbonButton), new PropertyMetadata(false));
		public bool IsOpenDropDown
		{
			get => (bool)GetValue(IsOpenDropDownProperty);
			set => SetValue(IsOpenDropDownProperty, value);
		}

		public static readonly DependencyProperty DropDownContentProperty =
		DependencyProperty.Register(nameof(DropDownContent), typeof(object), typeof(CRibbonButton));
		public object DropDownContent
		{
			get => GetValue(DropDownContentProperty);
			set => SetValue(DropDownContentProperty, value);
		}

		public static readonly DependencyProperty DropDownDirectionProperty =
		DependencyProperty.Register(nameof(DropDownDirection), typeof(ERibbonButtonDropDownDirection),
		typeof(CRibbonButton), new PropertyMetadata(ERibbonButtonDropDownDirection.Bottom));

		public ERibbonButtonDropDownDirection DropDownDirection
		{
			get => (ERibbonButtonDropDownDirection)GetValue(DropDownDirectionProperty);
			set => SetValue(DropDownDirectionProperty, value);
		}
	}
}
