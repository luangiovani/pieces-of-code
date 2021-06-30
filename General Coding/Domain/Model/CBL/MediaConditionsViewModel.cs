using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class MediaConditionsViewModel
    {
        /// <summary>
        /// mediaConditions_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Media Conditions ID")]
        public int mediaConditions_id { get; set; }
        
        /// <summary>
        /// Name
        /// Nome da Media
        /// </summary>
        [Required(ErrorMessage = "Name of Media is required")]
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição da Media
        /// </summary>     
        [Display(Name = "Description")]       
        public string description { get; set; }

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
