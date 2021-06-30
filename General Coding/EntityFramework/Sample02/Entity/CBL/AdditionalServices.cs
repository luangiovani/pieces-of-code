using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class AdditionalServices
    {
        /// <summary>      
        /// additionalServices_id 
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public decimal additionalServicesid { get; set; }

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
        /// value 
        /// valor
        /// </summary>
        public decimal valuess { get; set; }

        /// <summary>
        /// extraInformations
        /// informaçoes extras
        /// </summary>
        public string extraInformations { get; set; }

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

        
        //public virtual ICollection<ServiceOrderAdditionalServices> serviceOrderAdditionalServices { get; set; }
        
        public int? id_old { get; set; }
    }
}
