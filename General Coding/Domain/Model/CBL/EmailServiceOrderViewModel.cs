using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class EmailServiceOrderViewModel
    {
        /// <summary>
        /// emailServiceOrder_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Email ID")]
        public int emailServiceOrder_id { get; set; }
        
        /// <summary>
        /// Name
        /// Nome 
        /// </summary>
        [Required(ErrorMessage = "Name")]
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// email
        /// email
        /// </summary>     
        [Display(Name = "E-mail")]
        public string email { get; set; }

        /// <summary>
        /// location_id
        /// location_id
        /// </summary>     
        [Display(Name = "Location")]
        public int location_id { get; set; }

        /// <summary>
        /// Location
        /// Location
        /// </summary>
        [Display(Name = "Location")]
        public virtual LocationsViewModel location { get; set; }

        /// <summary>
        /// active
        /// active
        /// </summary>     
        [Display(Name = "Active")]
        public bool active { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste parceiro
        /// </summary>
        [Display(Name = "Register Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>

        public string userRegistration_id { get; set; }






    }
}
