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
    /// Mapeamento da Entidade PaymentMethods (Meios de pagamentos), para gravação na tabela PaymentMethods no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class PaymentMethods
    {
        /// <summary>
        /// Payment Methods ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int paymentMethods_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do meio de pagamento
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição do meio de pagamento
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Data inicial que este meio de pagamento está dispoível
        /// </summary>
        public DateTime? AvailableDateFrom { get; set; }

        /// <summary>
        /// Data final que este meio de pagamento está dispoível
        /// </summary>
        public DateTime? AvailableDateTo { get; set; }

        /// <summary>
        /// Número mínimo de parcelas
        /// </summary>
        public int? MinParcels { get; set; }

        /// <summary>
        /// Número máximo de parcelas
        /// </summary>
        public int? MaxParcels { get; set; }

        /// <summary>
        /// Taxa percentual sobre o valor da venda que deve ser aplicado
        /// </summary>
        public decimal? TaxPercentage { get; set; }

        /// <summary>
        /// Taxa monetária sobre o valor da venda que deve ser aplicado
        /// </summary>
        public decimal? TaxMonetary { get; set; }

        /// <summary>
        /// Informações adicionais, este será usado caso tenha alguma observação ao usuário ao solicitar o meio de pagamento, por exemplo
        /// "Com esta forma de pagamento você terá até X dias para efetuar o pagamento e enviar o comprovante a CBL"
        /// </summary>
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// ID Old
        /// Indicador do ID no antigo AIMS
        /// </summary>
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

        public virtual ICollection<ServiceOrderPayments> serviceOrderPayments { get; set; }
    }
}
