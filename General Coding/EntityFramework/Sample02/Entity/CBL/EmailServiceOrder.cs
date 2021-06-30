using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class EmailServiceOrder
    {
        /// <summary>      
        /// emailServiceOrder_id 
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int emailServiceOrder_id { get; set; }

        /// <summary>      
        /// name 
        /// Nome
        /// </summary>
        public string name { get; set; }

        /// <summary>      
        /// email 
        /// email
        /// </summary>
        public string email { get; set; }

        /// <summary>      
        /// active 
        /// Indicador active
        /// </summary>
        public bool active { get; set; }

        /// <summary>
        /// Location ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int location_id { get; set; }

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

        /// <summary>
        /// locations
        /// Locations
        /// </summary>
        public virtual Locations location { get; set; }
        
        public int? id_old { get; set; }
    }
}
