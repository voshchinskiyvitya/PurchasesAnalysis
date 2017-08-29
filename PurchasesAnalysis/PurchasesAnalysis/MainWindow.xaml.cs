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
                select);

            
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
    }
}
