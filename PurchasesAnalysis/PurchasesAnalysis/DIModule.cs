using DBConnector.ConnectionFactory;
using DBConnector.RequestExecuter;
using Ninject.Modules;
using PurchasesAnalysis.Core.ContextProvider;
using PurchasesAnalysis.Core.Repositories;
using PurchasesAnalysis.Core.Services;

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
            Bind<IDbContextProvider>().To<DbContextProvider>().InSingletonScope();
            Bind<IAnalysisService>().To<AnalysisService>().InSingletonScope();
        }
    }
}
