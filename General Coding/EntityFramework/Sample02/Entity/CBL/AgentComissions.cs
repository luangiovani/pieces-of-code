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
    /// Mapeamento da Entidade Agents Commissions (Comissões de Parceiros da CBL), para gravação na tabela AgentCommissions no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class AgentCommissions
    {
        /// <summary>
        /// Agent Commissions ID
        /// Id interno da comissão do parceiro que será utilizado nos relacionamentos e controle único
        /// </summary>
        public int agentCommissions_id { get; set; }

        /// <summary>
        /// Service Order ID (Job ID, Inquiry ID, Project ID)
        /// Id interno da ordem de serviço oriunda do Commissionamento do parceiro
        /// </summary>
        public decimal serviceOrder_id { get; set; }

        /// <summary>
        /// Agent ID
        /// Id interno do parceiro que será utilizado nos relacionamentos e controle único
        /// </summary>
        public int agent_id { get; set; }
        
        /// <summary>
        /// Currency
        /// Sigla da moeda do valor pago de Commissionamento para o parceiro
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// Project Invoice
        /// Nota Fiscal vinculada a Ordem de Serviço
        /// </summary>
        public string projectInvoice { get; set; }

        /// <summary>
        /// Quoted Amount
        /// Valor total orçado para o serviço a ser executado na Ordem de Serviço
        /// </summary>
        public decimal quotedAmount { get; set; }

        /// <summary>
        /// Discount Given
        /// Valor do Desconto concedido ao Parceiro
        /// </summary>
        public decimal discountGiven { get; set; }

        /// <summary>
        /// Discount Given
        /// Valor do Desconto concedido ao Parceiro
        /// </summary>
        public decimal nextDepotAmount { get; set; }

        /// <summary>
        /// Parts Purchase
        /// Indicador da quantidade de peças adquiridas para a execução da ordem de serviço
        /// </summary>
        public string partsPurchased { get; set; }

        /// <summary>
        /// Time Needed
        /// Indicador da quantidade de tempo para a execução da ordem de serviço
        /// </summary>
        public string timeNeeded { get; set; }

        /// <summary>
        /// Amount Comm
        /// Valor total da comissão
        /// </summary>
        public decimal amountComm { get; set; }

        /// <summary>
        /// Commission Paid
        /// Valor total pago da Comissão
        /// </summary>
        public decimal commisionPaid { get; set; }

        /// <summary>
        /// Commision Given
        /// Valor total da comissão dada para a ordem de serviço
        /// </summary>
        public decimal commisionGiven { get; set; }

        /// <summary>
        /// Date Received
        /// Data de Recebimento da Comissão
        /// </summary>
        public DateTime? received { get; set; }

        /// <summary>
        /// Date Quoted
        /// Data de orçamento da Ordem de Serviço
        /// </summary>
        public DateTime? quoted { get; set; }

        /// <summary>
        /// Date Go Ahead
        /// Data que a ordem de serviço passou para o status Go Ahead
        /// </summary>
        public DateTime? goAhead { get; set; }

        /// <summary>
        /// Date of Paid of Costumer
        /// Data de pagamento do Consumidor
        /// </summary>
        public DateTime? customerPaid { get; set; }

        /// <summary>
        /// Active
        /// Indicador do status da comissão do parceiro
        /// </summary>
        //public bool active { get; set; }

        public int? id_old { get; set; }

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
        /// Service Order
        /// Ordem de Serviço da comissão do parceiro
        /// </summary>
        public virtual ServiceOrder serviceOrder { get; set; }

        /// <summary>
        /// Agent
        /// Parceiro da comissão
        /// </summary>
        public virtual Agent agent { get; set; }
    }
}
