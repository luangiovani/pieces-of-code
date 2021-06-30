using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class Stock
    {
        public int stock_id { get; set; }

        public int? media_id { get; set; }

        public int? component_id { get; set; }

        public string material { get; set; }

        public DateTime dateOfMovement { get; set; }

        public decimal quantity { get; set; }

        public string typeOfMovement { get; set; }

        public string stockAddress { get; set; }

        public int? location_id { get; set; }

        public decimal? serviceOrder_id { get; set; }

        public string notes { get; set; }

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
        /// Date of Register
        /// Data da inclusão deste registro
        /// </summary>
        public DateTime lastUpdateDate { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
        public string lastUpdateUser_id { get; set; }

        public int? id_old { get; set; }

        public virtual Locations Location { get; set; }
        
        public virtual Media Media { get; set; }

        public virtual Component Component { get; set; }

    }
}
