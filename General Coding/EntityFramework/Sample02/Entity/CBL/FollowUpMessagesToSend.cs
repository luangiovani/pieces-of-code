using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class FollowUpMessagesToSend
    {
        public int followUpMessagesToSend_id { get; set; }
        public decimal serviceOrder_id { get; set; }
        public DateTime dateToSend  { get; set; }
        public string fromEmails { get; set; }
        public string toEmails { get; set; }
        public string bccEmails { get; set; }
        public string ccEmails { get; set; }
        public string subject  { get; set; }
        public string textBody  { get; set; }
        public DateTime dateUpdate { get; set; }
        public string userUpdate_id { get; set; }
        public DateTime dateRegistration { get; set; }
        public string userRegistration_id { get; set; }
        public DateTime? dateSented { get; set; }
        public string userSented_id { get; set; }
    }
}
