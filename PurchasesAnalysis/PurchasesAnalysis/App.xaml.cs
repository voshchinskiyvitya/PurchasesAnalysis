using System.Windows;
using Ninject;

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
    }
}
