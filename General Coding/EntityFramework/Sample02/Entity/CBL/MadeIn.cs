using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class MadeIn
    {
        /// <summary>
        /// Made In ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int madeIn_id { get; set; }

        /// <summary>
        /// Name
        /// Nome da Made In
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição da Made In
        /// </summary>
        public string description { get; set; }

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
    }
}
