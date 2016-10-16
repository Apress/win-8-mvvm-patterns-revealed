using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows.Input;
using FinanceHub.Common;
using FinanceHub.Model;
using FinanceHub.Infrastructure;
using Windows.UI.Xaml.Controls.Primitives;
using System.Runtime.Serialization;

namespace FinanceHub.ViewModel
{
    public class StocksPageViewModel : BindableBase, IHandle<ActionEventArgs>
    {

        private ObservableCollection<Stock> stocks;

        public ObservableCollection<Stock> Stocks
        {
            get
            {
                return this.stocks;
            }
            internal set
            {
                this.stocks = value;
                this.OnPropertyChanged("Stocks");
            }
        }

        private INavigationService navigationService;
        
        public StocksPageViewModel(INavigationService navigationService)
        {
            this.Stocks = new ObservableCollection<Stock>();
            this.navigationService = navigationService;
            this.stockSelectedCommand = new DelegateCommand(ExecuteStockSelected);
            EventAggregator.Instance.Subscribe(this);
            this.LoadData();
        }

        private void ExecuteStockSelected(object param)
        {
            if (param != null)
            {
                this.CurrentStock = param as Stock;
            }
            navigationService.Navigate(ViewModelLocator.StockDetailsViewType);
        } 


        private IEnumerable<Stock> randomStockData;

        private async void LoadData()
        {
            this.randomStockData = new ObservableCollection<Stock>(await LocalStorageHelper.GetRandomStockData());
            await LocalStorageHelper.Restore<Stock>();
            if (LocalStorageHelper.Data.Count > 0)
            {
                foreach (Stock item in LocalStorageHelper.Data)
                {
                    this.Stocks.Add(item);
                }
            }
            else
            {
                this.Stocks.Add(this.GetNewStock("MSFT"));
            }
        }

        private Stock GetNewStock(string symbol)
        {
            System.Random RndNumber = new System.Random();
            int index = RndNumber.Next(0, 4);
            var newStock = this.randomStockData.ElementAt(index);
            newStock.Symbol = symbol;
            return newStock;
        }

        private ICommand stockSelectedCommand;

        public ICommand StockSelectedCommand
        {
            get { return stockSelectedCommand; }

        }

        private Stock currentStock;

        public Stock CurrentStock
        {
            get { return currentStock; }
            set
            {
                this.currentStock = value;
                this.OnPropertyChanged("CurrentStock");
            }
        }

        public void Handle(ActionEventArgs e)
        {
            if (e.Action == StockAction.Add)
            {
                if (this.Stocks.Count > 0 && !(this.Stocks.Where(c => c.Symbol.ToUpper() == e.Data.ToString().ToUpper()).Count() > 0))
                {
                    this.Stocks.Add(this.GetNewStock(e.Data.ToString().ToUpper()));
                }
            }
            else
            {
                this.Stocks.Remove(e.Data as Stock);
            }
        }
    
    }
}
