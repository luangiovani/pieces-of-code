using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
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
    public class Notes
    {
        /// <summary>
        /// Note ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int note_id { get; set; }

        /// <summary>
        /// Description
        /// Descrição da nota
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Date
        /// Data da nota
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// Type note ID
        /// Tipo da nota
        /// </summary>
        public int typenote_id { get; set; }

        /// <summary>
        /// User ID
        /// Id do usuário
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// Type of Note
        /// Tipo da nota
        /// </summary>
        public virtual TypeOfNote typeOfNote { get; set; }

        /// <summary>
        /// User
        /// Usuário
        /// </summary>
        public virtual ApplicationUser user { get; set; }

        /// <summary>
        /// Indicador de status da nota
        /// </summary>
        //public bool active { get; set; }

        public int? id_old { get; set; }

        public virtual ICollection<ServiceOrderNotes> serviceOrderNotes { get; set; }
    }
}
