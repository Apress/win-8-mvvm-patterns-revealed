using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace FinanceHub.Model
{
    [KnownType(typeof(FinanceHub.Model.Stock))]
    [DataContractAttribute]
    public class Stock
    {
        [DataMember()]
        public string Symbol { get; set; }
        [DataMember()]
        public decimal CurrentPrice { get; set; }
        [DataMember()]
        public decimal OpenPrice { get; set; }
        [DataMember()]
        public double Change { get; set; }
        [DataMember()]
        public string DaysRange { get; set; }
        [DataMember()]
        public string Range52Week { get; set; }
    }
}
