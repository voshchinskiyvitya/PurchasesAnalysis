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
        public event EventHandler<AutoCompleteTextChangedArgs> OnProductRequest;

        public AddWindow()
        {
            InitializeComponent();
            ProductAutocomplete.OnTextChanged += OnAutocompleteChange;
        }

        public void Open()
        {
            Visibility = System.Windows.Visibility.Visible;
        }

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
