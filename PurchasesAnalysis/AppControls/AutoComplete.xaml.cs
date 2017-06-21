using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using AppControls.EventHandlerArgs;

namespace AppControls
{
    /// <summary>
    /// Interaction logic for AutoComplete.xaml
    /// </summary>
    public partial class AutoComplete : UserControl
    {
        private bool _ignoreChange;
        public event EventHandler<AutoCompleteTextChangedArgs> OnTextChanged;
        public event EventHandler<AutoCompleteItemSelectedArgs> OnItemSelected;

        public AutoComplete()
        {
            InitializeComponent();
            ListBox.Visibility = System.Windows.Visibility.Hidden;
        }

        public void SetListItems(IEnumerable<string> items)
        {
            ListBox.ItemsSource = items;
            if(items.Any())
                ListBox.Visibility = System.Windows.Visibility.Visible;
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
            _ignoreChange = true;
            if (e.AddedItems.Count > 0)
            {
                var text = (string) e.AddedItems[0];
                OnItemSelected?.Invoke(this, new AutoCompleteItemSelectedArgs(text));
            }
            _ignoreChange = false;
        }
    }
}
