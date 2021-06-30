using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ReferredByViewModel
    {
        /// <summary>
        /// referredBy_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "ReferredBy ID")]
        public int referredBy_id { get; set; }

        /// <summary>
        /// name
        /// Nome
        /// </summary>
        [Required(ErrorMessage = "Name is Required.")]
        [Display(Name = "Name")]
        public string name { get; set; }

        /// <summary>
        /// description
        /// Descrição
        /// </summary>       
        [Display(Name = "Description")]
        public string description { get; set; }

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

    }
}
