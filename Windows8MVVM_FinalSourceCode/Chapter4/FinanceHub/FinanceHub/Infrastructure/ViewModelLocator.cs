using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceHub.Model;
using FinanceHub.Infrastructure;
using FinanceHub.ViewModel;
using MetroIoc;

namespace FinanceHub.Infrastructure
{
    public class ViewModelLocator
    {
        public const string StockDetailsViewType = "FinanceHub.View.StockDetails";
        
        private Lazy<IContainer> container;
        public IContainer Container
        {
            get { return container.Value; }
        }

        public ViewModelLocator()
        {
            container = new Lazy<IContainer>(CreateContainer);
        }

        private IContainer CreateContainer()
        {
            var container = new MetroContainer();

            container.RegisterInstance(container);
            container.RegisterInstance<IContainer>(container);
            container.Register<INavigationService, NavigationService>
                (lifecycle: new SingletonLifecycle());
            return container;
        }

        public StocksPageViewModel StocksPageViewModel
        {
            get
            {
                var VM = Container.Resolve<StocksPageViewModel>();
                Container.RegisterInstance<IList<Stock>>(VM.Stocks);
                return VM;
            }
        }

        public INavigationService NavigationService
        {
            get { return Container.Resolve<INavigationService>(); }
        }

        public AddRemoveStockViewModel AddRemoveStockViewModel
        {
            get { return Container.Resolve<AddRemoveStockViewModel>(); }
        }
    }
}
