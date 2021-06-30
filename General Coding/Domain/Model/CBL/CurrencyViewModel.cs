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
    /// Mapeamento da Entidade Currency (Moeda), para gravação na tabela Currency no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class CurrencyViewModel
    {
        /// <summary>
        /// Credit Terms ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Currency ID")]
        public int currency_id { get; set; }

        /// <summary>
        /// Initials
        /// Sigla da Moeda (US$, USD, R$...)
        /// </summary>
        [Required(ErrorMessage = "Initials is required")]
        [Display(Name = "Initials")]
        [StringLength(4, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string initials { get; set; }

        /// <summary>
        /// Initials
        /// Sigla da Moeda (USS, USD, BRL...)
        /// </summary>

        [Display(Name = "currency")]
        [StringLength(3, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string currency { get; set; }

        /// <summary>
        /// Name
        /// Nome por extenso da moeda(Dólar, Dólar Canadense, Real...)
        /// </summary>
        [Required(ErrorMessage = "Name of Currency is required")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }
    }
}
