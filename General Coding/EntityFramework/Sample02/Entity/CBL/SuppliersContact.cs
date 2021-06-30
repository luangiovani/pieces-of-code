using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class SuppliersContact
    {
        /// <summary>
        /// Suppliers Contact ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int suppliersContact_id { get; set; }

        /// <summary>
        /// Supplier ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int supplier_id { get; set; }

        /// <summary>
        /// Contact ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int contact_id { get; set; }

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

        /// <summary>
        /// supplier
        /// Fornecedor
        /// </summary>
        public virtual Suppliers supplier { get; set; }

        /// <summary>
        /// contact
        /// contato
        /// </summary>
        public virtual Contact contact { get; set; }
    }
}
