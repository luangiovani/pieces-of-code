using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Mapeamento da Entidade LabNotes (Notas da Location CBL), para gravação na tabela LabNotes no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class LabNotesViewModel
    {
        /// <summary>
        /// Lab note ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Lab Note ID")]
        public int labNote_id { get; set; }

        /// <summary>
        /// Service Order ID
        /// Número da Ordem de Serviço que possui notas da Location CBL
        /// </summary>
        [Required(ErrorMessage="Service Order is Required")]
        [Display(Name="Service Order")]
        public decimal serviceOrder_id { get; set; }

        /// <summary>
        /// Note
        /// Nota escrita pela Location CBL
        /// </summary>
        [Required(ErrorMessage = "Note is Required")]
        [Display(Name = "Note")]
        [StringLength(8000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string note { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
        /// </summary>
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
         [Display(Name = "CSR")]
        public string userRegistration_id { get; set; }

         public virtual UsuarioViewModel usuario { get; set; }
        /// <summary>
        /// Service Order
        /// Ordem de Seriço da Lab Notes
        /// </summary>
        //[Display(Name="Service Order")]
        //public virtual ServiceOrderViewModel serviceOrder { get; set; }
    }
}
