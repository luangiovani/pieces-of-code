using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Framework.Domain.Model
{
    public class SendEmailViewModel
    {
        public int customer_id { get; set; }
        public decimal serviceOrder_id { get; set; }
        public string typeEmail { get; set; }
        [AllowHtml]
        public string bodyEmail { get; set; }
        public string mediasToPrint { get; set; }
    }
}
