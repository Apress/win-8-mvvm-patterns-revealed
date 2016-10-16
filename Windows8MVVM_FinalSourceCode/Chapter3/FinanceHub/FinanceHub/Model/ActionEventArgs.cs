using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceHub.Model
{
    public class ActionEventArgs
    {
        public StockAction Action { get; set; }
        public object Data { get; set; }
    }
}
