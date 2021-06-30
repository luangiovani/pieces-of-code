using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class RoleLocationsViewModel
    {
        [Key]
        [Display(Name="Id")]
        public int roleLocations_id { get; set; }

        /// <summary>
        /// Location ID
        /// </summary>
        [Display(Name = "Location")]
        [Required(ErrorMessage="The field {0} is required")]
        public int location_id { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "The field {0} is required")]
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

        [Display(Name = "Location")]
        public virtual LocationsViewModel location { get; set; }

        [Display(Name = "Perfil")]
        public virtual PerfilViewModel perfil { get; set; }
    }
}
