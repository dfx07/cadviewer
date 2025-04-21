using System;
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
using System.Collections.ObjectModel;

namespace CadViewer.UIControls
{
	public class CTextBox : TextBox
	{
		static CTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CTextBox),
				new FrameworkPropertyMetadata(typeof(CTextBox)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			Loaded += (s, e) =>
			{

			};
		}
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnPreviewKeyDown(e);

			if (e.Key == Key.Escape)
			{
				SuggestionList.Clear();
				IsPopupOpen = false;
				this.Focus();

				e.Handled = true;
			}
			else if (UseSuggestion && (e.Key == Key.Down || e.Key == Key.Up) && SuggestionList?.Count > 0)
			{
				var popupList = GetTemplateChild("PART_SuggestionList") as ListBox;
				if (popupList != null && popupList.IsVisible)
				{
					int nSelectIndex = popupList.SelectedIndex;

					nSelectIndex += (e.Key == Key.Down) ? 1 : -1;

					if (nSelectIndex < 0)
						nSelectIndex = SuggestionList.Count - 1;

					if(nSelectIndex >= SuggestionList.Count)
						nSelectIndex = 0;

					popupList.SelectedIndex = nSelectIndex;

					popupList.ScrollIntoView(popupList.SelectedItem);

					var item = popupList.ItemContainerGenerator.ContainerFromIndex(popupList.SelectedIndex) as ListBoxItem;
					if (item != null)
					{
						bDisableTextChanged = true;
						this.Text = item.Content.ToString();
						this.CaretIndex = this.Text.Length;
						item.Focus();
					}
				}

				e.Handled = true;
			}
			else if (e.Key == Key.Enter)
			{
				if(IsPopupOpen)
				{
					this.CaretIndex = this.Text.Length;
					IsPopupOpen = false;
					this.Focus();
				}
			}
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			base.OnTextChanged(e);

			if(!UseSuggestion)
				return;

			if(bDisableTextChanged)
			{
				bDisableTextChanged = false;
				e.Handled = true;
				return;
			}

			if (IsPopupOpen == false)
			{
				IsPopupOpen = true;
			}

			string currentText = this.Text;
			var filteredSuggestions = GetFilteredSuggestions(currentText);
			SuggestionList.Clear();

			foreach (var suggestion in filteredSuggestions)
			{
				SuggestionList.Add(suggestion);
			}

			if (SuggestionList.Count <= 0)
			{
				IsPopupOpen = false;
			}
		}

		private bool bDisableTextChanged = false;

		private IEnumerable<string> GetFilteredSuggestions(string text)
		{
			var allSuggestions = new List<string>
			{
				"Apple", "Banana", "Cherry", "Date", "Grape", "Mango"
			};

			return allSuggestions.Where(s => s.Contains(text));
		}

		public static readonly DependencyProperty ImageSourceProperty =
		DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CTextBox), new PropertyMetadata(null));

		public ImageSource ImageSource
		{
			get => (ImageSource)GetValue(ImageSourceProperty);
			set => SetValue(ImageSourceProperty, value);
		}

		public static readonly DependencyProperty ImageWidthProperty =
		DependencyProperty.Register(nameof(ImageWidth), typeof(double), typeof(CTextBox), new PropertyMetadata(double.NaN));

		public double ImageWidth
		{
			get => (double)GetValue(ImageWidthProperty);
			set => SetValue(ImageWidthProperty, value);
		}

		public static readonly DependencyProperty ImagePlacementProperty =
		DependencyProperty.Register(nameof(ImagePlacement), typeof(ImagePlacement), typeof(CTextBox), new PropertyMetadata(ImagePlacement.Left));

		public ImagePlacement ImagePlacement
		{
			get => (ImagePlacement)GetValue(ImagePlacementProperty);
			set => SetValue(ImagePlacementProperty, value);
		}

		public static readonly DependencyProperty ImageMarginProperty =
		DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(CTextBox), new PropertyMetadata(new Thickness(1)));
		public Thickness ImageMargin
		{
			get => (Thickness)GetValue(ImageMarginProperty);
			set => SetValue(ImageMarginProperty, value);
		}

		public static readonly DependencyProperty PlaceholderProperty =
		DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(CTextBox), new PropertyMetadata(string.Empty));
		public string Placeholder
		{
			get => (string)GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}

		public static readonly DependencyProperty UseSuggestionProperty =
		DependencyProperty.Register(nameof(UseSuggestion), typeof(bool), typeof(CTextBox), new PropertyMetadata(false));
		public bool UseSuggestion
		{
			get => (bool)GetValue(UseSuggestionProperty);
			set => SetValue(UseSuggestionProperty, value);
		}

		public ObservableCollection<string> SuggestionList
		{
			get { return (ObservableCollection<string>)GetValue(SuggestionListProperty); }
			set { SetValue(SuggestionListProperty, value); }
		}

		public static readonly DependencyProperty SuggestionListProperty =
		DependencyProperty.Register(nameof(SuggestionList), typeof(ObservableCollection<string>), typeof(CTextBox), new PropertyMetadata(new ObservableCollection<string>()));

		public static readonly DependencyProperty IsPopupOpenProperty =
		DependencyProperty.Register(nameof(IsPopupOpen), typeof(bool), typeof(CTextBox), new PropertyMetadata(false));
		public bool IsPopupOpen
		{
			get => (bool)GetValue(IsPopupOpenProperty);
			set => SetValue(IsPopupOpenProperty, value);
		}
	}
}
