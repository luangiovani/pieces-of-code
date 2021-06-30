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
    /// Mapeamento da Entidade ServiceOrderStatus (Status da Ordem de Serviço), para gravação na tabela ServiceOrderStatus no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class ServiceOrderStatusViewModel
    {
        /// <summary>
        /// Service Order Status ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Service Order Status ID")]
        [ScaffoldColumn(false)]
        public int serviceOrderStatus_id { get; set; }

        /// <summary>
        /// Service Order Status Parent ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>]
        [Display(Name = "Status Parent")]
        [ScaffoldColumn(false)]
        public int? serviceOrderStatusParent_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do Status da Ordem de Serviço
        /// </summary>
        [Required(ErrorMessage="Name is required.")]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// Name to Client
        /// Nome do Status para o Cliente da Ordem de Serviço
        /// </summary>
       
        [Display(Name = "Name to Client")]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string nameToClient { get; set; }

        /// <summary>
        /// Description
        /// Descrição do Status da ordem de serviço
        /// </summary>
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string description { get; set; }

        /// <summary>
        /// Order
        /// Ordem do Status, ordenação
        /// </summary>
        [Display(Name="Sort")]
        public int order { get; set; }

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

        [ScaffoldColumn(false)]
        [Display(Name="# of Service Orders in Status")]
        public int serviceOrdersInStatus { get; set; }

        
        /// <summary>
        /// Status parent do SubStatus
        /// </summary>
        [Display(Name="Status Parent")]
        public virtual ServiceOrderStatusViewModel serviceOrderStatusParent { get; set; }

        /*/// <summary>
        /// Lista de SubStatus vinculados como filhos deste Status
        /// </summary>
        [Display(Name = "Status Dependents")]
        public virtual ICollection<ServiceOrderStatusViewModel> serviceOrderStatusDependents { get; set; }
         * */
    }
}
