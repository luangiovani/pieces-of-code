using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class DocumentTypeViewModel
    {
        /// <summary>
        /// Document Type ID
        /// Identificador do tipo de documento
        /// </summary>
        [Key]
        [Display(Name = "Document Type ID")]
        public int documentType_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do Tipo de Documento
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// URL File
        /// Url do Arquivo do Documento
        /// </summary>
        [Display(Name = "File")]
        [StringLength(8000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string url_file { get; set; }

        /// <summary>
        /// Description
        /// Descrição do tipo de documento
        /// </summary>
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
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
