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

        #region Filter properties
        private string _selectedFilter;

        public IList<string> FilterItems => Constants.Dimentions.All;

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
                if (SelectedFilter == Constants.Dimentions.Product)
                {
                    var filterTypes = ValueSelect.SelectedItems.OfType<string>();
                    return new List<Expression<Func<Purchase, bool>>> { p => filterTypes.Contains(p.Product1.Name) };
                }

                if (SelectedFilter == Constants.Dimentions.Type)
                {
                    var filterTypes = ValueSelect.SelectedItems.OfType<string>();
                    return new List<Expression<Func<Purchase, bool>>> { p => filterTypes.Contains(p.Type1.Name) };
                }

                return new List<Expression<Func<Purchase, bool>>>();
            }
        }
        #endregion

        #region Aggregation properties
        private string _selectedAggregation;

        public IList<string> AggregationItems => Constants.Aggregation.All;

        public string SelectedAggregation
        {
            get { return _selectedAggregation; }
            set
            {
                OnAggregationChanged(value);
                _selectedAggregation = value;
            }
        }
        #endregion

        #region Select properties
        private string _selectedFact;
        private string _selectedDimention;

        public IList<string> FactItems => Constants.Facts.All;
        public IList<string> DimentionItems => Constants.Dimentions.All;

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
                if (SelectedDimention == Constants.Dimentions.Product)
                    return typeof (Product);
                if (SelectedDimention == Constants.Dimentions.Type)
                    return typeof(Core.Models.Type);

                return typeof(Date);
            }
        }

        public Type FactType
        {
            get
            {
                if (SelectedDimention == Constants.Facts.Price)
                    return typeof(decimal);

                return typeof(int);
            }
        }

        #endregion

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
            if (newValue == Constants.Dimentions.Product)
            {
                var table = _requestExecuter.ExecuteSelect("select distinct name from [dbo].[product]");
                var products = table.Rows.OfType<DataRow>().Select(r => (string) r.ItemArray[0]).ToArray();
                ValueSelect.ItemsSource = products;
            }

            if (newValue == Constants.Dimentions.Type)
            {
                var table = _requestExecuter.ExecuteSelect("select distinct name from [dbo].[type]");
                var types = table.Rows.OfType<DataRow>().Select(r => (string)r.ItemArray[0]).ToArray();
                ValueSelect.ItemsSource = types;
            }
            //Test code!!!!!!
        }

        private void OnAggregationChanged(string newValue)
        {
            
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            switch (SelectedDimention)
            {
                case Constants.Dimentions.Product:
                {
                    if (SelectedFact == Constants.Facts.Price)
                    {
                        var purchases = _analysisService.Analyse(
                            SelectedFilterExpressions,
                            p => new AnalysisResult<string, decimal> {Key = p.Product1.Name, Value = p.Price},
                            i => new AnalysisResult<string, decimal> {Key = i.Key, Value = i.Sum(p => p.Value)});

                        Table.ItemsSource = purchases.ToList();
                    }
                    else
                    {
                        var purchases = _analysisService.Analyse(
                            SelectedFilterExpressions,
                            p => new AnalysisResult<string, int> {Key = p.Product1.Name, Value = p.Quantity},
                            i => new AnalysisResult<string, int> {Key = i.Key, Value = i.Sum(p => p.Value)});

                        Table.ItemsSource = purchases.ToList();
                    }
                }
                    break;
                case Constants.Dimentions.Type:
                {
                    if (SelectedFact == Constants.Facts.Price)
                    {
                        var purchases = _analysisService.Analyse(
                            SelectedFilterExpressions,
                            p => new AnalysisResult<string, decimal> {Key = p.Type1.Name, Value = p.Price},
                            i => new AnalysisResult<string, decimal> {Key = i.Key, Value = i.Sum(p => p.Value)});

                        Table.ItemsSource = purchases.ToList();
                    }
                    else
                    {
                        var purchases = _analysisService.Analyse(
                            SelectedFilterExpressions,
                            p => new AnalysisResult<string, int> {Key = p.Type1.Name, Value = p.Quantity},
                            i => new AnalysisResult<string, int> {Key = i.Key, Value = i.Sum(p => p.Value)});

                        Table.ItemsSource = purchases.ToList();
                    }
                    break;
                }
                case Constants.Dimentions.Date:
                {
                    if (SelectedFact == Constants.Facts.Price)
                    {
                        var purchases = _analysisService.Analyse(
                            SelectedFilterExpressions,
                            p => new AnalysisResult<DateTime, decimal> {Key = p.Date1.Date1, Value = p.Price},
                            i => new AnalysisResult<DateTime, decimal> {Key = i.Key, Value = i.Sum(p => p.Value)});

                        Table.ItemsSource = purchases.ToList();
                    }
                    else
                    {
                        var purchases = _analysisService.Analyse(
                            SelectedFilterExpressions,
                            p => new AnalysisResult<DateTime, int> {Key = p.Date1.Date1, Value = p.Quantity},
                            i => new AnalysisResult<DateTime, int> {Key = i.Key, Value = i.Sum(p => p.Value)});

                        Table.ItemsSource = purchases.ToList();
                    }
                    break;
                }
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
