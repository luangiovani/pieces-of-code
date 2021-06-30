using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class StockInOutViewModel : StockViewModel
    {
        public StockInOutViewModel()
        {
            StockList = new List<StockViewModel>();
        }
        public ICollection<StockViewModel> StockList { get; set; }
    }
}
