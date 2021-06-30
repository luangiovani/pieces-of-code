using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderAdditionalServices
    {
        /// <summary>      
        /// serviceOrderAdditionalServices_id 
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>

        public decimal serviceOrderAdditionalServices_id { get; set; }

        /// <summary>      
        /// additionalServices_id 
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public decimal additionalServices_id { get; set; }

        /// <summary>      
        /// name 
        /// Nome
        /// </summary>
        public decimal serviceOrder_id { get; set; }

        /// <summary>      
        /// description 
        /// Descrição
        /// </summary>
        public int quantity { get; set; }

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

        //public virtual ServiceOrder serviceOrder { get; set; }
        //public virtual AdditionalServices additionalServices { get; set; }
        
        public int? id_old { get; set; }
    }
}
