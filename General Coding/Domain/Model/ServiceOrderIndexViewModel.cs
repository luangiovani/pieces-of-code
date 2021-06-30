using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.Domain.Model
{
    public class ServiceOrderIndexViewModel
    {
        public decimal serviceOrder_id { get; set; }
        public string orderIdOrderSeries { get; set; }
        public string customerName { get; set; }
        public string serviceOrderStatus { get; set; }
        public string serviceOrderDate { get; set; }
        public bool isRush { get; set; }
        public int delayDays { get; set; }
    }
}