using System;
using System.Windows.Controls;
using System.Windows.Media;
using AppControls.EventHandlerArgs;
using PurchasesAnalysis.Core.Models;
using Color = System.Windows.Media;

namespace AppControls
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : UserControl
    {
        private readonly SolidColorBrush errorBrush = new SolidColorBrush(Color.Color.FromArgb(255, 255, 176, 176));
        private readonly SolidColorBrush normalBrush = new SolidColorBrush(Color.Color.FromRgb(255, 255, 255));

        // TODO: should be removed when custom inputs will be implemented.
        private volatile bool _isDateError;
        private volatile bool _isTypeError;
        private volatile bool _isQuantityError;
        private volatile bool _isPriceError;

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

            var product = new ProductItem
            {
                Name = ProductAutocomplete.Text,
            };

            var type = new TypeItem
            {
                Name = Type.Text
            };

            var purchase = new PurchaseItem
            {
                Product = product,
                Type = type,
                Date = Date.SelectedDate.Value,
                // ValidateInputs method provides 100% guarantee that values will be correct.
                Price = decimal.Parse(Price.Text),
                Quantity = int.Parse(Quantity.Text)
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
            if (string.IsNullOrEmpty(ProductAutocomplete.Text))
            {
                ProductAutocomplete.ShowError();
                return false;
            }

            if (!Date.SelectedDate.HasValue)
            {
                Date.Background = errorBrush;
                _isDateError = true;
                return false;
            }

            if (string.IsNullOrEmpty(Type.Text))
            {
                Type.Background = errorBrush;
                _isTypeError = true;
                return false;
            }

            int stubInt;
            if (string.IsNullOrEmpty(Quantity.Text) || !int.TryParse(Quantity.Text, out stubInt))
            {
                Quantity.Background = errorBrush;
                _isQuantityError = true;
                return false;
            }

            decimal stubDec;
            if (string.IsNullOrEmpty(Price.Text) || !decimal.TryParse(Price.Text, out stubDec))
            {
                Price.Background = errorBrush;
                _isPriceError = true;
                return false;
            }

            return true;
        }

        private void Type_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isTypeError)
            {
                Type.Background = normalBrush;
                _isTypeError = false;
            }
        }

        private void Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isQuantityError)
            {
                Quantity.Background = normalBrush;
                _isQuantityError = false;
            }
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isPriceError)
            {
                Price.Background = normalBrush;
                _isPriceError = false;
            }
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isDateError)
            {
                Date.Background = normalBrush;
                _isDateError = false;
            }
        }
    }
}
