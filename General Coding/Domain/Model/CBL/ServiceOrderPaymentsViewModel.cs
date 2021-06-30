using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderPaymentsViewModel
    {
        public int serviceOrderPayment_id { get; set; }
        public decimal serviceOrder_id { get; set; }
        public int paymentMethod_id { get; set; }
        public int customer_id { get; set; }
        public decimal paymentValue { get; set; }
        public DateTime paymentDate { get; set; }
        public decimal? discountGiven { get; set; }
        public string attachment { get; set; }
        public DateTime dateRegistration { get; set; }
        public string userRegistrationId { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        public virtual ServiceOrderViewModel serviceOrder { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        public virtual PaymentMethodsViewModel paymentMethod { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        public virtual CustomerViewModel customer { get; set; }
    }
}
