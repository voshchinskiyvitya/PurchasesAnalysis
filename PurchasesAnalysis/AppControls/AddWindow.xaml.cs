using System;
using System.Windows.Controls;
using AppControls.EventHandlerArgs;
using PurchasesAnalysis.Core.Models;

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

        /// <summary>
        /// Will be invoked when user try to save purchased item.
        /// </summary>
        public event EventHandler<object> OnAddButtonClick;

        public AddWindow()
        {
            InitializeComponent();

            // Today should be dafault date for datepicker.
            Date.SelectedDate = DateTime.Now.Date;

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
            if (!ValidateInputs())
                return;

            var product = new Product
            {
                Id = 1,
                Name = ProductAutocomplete.Text,
                Price = decimal.Parse(Price.Text), // TODO: use try parse
                Quantity = int.Parse(Quantity.Text) // TODO: use try parse
            };

            var type = new PurchasesAnalysis.Core.Models.Type
            {
                Name = Type.Text
            };

            var purchase = new PurchaseItem
            {
                Product = product,
                Type = type,
                Date = Date.SelectedDate.Value
            };

            OnAddButtonClick?.Invoke(sender, purchase);
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

        private bool ValidateInputs()
        {
            // TODO: Apply validation logic.
            return true;
        }
    }
}
