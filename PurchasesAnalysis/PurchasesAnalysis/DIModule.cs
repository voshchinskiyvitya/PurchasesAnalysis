using DBConnector.ConnectionFactory;
using DBConnector.RequestExecuter;
using Ninject.Modules;
using PurchasesAnalysis.Core.Repositories;

namespace PurchasesAnalysis
{
    public class DIModule: NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindow>().ToSelf();
            Bind<IRequestExecuter>().To<RequestExecuter>().InSingletonScope();
            Bind<IDbConnectionFactory>().To<DbConnectionFactory>().InSingletonScope();
            Bind<IPurchasesRepository>().To<PurchasesRepository>().InSingletonScope();
        }
    }
}
