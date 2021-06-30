using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderPayments
    {
        public int serviceOrderPayment_id { get; set; }
        public decimal serviceOrder_id { get; set; }
        public int paymentMethod_id { get; set; }
        public int customer_id { get; set; }
        public decimal paymentValue { get; set; }
        public DateTime paymentDate { get; set; }
        public decimal ? discountGiven { get; set; }
        public string attachment { get; set; }
        public DateTime dateRegistration { get; set; }
        public string userRegistrationId { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        public virtual ServiceOrder serviceOrder { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        public virtual PaymentMethods paymentMethod { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        public virtual Customer customer { get; set; }

        public virtual ICollection<ServiceOrderPaymentsItems> serviceOrderPaymentsItems { get; set; }
    }
}
