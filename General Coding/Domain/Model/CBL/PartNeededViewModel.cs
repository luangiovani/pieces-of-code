using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class PartNeededViewModel
    {
        /// <summary>
        /// partNeeded_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "PartNeeded ID")]
        public int partNeeded_id { get; set; }

        /// <summary>
        /// serviceOrderQuoting_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Service Order ID")]
        public int serviceOrder_id { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
        /// </summary>
        [Display(Name = "Date Registration")]
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
        [Display(Name = "User Registration Id")]
        public string userRegistration_id { get; set; }

        /// <summary>
        /// partNeeded
        /// Peças necessárias
        /// </summary>
        [Display(Name = "Part Needed")]
        public string partNeeded { get; set; }

        /// <summary>
        /// supplier_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "supplier ID")]
        public int? supplier_id { get; set; }

        /// <summary>
        /// partCost
        /// Parte do Custo 
        /// </summary>
        [Display(Name = "PartCost")]
        public decimal partCost { get; set; }

        /// <summary>
        /// Arriving
        /// Atingir, Vir, Chegar
        /// </summary>
        [Display(Name = "Arriving")]
        public DateTime? arriving { get; set; }

        /// <summary>
        /// media_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Media ID")]
        public int? media_id { get; set; }

        /// <summary>
        /// serviceOrderQuoting
        /// Ordem do serviço cotado
        /// </summary>
        [Display(Name = "Service Order Quoting")]
        public virtual ServiceOrderQuoting serviceOrderQuoting { get; set; }

        /// <summary>
        /// supplier
        /// Fornecedor
        /// </summary>
        [Display(Name = "Supplier")]
        public virtual SuppliersViewModel supplier { get; set; }

        /// <summary>
        /// media_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        public virtual MediaViewModel media { get; set; }
    }
}
