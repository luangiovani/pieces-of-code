using Framework.Database.Entity.CBL;
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
    /// Mapeamento da Entidade Notes (Notas descritivas), para gravação na tabela Notes no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class NotesViewModel
    {
        /// <summary>
        /// Note ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Note ID")]
        public int note_id { get; set; }

        /// <summary>
        /// Description
        /// Descrição da nota
        /// </summary>
        [Required(ErrorMessage="Description is Required")]
        [Display(Name = "Description")]
        [StringLength(8000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }

        /// <summary>
        /// Date
        /// Data da nota
        /// </summary>
        [Display(Name = "Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date { get; set; }

        /// <summary>
        /// Type note ID
        /// Tipo da nota
        /// </summary>
        [Required(ErrorMessage = "Type of Note is Required")]
        [Display(Name = "Type Of Note")]
        public int typenote_id { get; set; }

        /// <summary>
        /// User ID
        /// Id do usuário
        /// </summary>
        [Required(ErrorMessage = "User is Required")]
        [Display(Name = "User")]
        [StringLength(128, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string user_id { get; set; }

        /// <summary>
        /// Type of Note
        /// Tipo da nota
        /// </summary>
        [Display(Name = "Type Of Note")]
        public virtual TypeOfNoteViewModel typeOfNote { get; set; }

        /// <summary>
        /// User
        /// Usuário
        /// </summary>
        [Display(Name = "User")]
        public virtual UsuarioViewModel user { get; set; }





        public static implicit operator NotesViewModel(Notes obj)
        {
            if (obj != null)
            {
                return new NotesViewModel
                {
                    note_id = obj.note_id,
                    description = obj.description,
                    date = obj.date,
                    typenote_id = obj.typenote_id,
                    //typeOfNote = obj.typeOfNote,
                    //user = obj.user,
                    user_id = obj.user_id

                };
            }
            else
            {
                return null;
            }
        }


        public static implicit operator Notes(NotesViewModel obj)
        {
            if (obj != null)
            {
                return new Notes
                {
                    note_id = obj.note_id,
                    description = obj.description,
                    date = obj.date,
                    typenote_id = obj.typenote_id,
                    //typeOfNote = obj.typeOfNote,
                    //user = obj.user,
                    user_id = obj.user_id

                };
            }
            else
            {
                return null;
            }
        }
    }
}
