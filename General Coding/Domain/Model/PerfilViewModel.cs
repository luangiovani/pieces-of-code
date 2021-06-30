using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Model
{
    public class PerfilViewModel
    {
        [Key]
        [Display(Name = "Código Perfil")]
        public string id { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Perfil")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter no máximo {1} Caracteres.")]
        public string name { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Descrição")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter no máximo {1} Caracteres.")]
        public string discriminator { get; set; }

        [Display(Name = "Permission to send sms")]        
        public bool indSendSms { get; set; }

    }
}