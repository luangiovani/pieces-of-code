using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderRecoveryFollowUpViewModel
    {
        /// <summary>
        /// Service Order ID
        /// Id interno que será utilizado nos relacionamentos
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
        [Display(Name = "User Registration ID")]
        public string userRegistration_id { get; set; }

        /// <summary>
        /// Media Status
        /// Estado de mídia
        /// </summary>
        [Display(Name = "Media Status")]
        public string mediaStatus { get; set; }

        /// <summary>
        /// rateOurService
        /// Taxa do Nosso Serviço
        /// </summary>
        [Display(Name = "Rate Our Service")]
        public string rateOurService { get; set; }

        /// <summary>
        /// would Be Reference
        /// Seria referenciado
        /// </summary>
        [Display(Name = "Would Be Reference")]
        public string wouldBeReference { get; set; }

        /// <summary>
        /// Send Letter Reference
        /// Enviar Carta de Referência
        /// </summary>
        [Display(Name = "Send Letter Reference")]
        public string sendLetterReference { get; set; }

        /// <summary>
        /// Date Complete
        /// Data completa
        /// </summary>
        [Display(Name = "Date Complete")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dateComplete { get; set; }

        /// <summary>
        /// IntroFaxed
        /// Introdução de fax em...
        /// </summary>
        [Display(Name = "Intro Faxed")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? introFaxed { get; set; }

        /// <summary>
        /// emailSent
        /// E-mail enviado
        /// </summary>
        [Display(Name = "Email Sent")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? emailSent { get; set; }

        /// <summary>
        /// comments
        /// Comentários
        /// </summary>
        [Display(Name = "Comments")]
        [Required(ErrorMessage="The filed Comments is required")]
        [StringLength(2000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string comments { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>
        //public virtual ServiceOrderViewModel serviceOrder { get; set; }



        public static implicit operator ServiceOrderRecoveryFollowUpViewModel(ServiceOrderRecoveryFollowUp obj)
        {
            if (obj != null)
            {
                return new ServiceOrderRecoveryFollowUpViewModel
                {
                    comments = obj.comments,
                    dateComplete = obj.dateComplete,
                    dateRegistration = obj.dateRegistration,
                    emailSent = obj.emailSent,
                    introFaxed = obj.introFaxed,
                    mediaStatus = obj.mediaStatus,
                    rateOurService = obj.rateOurService,
                    sendLetterReference = obj.sendLetterReference,
                    serviceOrder_id = obj.serviceOrder_id,
                    userRegistration_id = obj.userRegistration_id

                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator ServiceOrderRecoveryFollowUp(ServiceOrderRecoveryFollowUpViewModel obj)
        {
            if (obj != null)
            {
                return new ServiceOrderRecoveryFollowUp
                {

                    comments = obj.comments,
                    dateComplete = obj.dateComplete,
                    dateRegistration = obj.dateRegistration,
                    emailSent = obj.emailSent,
                    introFaxed = obj.introFaxed,
                    mediaStatus = obj.mediaStatus,
                    rateOurService = obj.rateOurService,
                    sendLetterReference = obj.sendLetterReference,
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
