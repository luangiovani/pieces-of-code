using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ReferredBy
    {
        /// <summary>
        /// referredBy_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int referredBy_id { get; set; }

        /// <summary>
        /// name
        /// Nome
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// description
        /// Descrição
        /// </summary>  
        public string description { get; set; }

        /// <summary>
        /// Active
        /// Indicador de status 
        /// </summary>
        //public bool active { get; set; }

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
    }
}
