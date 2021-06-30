using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class AdditionalServicesViewModel
    {
        /// <summary>
        /// emailServiceOrder_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Additional Services ID")]
        public int additionalServicesid { get; set; }
        
        /// <summary>
        /// Name
        /// Nome 
        /// </summary>
        [Required(ErrorMessage = "Name")]
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// description
        /// description
        /// </summary>     
        [Display(Name = "Description")]
        public string description { get; set; }

        /// <summary>
        /// value
        /// value
        /// </summary>     
        [Display(Name = "Value")]
        [Required(ErrorMessage = "Value")]
        public decimal value { get; set; }

        /// <summary>
        /// extraInformations
        /// extraInformations
        /// </summary>
        [Display(Name = "Extra Informations")]
        public string extraInformations { get; set; }        

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

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste parceiro
        /// </summary>
        [Display(Name = "Date Update")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateUpdate { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
        public string userUpdate_id { get; set; }


    }
}
