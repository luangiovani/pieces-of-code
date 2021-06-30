using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class MediaConditions
    {
        /// <summary>      
        /// mediaConditions_id 
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int mediaConditions_id { get; set; }

        /// <summary>      
        /// name 
        /// Nome da media
        /// </summary>
        public string name { get; set; }

        /// <summary>      
        /// description 
        /// Descrição
        /// </summary>
        public string description { get; set; }

        /// <summary>      
        /// active 
        /// Indicador do status da media
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
