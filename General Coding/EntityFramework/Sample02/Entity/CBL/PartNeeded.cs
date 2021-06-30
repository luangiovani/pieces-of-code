using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class PartNeeded
    {
        /// <summary>
        /// partNeeded_id 
        /// Id interno que será utilizado nos relacionamentos do BD e controle único
        /// </summary>
        public int partNeeded_id { get; set; }

        /// <summary>
        /// serviceOrderQuoting_id 
        /// Id FK interno que será utilizado nos relacionamentos do BD 
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

        public string partNeeded { get; set; }

        /// <summary>
        /// supplier_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        public int? supplier_id { get; set; }

        public decimal partCost { get; set; }

        /// <summary>
        /// arriving
        /// Atingir, Vir, Chegar
        /// </summary>
        public DateTime? arriving { get; set; }

        /// <summary>
        /// media_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        public int? media_id { get; set; }

        public int? id_old { get; set; }

        /// <summary>
        /// serviceOrderQuoting
        /// Ordem do serviço cotado
        /// </summary>
        public virtual ServiceOrderQuoting serviceOrderQuoting { get; set; }

        /// <summary>
        /// supplier
        /// Fornecedor
        /// </summary>
        public virtual Suppliers supplier { get; set; }

        /// <summary>
        /// media_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        public virtual Media media { get; set; }
    }
}
