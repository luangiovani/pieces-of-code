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
    /// Mapeamento da Entidade HearOfUs (De onde nos conhece, ou , como chegou até nós), para gravação na tabela HearOfUs no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class HearOfUsViewModel
    {
        /// <summary>
        /// Hear Of Us ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Hear Of Us ID")]
        public int hearOfUs_id { get; set; }

        /// <summary>
        /// Name
        /// Nome da origem de onde chegou até a CBL
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição da Origem de onde chegou até a CBL
        /// </summary>
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }

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

        public string userRegistration_id { get; set; }

    }
}
