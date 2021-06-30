using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Framework.Domain.Model.CBL
{
    public class LogSistemaViewModel
    {/// <summary>
        /// LogSistema ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Log ID")]
        public int log_id { get; set; }
        
        /// <summary>
        /// Description
        /// descricao do LogSistema
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }


       

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
        /// </summary>
        public DateTime date { get; set; }

        

    }
}
