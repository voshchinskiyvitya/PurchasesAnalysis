using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AppControls.EventHandlerArgs;

namespace AppControls
{
    /// <summary>
    /// Interaction logic for AutoComplete.xaml
    /// </summary>
    public partial class AutoComplete : UserControl
    {
        private volatile bool _ignoreChange;

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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_ignoreChange)
            {
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
