using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderBilling
    {
        /// <summary>
        /// serviceOrderBilling_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public decimal serviceOrder_id { get; set; }

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
        /// billingAddress
        /// Endereço de cobrança
        /// </summary>
        public string billingAddress { get; set; }

        /// <summary>
        /// billingCompany
        /// Faturamento da Empresa
        /// </summary>
        public string billingCompany { get; set; }

        /// <summary>
        /// billingCity_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        public int? billingCity_id { get; set; }

        /// <summary>
        /// billingPostalZipCode
        /// Faturamento Postal, Código Postal
        /// </summary>
        public string billingPostalZipCode { get; set; }

        /// <summary>
        /// billingDistrict
        /// Distrito de faturamento
        /// </summary>
        public string billingDistrict { get; set; }

        /// <summary>
        /// paymentMethod
        /// Método de pagamento
        /// </summary>
        public string paymentMethod { get; set; }

        /// <summary>
        /// freight
        /// Método de pagamento
        /// </summary>
        public string freight { get; set; }

        /// <summary>
        /// creditCardNumber
        /// Número do Cartão de Crédito
        /// </summary>
        public string creditCardNumber { get; set; }

        /// <summary>
        /// creditCardNumber
        /// Número do Cartão de Crédito
        /// </summary>
        public string nameCreditCard { get; set; }

        /// <summary>
        /// expireCreditCard
        /// Cartão de crédito expirado
        /// </summary>
        public string expireCreditCard { get; set; }

        /// <summary>
        /// originalQuotedAmount
        /// VALOR ORIGINAL Citado
        /// </summary>
        public decimal originalQuotedAmount { get; set; }

        /// <summary>
        /// discountCost
        /// Custo de desconto
        /// </summary>
        public decimal discountCost { get; set; }

        /// <summary>
        /// amountToBeBilled
        /// Montante a ser faturado
        /// </summary>
        public decimal amountToBeBilled { get; set; }

        /// <summary>
        /// invoicedAmount
        /// Facturado Montante
        /// </summary>
        public string invoiceNumber { get; set; }

        /// <summary>
        /// invoicedAmount
        /// Facturado Montante
        /// </summary>
        public decimal invoicedAmount { get; set; }

        /// <summary>
        /// datePaid
        /// Data Pagamento
        /// </summary>
        public DateTime? datePaid { get; set; }

        /// <summary>
        /// referredBy
        /// Referido por
        /// </summary>
        public string referredBy { get; set; }

        /// <summary>
        /// Commission Amount
        /// Comissão Montante
        /// </summary>
        public decimal commissionAmount { get; set; }

        /// <summary>
        /// comissionDate
        /// comissão Data
        /// </summary>
        public DateTime? comissionDate { get; set; }

        /// <summary>
        /// partsNeeded
        /// Peças Necessárias
        /// </summary>
        public int partsNeeded { get; set; }

        /// <summary>
        /// partsAmount
        /// Partes Montante
        /// </summary>
        public decimal partsAmount { get; set; }

        public int? id_old { get; set; }

        /// <summary>
        /// city
        /// Cidade
        /// </summary> 
        public virtual City city { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem do serviço
        /// </summary> 
        public virtual ServiceOrder serviceOrder { get; set; }
    }
}
