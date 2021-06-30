using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderMedias
    {
        public int serviceOrderMedias_id { get; set; }

        public decimal serviceOrder_id { get; set; }

        public int media_id { get; set; }

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
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        public virtual ServiceOrder serviceOrder { get; set; }

        /// <summary>
        /// Media
        /// Equipamento da Ordem de Serviço
        /// </summary>
        public virtual Media media { get; set; }
    }
}
