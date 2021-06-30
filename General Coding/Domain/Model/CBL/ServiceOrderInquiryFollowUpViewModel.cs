using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderInquiryFollowUpViewModel
    {
        /// <summary>
        /// serviceOrder_id ID
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
        /// Cause Not Sent
        /// Causar não enviada
        /// </summary>
        [Display(Name = "Cause Not Sent")] 
        public string causeNotSent { get; set; }

        /// <summary>
        /// Sent Some where Else Where
        /// Enviada em outro lugar onde...
        /// </summary>
        [Display(Name = "Sent Some where Else Where")]
        public string sentSomewhereElseWhere { get; set; }

        /// <summary>
        /// Comments
        /// Comentários
        /// </summary>
        [Display(Name = "Comments")]
        public string comments { get; set; }

        /// <summary>
        /// dateComplete
        /// Data Completa
        /// </summary>
        [Display(Name = "Date Complete")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
        public DateTime? dateComplete { get; set; }

        /// <summary>
        /// userFollowUp_id
        /// Usuário Acompanhamento ID
        /// </summary>
        [Display(Name = "User FollowUp Id")]
        public string userFollowUp_id { get; set; }

        //public virtual ServiceOrderViewModel serviceOrder { get; set; }



        public static implicit operator ServiceOrderInquiryFollowUpViewModel(ServiceOrderInquiryFollowUp obj)
        {
            if (obj != null)
            {
                return new ServiceOrderInquiryFollowUpViewModel
                {
                   causeNotSent = obj.causeNotSent,
                   comments = obj.comments,
                   dateComplete = obj.dateComplete,
                   dateRegistration = obj.dateRegistration,
                   sentSomewhereElseWhere = obj.sentSomewhereElseWhere,
                   serviceOrder_id = obj.serviceOrder_id,
                   userFollowUp_id = obj.userFollowUp_id,
                   userRegistration_id = obj.userRegistration_id

                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator ServiceOrderInquiryFollowUp(ServiceOrderInquiryFollowUpViewModel obj)
        {
            if (obj != null)
            {
                return new ServiceOrderInquiryFollowUp
                {

                    causeNotSent = obj.causeNotSent,
                    comments = obj.comments,
                    dateComplete = obj.dateComplete,
                    dateRegistration = obj.dateRegistration,
                    sentSomewhereElseWhere = obj.sentSomewhereElseWhere,
                    serviceOrder_id = obj.serviceOrder_id,
                    userFollowUp_id = obj.userFollowUp_id,
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
