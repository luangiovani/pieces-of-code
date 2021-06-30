using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderShippingViewModel
    {
        /// <summary>
        /// ServiceOrder ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Service Order ID")]
        public decimal serviceOrder_id { get; set; }

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
        [Display(Name = "User Registration ID")]
        public string userRegistration_id { get; set; }

        /// <summary>
        /// shipCompany
        /// Recipiente da Empresa
        /// </summary>   
        [Display(Name = "Company")]
        public string shipCompany { get; set; }

        /// <summary>
        /// shipAddress
        /// Endereço do Recipiente 
        /// </summary>   
        [Display(Name = "Address")]
        public string shipAddress { get; set; }

        /// <summary>
        /// shipCity_id
        /// Recipiente da Cidade
        /// </summary>   
        [Display(Name = "City")]
        public int? shipCity_id { get; set; }

        /// <summary>
        /// ship Postal ZipCode
        /// Código Postal
        /// </summary>   
        [Display(Name = "Postal ZipCode")]
        public string shipPostalZipCode { get; set; }

        /// <summary>
        /// Ship District
        /// Distrito de recipiente
        /// </summary>   
        [Display(Name = "District")]
        public string shipDistrict { get; set; }

        /// <summary>
        /// Ship Contact
        /// Distrito de Contato
        /// </summary>   
        [Display(Name = "Contact")]
        public string shipContact { get; set; }

        /// <summary>
        /// Ship Telephone
        /// Distrito de telefone
        /// </summary>   
        [Display(Name = "Telephone")]
        public string shipTelephone { get; set; }

        /// <summary>
        /// Ship Email
        /// Distrito de e-mail
        /// </summary>   
        [Display(Name = "Email")]
        public string shipEmail { get; set; }

        /// <summary>
        /// Ship Method
        /// Método de envio, Método de navio
        /// </summary>   
        [Display(Name = "Method")]
        public string shipMethod { get; set; }

        /// <summary>
        /// Ship Account Number
        /// Número de Conta navio
        /// </summary>   
        [Display(Name = "Account #")]
        public string shipAccountNumber { get; set; }

        /// <summary>
        /// Ship Tracking Number
        /// Número de Rastreamento
        /// </summary>   
        [Display(Name = "Tracking #")]
        public string shipTrackingNumber { get; set; }

        /// <summary>
        /// Ship Media Status
        /// Estado de Envio de mídia 
        /// </summary>   
        [Display(Name = "Media Status")]
        public string shipMediaStatus { get; set; }

        /// <summary>
        /// Ship Media Date 
        /// Envio de mídia Taxa
        /// </summary> 
        [Display(Name = "Media Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? shipMediaDate { get; set; }

        /// <summary>
        /// Ship Data Shipped
        /// Dados do navio Enviado
        /// </summary> 
        [Display(Name = "Data Shipped")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? shipDataShipped { get; set; }

        /// <summary>
        /// Ship Instructions
        /// Instruções de navio/ instruções de envio
        /// </summary> 
        [Display(Name = "Instructions")]
        public string shipInstructions { get; set; }

        /// <summary>
        /// Ship Contents
        /// Índice de navios / índice de envio
        /// </summary> 
        /// [Display(Name = "Contents")]
        [Display(Name = "Shipping Notes")]
        public string shipContents { get; set; }

        /// <summary>
        /// Ship Pre Recovery Info
        /// Envio de Pré Informações Recuperação
        /// </summary> 
        [Display(Name = "Pre Recovery Info")]
        public string shipPreRecoveryInfo { get; set; }

        /// <summary>
        /// Service Order
        /// Ordem de serviço
        /// </summary> 
        //public virtual ServiceOrderViewModel serviceOrder { get; set; }

        /// <summary>
        /// City
        /// Cidade
        /// </summary> 
        [Display(Name="City")]
        public virtual CityViewModel city { get; set; }



        public static implicit operator ServiceOrderShippingViewModel(ServiceOrderShipping obj)
        {
            if (obj != null)
            {
                return new ServiceOrderShippingViewModel
                {
                    city = obj.city,
                    dateRegistration = obj.dateRegistration,
                    serviceOrder_id = obj.serviceOrder_id,
                    shipAccountNumber = obj.shipAccountNumber,
                    shipAddress = obj.shipAddress,
                    shipCity_id = obj.shipCity_id,
                    shipCompany = obj.shipCompany,
                    shipContact = obj.shipContact,
                    shipContents = obj.shipContents,
                    shipDataShipped = obj.shipDataShipped,
                    shipDistrict = obj.shipDistrict,
                    shipEmail = obj.shipEmail,
                    shipInstructions = obj.shipInstructions,
                    shipMediaDate = obj.shipMediaDate,
                    shipMediaStatus = obj.shipMediaStatus,
                    shipMethod = obj.shipMethod,
                    shipPostalZipCode = obj.shipPostalZipCode,
                    shipPreRecoveryInfo = obj.shipPreRecoveryInfo,
                    shipTelephone = obj.shipTelephone,
                    shipTrackingNumber = obj.shipTrackingNumber,
                    userRegistration_id = obj.userRegistration_id


                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator ServiceOrderShipping(ServiceOrderShippingViewModel obj)
        {
            if (obj != null)
            {
                return new ServiceOrderShipping
                {

                    city = obj.city,
                    dateRegistration = obj.dateRegistration,
                    serviceOrder_id = obj.serviceOrder_id,
                    shipAccountNumber = obj.shipAccountNumber,
                    shipAddress = obj.shipAddress,
                    shipCity_id = obj.shipCity_id,
                    shipCompany = obj.shipCompany,
                    shipContact = obj.shipContact,
                    shipContents = obj.shipContents,
                    shipDataShipped = obj.shipDataShipped,
                    shipDistrict = obj.shipDistrict,
                    shipEmail = obj.shipEmail,
                    shipInstructions = obj.shipInstructions,
                    shipMediaDate = obj.shipMediaDate,
                    shipMediaStatus = obj.shipMediaStatus,
                    shipMethod = obj.shipMethod,
                    shipPostalZipCode = obj.shipPostalZipCode,
                    shipPreRecoveryInfo = obj.shipPreRecoveryInfo,
                    shipTelephone = obj.shipTelephone,
                    shipTrackingNumber = obj.shipTrackingNumber,
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
