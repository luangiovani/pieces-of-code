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
    /// Mapeamento da Entidade FollowUpQuestions (Perguntas de FollowUp), para gravação na tabela FollowUpQuestions no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class FollowUpQuestionsViewModel
    {
        /// <summary>
        /// Follow Up Question ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Follow Up Question ID")]
        public int followUpQuestion_id { get; set; }

        /// <summary>
        /// Description
        /// Descrição ou pergunta para o FollowUp
        /// </summary>
        [Required(ErrorMessage = "Description is Required")]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }

        /// <summary>
        /// Question order
        /// Ordem que deverá aparecer a pergunta no Follow Up
        /// </summary>
        [Display(Name = "Sort")]
        public int questionOrder { get; set; }
    }
}
