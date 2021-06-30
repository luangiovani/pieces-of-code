using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ShippingMediaStatus
    {
        /// <summary>
        /// shippingMediaStatus_id
        /// Transporte de mídia Status_id
        /// </summary>
        public int shippingMediaStatus_id { get; set; }

        /// <summary>
        /// Name
        /// Nome
        /// </summary> 
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição
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

        public int? id_old { get; set; }
    }
}
