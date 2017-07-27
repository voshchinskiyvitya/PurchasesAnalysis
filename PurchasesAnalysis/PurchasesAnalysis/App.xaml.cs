using System;
using System.Collections.Generic;
using System.Windows;
using Ninject;
using PurchasesAnalysis.Core.Models.OLAP;
using PurchasesAnalysis.Core.Models.OLAP.Dimentions;

namespace PurchasesAnalysis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            ConfigureDbStructure();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            container = new StandardKernel(new DIModule());
        }

        private void ComposeObjects()
        {
            Current.MainWindow = container.Get<MainWindow>();
        }

        private void ConfigureDbStructure()
        {
            OlapDb.RegisterDbStructure(new FactsTable("Purchases", new List<IFact>
            {
                new Fact<int>("Quantity"), 
                new Fact<decimal>("Price") 
            }),
            new List<IDimention>
            {
                new Dimention("Dates", new List<ICriteria>
                {
                    new Criteria<DateTime>("Date"),
                    new Criteria<int>("Year"),
                    new Criteria<int>("Month"),
                    new Criteria<int>("DayOfWeek"),
                    new Criteria<int>("DayOfMonth")
                }),
                new Dimention("Type", new List<ICriteria>
                {
                    new Criteria<string>("Name")
                }),
                new Dimention("Product", new List<ICriteria>
                {
                    new Criteria<string>("Name")
                })
            });
        }
    }
}
