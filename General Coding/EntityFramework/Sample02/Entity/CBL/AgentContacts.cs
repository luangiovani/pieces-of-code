using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class AgentContacts
    {
        public int agentContact_id { get; set; }

        public int agent_id { get; set; }

        public int contact_id { get; set; }

        //public bool active { get; set; }

        public string email { get; set; }
        public string password { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
        /// </summary>
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
        public string userRegistration_id { get; set; }

        public int? id_old { get; set; }

        public string tokenCode { get; set; }
        public bool ativo { get; set; }

        public virtual Agent agent { get; set; }

        public virtual Contact contact { get; set; }
    }
}
