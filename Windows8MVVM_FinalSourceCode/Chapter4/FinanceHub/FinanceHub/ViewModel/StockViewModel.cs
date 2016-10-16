using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FinanceHub.Common;
using FinanceHub.Infrastructure;
using FinanceHub.Model;


namespace FinanceHub.ViewModel
{
    public class StockViewModel : BindableBase
    {
        public Stock Stock { get; set; }
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }
    }
}
