using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using AppControls;
using AppControls.EventHandlerArgs;
using DBConnector.RequestExecuter;
using PurchasesAnalysis.Core.Models;
using PurchasesAnalysis.Core.Repositories;

namespace PurchasesAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPurchasesRepository _purchasesRepository;
        private readonly IRequestExecuter _requestExecuter;

        public Test[] Test { get; set; }

        public MainWindow(IPurchasesRepository purchasesRepository, IRequestExecuter requestExecuter)
        {
            _purchasesRepository = purchasesRepository;
            _requestExecuter = requestExecuter;

            InitializeComponent();
            //Test code!!!!!!
            var table = _requestExecuter.ExecuteSelect("select pu.price, t.name from purchases pu join type t on t.id = pu.type");

            Test = table.Rows.OfType<DataRow>().Select(r => new Test {
                Type = (string)r.ItemArray[1],
                Price = (decimal)r.ItemArray[0]
            }).ToArray();

            Table.ItemsSource = Test;

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
