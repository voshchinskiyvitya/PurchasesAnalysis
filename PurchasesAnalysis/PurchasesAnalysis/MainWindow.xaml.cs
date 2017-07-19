using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppControls;
using AppControls.EventHandlerArgs;
using DBConnector.ConnectionFactory;
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
        public Test[] Test { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            //Test code!!!!!!
            var table = new RequestExecuter(new DbConnectionFactory()).ExecuteSelect("select * from [dbo].[product]");
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
                var table =
                    new RequestExecuter(new DbConnectionFactory()).ExecuteSelect(
                        "select * from [dbo].[product] where name like '" + e.Text + "%'");
                var products = table.Rows.OfType<DataRow>().Select(r => (string) r.ItemArray[1]).ToArray();
                ((AutoComplete) sender).SetListItems(products);
                //Test code!!!!!!
            }
        }

        private void OnAddButtonClick(object sender, object e)
        {
            //Test code!!!!!!
            var repo = new PurchasesRepository(new RequestExecuter(new DbConnectionFactory()));

            repo.Save((PurchaseItem) e);
            //Test code!!!!!!
        }
    }
}
