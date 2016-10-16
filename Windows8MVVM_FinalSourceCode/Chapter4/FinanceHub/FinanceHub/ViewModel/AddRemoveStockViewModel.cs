using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using FinanceHub.Common;
using FinanceHub.Model;
using FinanceHub.Infrastructure;
using System.Collections.ObjectModel;

namespace FinanceHub.ViewModel
{
    public class AddRemoveStockViewModel : BindableBase
    {
        private string symbol;

        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                this.OnPropertyChanged("Symbol");
            }
        }

        private ICommand addCommand;

        public ICommand AddCommand
        {
            get { return addCommand; }
        }

        public AddRemoveStockViewModel()
        {
            this.addCommand = new DelegateCommand(ExecuteAdd);
        }

        private void ExecuteAdd(object param)
        {
            // Notify Subscribers
            if (this.Symbol != null)
            {
                EventAggregator.Instance.Publish(new ActionEventArgs { Action = StockAction.Add, Data = this.Symbol });
                this.Symbol = null;
            }
        }

        private ObservableCollection<StockViewModel> stocks;
        public ObservableCollection<StockViewModel> Stocks
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

        private ICommand removeStockCommand;
        public ICommand RemoveStockCommand
        {
            get { return removeStockCommand; }

        }

        public AddRemoveStockViewModel(IList<Stock> stocks)
            : this()
        {
            this.Stocks = new ObservableCollection<StockViewModel>(stocks.Select(c => new StockViewModel { Stock = c }).ToList());
            this.removeStockCommand = new DelegateCommand(ExecuteRemove);
        }

        private void ExecuteRemove(object param)
        {
            var stockToRemove = this.Stocks.Where(c => c.IsSelected).Select(c => c.Stock);
            if (stockToRemove.Count() > 0)
            {
                foreach (var item in stockToRemove)
                {
                    EventAggregator.Instance.Publish(new ActionEventArgs { Action = StockAction.Remove, Data = item });

                }
            }
        }


    }
}
