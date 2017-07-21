using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AppControls.EventHandlerArgs;
using Color = System.Windows.Media;

namespace AppControls
{
    /// <summary>
    /// Interaction logic for AutoComplete.xaml
    /// </summary>
    public partial class AutoComplete : UserControl
    {
        private volatile bool _ignoreChange;
        private volatile bool _isError;

        /// <summary>
        /// Returns text from autocomplete textbox.
        /// </summary>
        public string Text => TextBox.Text;

        /// <summary>
        /// Will be invoked when user enters text.
        /// </summary>
        public event EventHandler<AutoCompleteTextChangedArgs> OnTextChanged;

        /// <summary>
        /// Will be invoked when user selects item in drop down list.
        /// </summary>
        public event EventHandler<AutoCompleteItemSelectedArgs> OnItemSelected;

        public AutoComplete()
        {
            InitializeComponent();
            ListBox.Visibility = System.Windows.Visibility.Hidden;

            if (Width > 0)
            {
                ListBox.Width = Width;
                TextBox.Width = Width;
            }

            if (Height > 0 && Height - TextBox.Height > 0)
            {
                ListBox.Height = Height - TextBox.Height;
            }
        }

        /// <summary>
        /// Sets list items to autocomplete drop down list.
        /// </summary>
        /// <param name="items"></param>
        public void SetListItems(IList<string> items)
        {
            ListBox.ItemsSource = items;
            ListBox.Visibility = items.Any() ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Shows that entered value is invalid.
        /// </summary>
        /// <remarks>
        /// Should be invoked when autocomplete value didn't pass validation.
        /// </remarks>
        public void ShowError()
        {
            _isError = true;
            TextBox.Background = new SolidColorBrush(Color.Color.FromArgb(255, 255, 176, 176)); // Pink
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_ignoreChange)
            {
                if (_isError)
                {
                    TextBox.Background = new SolidColorBrush(Color.Color.FromRgb(255,255,255)); // White
                    _isError = false;
                }
                OnTextChanged?.Invoke(this, new AutoCompleteTextChangedArgs(TextBox.Text));
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;

            _ignoreChange = true;

            // Empty list.
            SetListItems(new List<string>());

            // Set selected item text.
            var text = (string) e.AddedItems[0];
            TextBox.Text = text;

            OnItemSelected?.Invoke(this, new AutoCompleteItemSelectedArgs(text));

            _ignoreChange = false;
        }
    }
}
