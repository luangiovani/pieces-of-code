using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderPaymentsItemsViewModel
    {
        public int serviceOrderPaymentItems_id { get; set; }
        public int serviceOrderPayment_id { get; set; }
        public int? additionalService_id { get; set; }
        public string description { get; set; }
        public decimal itemValue { get; set; }
        public DateTime dateRegistration { get; set; }
        public string userRegistrationId { get; set; }

        public virtual ServiceOrderPaymentsViewModel serviceOrderPayment { get; set; }
    }
}
