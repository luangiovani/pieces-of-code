using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class SuppliersContactViewModel
    {
        /// <summary>
        /// Suppliers Contact ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Suppliers Contact ID")]
        public int suppliersContact_id { get; set; }

        /// <summary>
        /// Supplier ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Supplier ID")]
        public int supplier_id { get; set; }

        /// <summary>
        /// Contact ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Contact ID")]
        public int contact_id { get; set; }

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
        [Display(Name = "Contact ID")]
        public string userRegistration_id { get; set; }

        /// <summary>
        /// supplier
        /// Fornecedor
        /// </summary>
        public virtual SuppliersViewModel supplier { get; set; }

        /// <summary>
        /// contact
        /// contato
        /// </summary>
        public virtual ContactViewModel contact { get; set; }

    }
}
