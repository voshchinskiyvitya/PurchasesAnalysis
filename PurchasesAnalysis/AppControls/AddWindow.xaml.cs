using System;
using System.Windows.Controls;
using AppControls.EventHandlerArgs;

namespace AppControls
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : UserControl
    {
        /// <summary>
        /// Will be invoked when user try to select product in autocomplete.
        /// </summary>
        public event EventHandler<AutoCompleteTextChangedArgs> OnProductRequest;

        public AddWindow()
        {
            InitializeComponent();
            ProductAutocomplete.OnTextChanged += OnAutocompleteChange;
        }

        /// <summary>
        /// Opens Add window.
        /// </summary>
        public void Open()
        {
            Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Closes Add window.
        /// </summary>
        public void Close()
        {
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private void OnAutocompleteChange(object sender, AutoCompleteTextChangedArgs e)
        {
            OnProductRequest?.Invoke(sender, e);
        }
    }
}
