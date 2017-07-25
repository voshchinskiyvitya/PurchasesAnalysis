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
        private readonly IRequestExecuter _requestExecuter;
        private readonly IPurchasesRepository _purchasesRepository;

        public Test[] Test { get; set; }

        public MainWindow(IRequestExecuter requestExecuter, IPurchasesRepository purchasesRepository)
        {
            _requestExecuter = requestExecuter;
            _purchasesRepository = purchasesRepository;

            InitializeComponent();
            //Test code!!!!!!
            var table = _requestExecuter.ExecuteSelect("select * from [dbo].[product]");
            Test = table.Rows.OfType<DataRow>().Select(r => new Test {
                Name = (string) r.ItemArray[1],
                Id = (int)r.ItemArray[0]
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
