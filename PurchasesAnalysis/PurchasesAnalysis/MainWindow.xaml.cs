using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using AppControls;
using AppControls.EventHandlerArgs;
using DBConnector.RequestExecuter;
using PurchasesAnalysis.Core.Models;
using PurchasesAnalysis.Core.Repositories;
using PurchasesAnalysis.Core.Services;

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
        private string _selectedFilter;


        public IList<string> FilterItems => Constants.Filters.All;

        public string SelectedFilter
        {
            get { return _selectedFilter; }
            set
            {
                OnFilterChanged(value);
                _selectedFilter = value;
            } 
        }

        public Expression<Func<Purchase, bool>> SelectedFilterExpression
        {
            get
            {
                if (SelectedFilter == Constants.Filters.Product)
                {
                    var filterTypes = ValueSelect.SelectedItems.OfType<string>();
                    return p => filterTypes.Contains(p.Product1.Name);
                }

                if (SelectedFilter == Constants.Filters.Type)
                {
                    var filterTypes = ValueSelect.SelectedItems.OfType<string>();
                    return p => filterTypes.Contains(p.Type1.Name);
                }

                throw new InvalidOperationException("Unexpected filter type");
            }
        }

        public Test[] Test { get; set; }

        public MainWindow(
            IPurchasesRepository purchasesRepository, 
            IRequestExecuter requestExecuter, 
            IAnalysisService analysisService)
        {
            _purchasesRepository = purchasesRepository;
            _requestExecuter = requestExecuter;
            _analysisService = analysisService;

            InitializeComponent();
            //Test code!!!!!!
            Expression<Func<Purchase, AnalysisResult<DateTime, decimal>>> select = p => new AnalysisResult<DateTime, decimal> {Key = p.Date1.Date1, Value = p.Price};

            var purchases = _analysisService.Analyse(
                new List<Expression<Func<Purchase, bool>>> { p => p.Type1.Name == "Продукти" },
                select,
                i => new AnalysisResult<DateTime, decimal> { Key = i.Key, Value = i.Sum(p => p.Value) });

            
            Table.ItemsSource = purchases.ToList();

            AddWindow.OnProductRequest += OnProductRequest;
            AddWindow.OnAddButtonClick += OnAddButtonClick;

            //Test code!!!!!!
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWindow.Open();
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
            if (newValue == Constants.Filters.Product)
            {
                var table = _requestExecuter.ExecuteSelect("select distinct name from [dbo].[product]");
                var products = table.Rows.OfType<DataRow>().Select(r => (string) r.ItemArray[0]).ToArray();
                ValueSelect.ItemsSource = products;
            }

            if (newValue == Constants.Filters.Type)
            {
                var table = _requestExecuter.ExecuteSelect("select distinct name from [dbo].[type]");
                var types = table.Rows.OfType<DataRow>().Select(r => (string)r.ItemArray[0]).ToArray();
                ValueSelect.ItemsSource = types;
            }
            //Test code!!!!!!
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Expression<Func<Purchase, AnalysisResult<DateTime, decimal>>> select = p => new AnalysisResult<DateTime, decimal> { Key = p.Date1.Date1, Value = p.Price };

            var purchases = _analysisService.Analyse(new List<Expression<Func<Purchase, bool>>> { SelectedFilterExpression }, select,
                i => new AnalysisResult<DateTime, decimal> { Key = i.Key, Value = i.Sum(p => p.Value) });


            Table.ItemsSource = purchases.ToList();
        }
    }
}
