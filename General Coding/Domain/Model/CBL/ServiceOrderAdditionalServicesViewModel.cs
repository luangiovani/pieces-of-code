using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderAdditionalServicesViewModel
    {

        /// <summary>
        /// serviceOrderAdditionalServices_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "serviceOrderAdditionalServices ID")]
        public int serviceOrderAdditionalServices_id { get; set; }

        /// <summary>
        /// additionalServices_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Display(Name = "Additional Services Id")]
        public int additionalServices_id { get; set; }

        /// <summary>
        /// serviceOrder_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Display(Name = "Service Order Id")]
        public int serviceOrder_id { get; set; }
        
        
        /// <summary>
        /// quantity
        /// quantity
        /// </summary>     
        [Display(Name = "Quantity")]
        public string quantity { get; set; }


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
