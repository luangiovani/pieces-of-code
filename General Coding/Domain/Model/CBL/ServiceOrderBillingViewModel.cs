using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderBillingViewModel
    {
        /// <summary>
        /// serviceOrder_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Service Order ID")]
        public decimal serviceOrder_id { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
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

        /// <summary>
        /// billingAddress
        /// Endereço de cobrança
        /// </summary>
        [Display(Name = "Address")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string billingAddress { get; set; }

        /// <summary>
        /// billingCompany
        /// Faturamento da Empresa
        /// </summary>
        [Display(Name = "Company")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string billingCompany { get; set; }

        /// <summary>
        /// billingCity_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        [Display(Name = "City")]
        public int? billingCity_id { get; set; }

        /// <summary>
        /// billingPostalZipCode
        /// Faturamento Postal, Código Postal
        /// </summary>
        [Display(Name = "Postal ZipCode")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string billingPostalZipCode { get; set; }

        /// <summary>
        /// billingDistrict
        /// Distrito de faturamento
        /// </summary>
        [Display(Name = "District")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string billingDistrict { get; set; }

        /// <summary>
        /// paymentMethod
        /// Método de pagamento
        /// </summary>
        [Display(Name = "Payment Method")]
        public string paymentMethod { get; set; }

        /// <summary>
        /// freight
        /// Método de pagamento
        /// </summary>
        [Display(Name = "Freight")]
        public string freight { get; set; }

        /// <summary>
        /// creditCardNumber
        /// Número do Cartão de Crédito
        /// </summary>
        [Display(Name = "# Credit Card")]
        public string creditCardNumber { get; set; }

        /// <summary>
        /// creditCardNumber
        /// Número do Cartão de Crédito
        /// </summary>
        [Display(Name = "Credit Card Name")]
        public string nameCreditCard { get; set; }

        /// <summary>
        /// expireCreditCard
        /// Cartão de crédito expirado
        /// </summary>
        [Display(Name = "Expire Credit Card")]
        public string expireCreditCard { get; set; }

        /// <summary>
        /// originalQuotedAmount
        /// VALOR ORIGINAL Citado
        /// </summary>
        [Display(Name = "Original Amount")]
        public decimal originalQuotedAmount { get; set; }

        /// <summary>
        /// discountCost
        /// Custo de desconto
        /// </summary>
        [Display(Name = "Discount Cost")]
        public decimal discountCost { get; set; }

        /// <summary>
        /// amountToBeBilled
        /// Montante a ser faturado
        /// </summary>
        [Display(Name = "Amount To Be Billed")]
        public decimal amountToBeBilled { get; set; }

        /// <summary>
        /// invoiceNumber
        /// Número de fatura
        /// </summary>
        [Display(Name = "Invoice #")]
        public string invoiceNumber { get; set; }

        /// <summary>
        /// invoicedAmount
        /// Facturado Montante
        /// </summary>
        [Display(Name = "Invoiced Amount")]
        public decimal invoicedAmount { get; set; }

        /// <summary>
        /// datePaid
        /// Data Pagamento
        /// </summary>
        [Display(Name = "Date Paid")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? datePaid { get; set; }

        /// <summary>
        /// referredBy
        /// Referido por
        /// </summary>
        [Display(Name = "Referred By")]
        public string referredBy { get; set; }

        /// <summary>
        /// Commission Amount
        /// Comissão Montante
        /// </summary>
        [Display(Name = "Commission Amount")]
        public decimal commissionAmount { get; set; }

        /// <summary>
        /// comissionDate
        /// comissão Data
        /// </summary>
        [Display(Name = "Commission Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? comissionDate { get; set; }

        /// <summary>
        /// partsNeeded
        /// Peças Necessárias
        /// </summary>
        [Display(Name = "Parts Needed")]
        public int partsNeeded { get; set; }

        /// <summary>
        /// partsAmount
        /// Partes Montante
        /// </summary>
        [Display(Name = "Parts Amount")]
        public decimal partsAmount { get; set; }

        /// <summary>
        /// city
        /// Cidade
        /// </summary>       
        [Display(Name = "City")]
        public virtual CityViewModel city { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem do serviço
        /// </summary>  
        //public virtual ServiceOrderViewModel serviceOrder { get; set; }


        public static implicit operator ServiceOrderBillingViewModel(ServiceOrderBilling obj)
        {
            if (obj != null)
            {
                return new ServiceOrderBillingViewModel
                {
                    amountToBeBilled = obj.amountToBeBilled,
                    billingAddress = obj.billingAddress,
                    billingCity_id = obj.billingCity_id,
                    billingCompany = obj.billingCompany,
                    billingDistrict = obj.billingDistrict,
                    billingPostalZipCode = obj.billingPostalZipCode,
                    city = obj.city,
                    comissionDate = obj.comissionDate,
                    commissionAmount = obj.commissionAmount,
                    creditCardNumber = obj.creditCardNumber,
                    datePaid = obj.datePaid,
                    dateRegistration = obj.dateRegistration,
                    discountCost = obj.discountCost,
                    expireCreditCard = obj.expireCreditCard,
                    invoicedAmount = obj.invoicedAmount,
                    invoiceNumber = obj.invoiceNumber,
                    nameCreditCard = obj.nameCreditCard,
                    originalQuotedAmount = obj.originalQuotedAmount,
                    partsAmount = obj.partsAmount,
                    partsNeeded = obj.partsNeeded,
                    paymentMethod = obj.paymentMethod,
                    freight = obj.freight,
                    referredBy = obj.referredBy,
                    serviceOrder_id = obj.serviceOrder_id,
                    userRegistration_id = obj.userRegistration_id


                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator ServiceOrderBilling(ServiceOrderBillingViewModel obj)
        {
            if (obj != null)
            {
                return new ServiceOrderBilling
                {

                    amountToBeBilled = obj.amountToBeBilled,
                    billingAddress = obj.billingAddress,
                    billingCity_id = obj.billingCity_id,
                    billingCompany = obj.billingCompany,
                    billingDistrict = obj.billingDistrict,
                    billingPostalZipCode = obj.billingPostalZipCode,
                    city = obj.city,
                    comissionDate = obj.comissionDate,
                    commissionAmount = obj.commissionAmount,
                    creditCardNumber = obj.creditCardNumber,
                    datePaid = obj.datePaid,
                    dateRegistration = obj.dateRegistration,
                    discountCost = obj.discountCost,
                    expireCreditCard = obj.expireCreditCard,
                    invoicedAmount = obj.invoicedAmount,
                    invoiceNumber = obj.invoiceNumber,
                    nameCreditCard = obj.nameCreditCard,
                    originalQuotedAmount = obj.originalQuotedAmount,
                    partsAmount = obj.partsAmount,
                    partsNeeded = obj.partsNeeded,
                    paymentMethod = obj.paymentMethod,
                    referredBy = obj.referredBy,
                    serviceOrder_id = obj.serviceOrder_id,
                    userRegistration_id = obj.userRegistration_id


                };
            }
            else
            {
                return null;
            }
        }
    }
}
