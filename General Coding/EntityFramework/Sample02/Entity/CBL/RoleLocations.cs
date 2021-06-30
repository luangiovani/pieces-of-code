using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class RoleLocations
    {
        public int roleLocations_id { get; set; }

        public int location_id { get; set; }

        public string id_perfil { get; set; }

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

        public virtual ApplicationRole perfil { get; set; }

        public virtual Locations location { get; set; }
    }
}
