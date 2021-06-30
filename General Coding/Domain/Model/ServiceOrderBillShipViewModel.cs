using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model
{
    public class ServiceOrderBillShipViewModel
    {
        public ServiceOrderBillShipViewModel()
        {
            billing = new ServiceOrderBillingViewModel();
            shipping = new ServiceOrderShippingViewModel();
        }
        public ServiceOrderBillingViewModel billing { get; set; }
        public ServiceOrderShippingViewModel shipping { get; set; }
        public decimal serviceOrder_id { get; set; }
    }
}
