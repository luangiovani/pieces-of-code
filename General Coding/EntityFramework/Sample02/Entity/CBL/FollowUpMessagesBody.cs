using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class FollowUpMessagesBody
    {
        /// <summary>
        /// 
        /// </summary>
        public int followUpMessages_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string textBody { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
        /// </summary>
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da update deste registro
        /// </summary>
        public DateTime dateUpdate { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
        public string userRegistration_id { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que update no registro
        /// </summary>
        public string userUpdate_id { get; set; }
    }
}
