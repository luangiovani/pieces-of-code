using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <task_url>https://esfera.teamworkpm.net/#tasks/7016888</task_url>
/// <autor>Fabricio Kikina</autor>
/// <date>23/11/2016</date>
/// <sumary>
/// Mapeamento da Entidade Component, para gravação na tabela Component no Banco de Dados
/// </sumary>

namespace Framework.Domain.Model.CBL
{
    public class ComponentViewModel
    {/// <summary>
        /// Component ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Component ID")]
        public int component_id { get; set; }
        
        /// <summary>
        /// Description
        /// descricao do componente
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }


        /// <summary>
        /// Color
        /// Color do componente
        /// </summary>
        [Required(ErrorMessage = "Color is required")]
        [Display(Name = "Color")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string color { get; set; }

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

        /// <summary>
        /// Stock Address
        /// Endereço do Componente no Estoque
        /// </summary>
        [Display(Name = "Stock Address")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string stockAddress { get; set; }

        [Display(Name = "Stock Movements")]
        public virtual ICollection<StockViewModel> stockMovements { get; set; }
    }
}
