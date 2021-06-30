using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderNotes
    {
        /// <summary>
        /// ServiceOrderNotes ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int serviceOrderNotes_id { get; set; }

        /// <summary>
        /// Note ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int note_id { get; set; }

        /// <summary>
        /// serviceOrder_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public decimal serviceOrder_id { get; set; }

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

        public string serviceOrderStatus { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        public virtual ServiceOrder serviceOrder { get; set; }

        /// <summary>
        /// Note
        /// Nota
        /// </summary>
        public virtual Notes note { get; set; }
    }
}
