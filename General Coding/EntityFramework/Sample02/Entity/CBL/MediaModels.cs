using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class MediaModels
    {
        public int mediaModels_id { get; set; }

        public int manufacturer_id { get; set; }

        public string model { get; set; }

        public string compatibility { get; set; }

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

        public virtual Manufacturer manufacturer { get; set; }
    }
}
