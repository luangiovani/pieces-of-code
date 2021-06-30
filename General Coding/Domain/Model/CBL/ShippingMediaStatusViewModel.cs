using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ShippingMediaStatusViewModel
    {
        /// <summary>
        /// shippingMediaStatus_id
        /// Transporte de mídia Status_id
        /// </summary>
        [Key]
        [Display(Name = "Shipping Media Status ID")]
        public int shippingMediaStatus_id { get; set; }

        /// <summary>
        /// Name
        /// Nome
        /// </summary>       
        [Display(Name = "Name")]
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição
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
        [Key]
        [Display(Name = "User Registration ID")]
        public string userRegistration_id { get; set; }

    }
}
