using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderNotesViewModel
    {
        /// <summary>
        /// ServiceOrderNotes ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "ServiceOrderNotes ID")]
        public int serviceOrderNotes_id { get; set; }

        /// <summary>
        /// Note ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Note ID")]
        public int note_id { get; set; }

        /// <summary>
        /// serviceOrder_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Service Order ID")]
        public decimal serviceOrder_id { get; set; }
        
        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
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

        [Required(ErrorMessage = "Service Order Status is Required")]
        [Display(Name = "Service Order Status")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string serviceOrderStatus { get; set; }

        public virtual ServiceOrderViewModel serviceOrderOfNote { get; set; }

        public virtual NotesViewModel note { get; set; }


    }
}
