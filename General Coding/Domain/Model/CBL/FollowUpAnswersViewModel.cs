﻿using System;
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
    /// Mapeamento da Entidade FollowUpAnswers (Respostas de FollowUp), para gravação na tabela FollowUpAnswers no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class FollowUpAnswersViewModel
    {
        /// <summary>
        /// Follow Up Answer ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Follow Up Answer ID")]
        public int followUpAnswer_id { get; set; }

        /// <summary>
        /// Description
        /// Descrição ou valor da resposta de follow up
        /// </summary>
        [Required(ErrorMessage = "Description is Required")]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }

        /// <summary>
        /// Answer Order
        /// Ordem que deverá aparecer a resposta
        /// </summary>
        [Display(Name = "Sort")]
        public int answerOrder { get; set; }
    }
}
