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
    /// Mapeamento da Entidade Agents Commissions (Comissões de Parceiros da CBL), para gravação na tabela AgentCommissions no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class AgentCommissionsViewModel
    {
        public AgentCommissionsViewModel()
        {
            //agent = new AgentViewModel();
        }
        /// <summary>
        /// Agent Commissions ID
        /// Id interno da comissão do parceiro que será utilizado nos relacionamentos e controle único
        /// </summary>
        [Key]
        [Display(Name = "Agent Commissions ID")]
        public int agentCommissions_id { get; set; }

        /// <summary>
        /// Service Order ID (Job ID, Inquiry ID, Project ID)
        /// Id interno da ordem de serviço oriunda do Commissionamento do parceiro
        /// </summary>
        [Required(ErrorMessage="Service Order is required.")]
        [Display(Name = "Service Order")]
        public decimal serviceOrder_id { get; set; }

        /// <summary>
        /// Agent ID
        /// Id interno do parceiro que será utilizado nos relacionamentos e controle único
        /// </summary>
        [Required(ErrorMessage="Agent ID is required")]
        [Display(Name="Agent")]
        public int agent_id { get; set; }

        /// <summary>
        /// Currency
        /// Sigla da moeda do valor pago de Commissionamento para o parceiro
        /// </summary>
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Display(Name = "Currency")]
        public string currency { get; set; }

        /// <summary>
        /// Project Invoice
        /// Nota Fiscal vinculada a Ordem de Serviço
        /// </summary>
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Display(Name = "Invoice")]
        public string projectInvoice { get; set; }

        /// <summary>
        /// Quoted Amount
        /// Valor total orçado para o serviço a ser executado na Ordem de Serviço
        /// </summary>
        [Display(Name = "Quoted Amount")]
        public decimal quotedAmount { get; set; }

        /// <summary>
        /// Discount Given
        /// Valor do Desconto concedido ao Parceiro
        /// </summary>
        [Display(Name = "Discount Given")]
        public decimal discountGiven { get; set; }
        
        /// <summary>
        /// Discount Given
        /// Valor do Desconto concedido ao Parceiro
        /// </summary>
        [Display(Name = "Next Depot Amount")]
        public decimal nextDepotAmount { get; set; }

        /// <summary>
        /// Parts Purchase
        /// Indicador da quantidade de peças adquiridas para a execução da ordem de serviço
        /// </summary>
        [Display(Name = "Parts Purchased")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string partsPurchased { get; set; }

        /// <summary>
        /// Time Needed
        /// Indicador da quantidade de tempo para a execução da ordem de serviço
        /// </summary>
        [Display(Name = "Time Needed")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string timeNeeded { get; set; }

        /// <summary>
        /// Amount Comm
        /// Valor total da comissão
        /// </summary>
        [Display(Name = "Amount Comm")]
        public decimal amountComm { get; set; }

        /// <summary>
        /// Commission Paid
        /// Valor total pago da Comissão
        /// </summary>
        [Display(Name = "Commission Paid")]
        public decimal commisionPaid { get; set; }

        /// <summary>
        /// Commision Given
        /// Valor total da comissão dada para a ordem de serviço
        /// </summary>
        [Display(Name = "Commision Given")]
        public decimal commisionGiven { get; set; }

        /// <summary>
        /// Date Received
        /// Data de Recebimento da Comissão
        /// </summary>
        [ScaffoldColumn(false)]
        [Display(Name = "Date Received")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? received { get; set; }

        /// <summary>
        /// Date Quoted
        /// Data de orçamento da Ordem de Serviço
        /// </summary>
        [ScaffoldColumn(false)]
        [Display(Name = "Date Quoted")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? quoted { get; set; }

        /// <summary>
        /// Date Go Ahead
        /// Data que a ordem de serviço passou para o status Go Ahead
        /// </summary>
        [ScaffoldColumn(false)]
        [Display(Name = "Date GoAhead")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? goAhead { get; set; }

        /// <summary>
        /// Date of Paid of Costumer
        /// Data de pagamento do Consumidor
        /// </summary>
        [ScaffoldColumn(false)]
        [Display(Name = "Date of Paid Costumer")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? customerPaid { get; set; }

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

        ///// <summary>
        ///// Service Order
        ///// Ordem de Serviço da comissão do parceiro
        ///// </summary>
        //[Display(Name = "Service Order")]
        //public virtual ServiceOrderViewModel serviceOrder { get; set; }

        /// <summary>
        /// Agent
        /// Parceiro da comissão
        /// </summary>
        [Display(Name = "Agent")]
        public virtual AgentViewModel agent { get; set; }
    }
}
