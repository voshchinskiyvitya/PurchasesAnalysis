using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using AppControls;
using AppControls.EventHandlerArgs;
using DBConnector.RequestExecuter;
using PurchasesAnalysis.Core.Extentions;
using PurchasesAnalysis.Core.Models;
using PurchasesAnalysis.Core.Repositories;
using PurchasesAnalysis.Core.Services;
using Expression = System.Linq.Expressions.Expression;
using Type = System.Type;

namespace PurchasesAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPurchasesRepository _purchasesRepository;
        private readonly IRequestExecuter _requestExecuter;
        private readonly IAnalysisService _analysisService;

        private readonly AddWindow addWindow;

        #region Filter properties
        private string _selectedFilter;

        public IList<string> FilterItems => Constants.AllDimentions;

        public string SelectedFilter
        {
            get { return _selectedFilter; }
            set
            {
                OnFilterChanged(value);
                _selectedFilter = value;
            } 
        }

        public List<Expression<Func<Purchase, bool>>> SelectedFilterExpressions
        {
            get
            {
                if (SelectedFilter == Constants.Dimentions.Product.GetDescription())
                {
                    var filterTypes = ValueSelect.SelectedItems.OfType<string>();
                    return new List<Expression<Func<Purchase, bool>>> { p => filterTypes.Contains(p.Product.Name) };
                }

                if (SelectedFilter == Constants.Dimentions.Type.GetDescription())
                {
                    var filterTypes = ValueSelect.SelectedItems.OfType<string>();
                    return new List<Expression<Func<Purchase, bool>>> { p => filterTypes.Contains(p.Type.Name) };
                }

                return new List<Expression<Func<Purchase, bool>>>();
            }
        }
        #endregion

        #region Aggregation properties
        private string _selectedAggregation = Constants.Aggregation.Sum.GetDescription();

        public IList<string> AggregationItems => Constants.AllAggregation;

        public string SelectedAggregation
        {
            get { return _selectedAggregation; }
            set
            {
                //OnAggregationChanged(value);
                _selectedAggregation = value;
            }
        }
        #endregion

        #region Select properties
        private string _selectedFact = Constants.Facts.Price.GetDescription();
        private string _selectedDimention = Constants.Dimentions.Date.GetDescription();

        public IList<string> FactItems => Constants.AllFacts;
        public IList<string> DimentionItems => Constants.AllDimentions;

        public string SelectedFact
        {
            get { return _selectedFact; }
            set
            {
                //OnAggregationChanged(value);
                _selectedFact = value;
            }
        }

        public string SelectedDimention
        {
            get { return _selectedDimention; }
            set
            {
                //OnAggregationChanged(value);
                _selectedDimention = value;
            }
        }

        public Type DimentionType
        {
            get
            {
                if (SelectedDimention == Constants.Dimentions.Product.GetDescription())
                    return typeof (Product);
                if (SelectedDimention == Constants.Dimentions.Type.GetDescription())
                    return typeof(Core.Models.Type);

                return typeof(Date);
            }
        }

        public Type FactType
        {
            get
            {
                if (SelectedDimention == Constants.Facts.Price.GetDescription())
                    return typeof(decimal);

                return typeof(int);
            }
        }

        #endregion

        public MainWindow(
            IPurchasesRepository purchasesRepository, 
            IRequestExecuter requestExecuter,
            IAnalysisService analysisService)
        {
            _purchasesRepository = purchasesRepository;
            _requestExecuter = requestExecuter;
            _analysisService = analysisService;

            InitializeComponent();

            ApplyButton_Click(null, null);

            addWindow = new AddWindow();

            addWindow.OnProductRequest += OnProductRequest;
            addWindow.OnAddButtonClick += OnAddButtonClick;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addWindow.Open();
        }

        private void OnProductRequest(object sender, AutoCompleteTextChangedArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                //Test code!!!!!!
                var table = _requestExecuter.ExecuteSelect("select * from [dbo].[product] where name like '" + e.Text + "%'");
                var products = table.Rows.OfType<DataRow>().Select(r => (string) r.ItemArray[1]).ToArray();
                ((AutoComplete) sender).SetListItems(products);
                //Test code!!!!!!
            }
        }

        private void OnAddButtonClick(object sender, object e)
        {
            //Test code!!!!!!
            _purchasesRepository.Save((PurchaseItem) e);
            //Test code!!!!!!
        }

        private void OnFilterChanged(string newValue)
        {
            //Test code!!!!!!
            if (newValue == Constants.Dimentions.Product.GetDescription())
            {
                var table = _requestExecuter.ExecuteSelect("select distinct name from [dbo].[product]");
                var products = table.Rows.OfType<DataRow>().Select(r => (string) r.ItemArray[0]).ToArray();
                ValueSelect.ItemsSource = products;
            }

            if (newValue == Constants.Dimentions.Type.GetDescription())
            {
                var table = _requestExecuter.ExecuteSelect("select distinct name from [dbo].[type]");
                var types = table.Rows.OfType<DataRow>().Select(r => (string)r.ItemArray[0]).ToArray();
                ValueSelect.ItemsSource = types;
            }
            //Test code!!!!!!
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var purchases = _analysisService.Analyse(
                SelectedFilterExpressions,
                SelectedDimention.GetValueByDescription<Constants.Dimentions>(),
                SelectedFact.GetValueByDescription<Constants.Facts>(),
                SelectedAggregation.GetValueByDescription<Constants.Aggregation>()
                );

            Table.ItemsSource = purchases.ToList();
            if (Table.Columns.Any())
            {
                Table.Columns.FirstOrDefault().Header = SelectedDimention;
                Table.Columns.LastOrDefault().Header = SelectedFact;
            }
        }
    }
}
