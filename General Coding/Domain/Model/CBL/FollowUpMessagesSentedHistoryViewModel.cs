using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class FollowUpMessagesSentedHistoryViewModel
    {
        /// <summary>
        /// ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "ID")]
        public int followUpMessagesSentedHistory_id { get; set; }

        [Display(Name = "# Order")]
        [Required(ErrorMessage = "Service Order Number Is Required")]
        public decimal serviceOrder_id { get; set; }

        [Display(Name = "Date to Send")]
        [Required(ErrorMessage = "Date to Send email is required")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateToSend { get; set; }

        [Display(Name = "From Emails")]
        [Required(ErrorMessage = "From Emails is required")]
        public string fromEmails { get; set; }

        [Display(Name = "To Emails")]
        [Required(ErrorMessage = "To Emails is required")]
        public string toEmails { get; set; }

        [Display(Name = "BCC Emails")]
        public string bccEmails { get; set; }

        [Display(Name = "CC Emails")]
        public string ccEmails { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject is required")]
        public string subject { get; set; }

        [Display(Name = "Body")]
        [Required(ErrorMessage = "Body is required")]
        public string textBody { get; set; }

        [Display(Name = "Date Update")]
        [Required(ErrorMessage = "Update is required")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateUpdate { get; set; }

        [Display(Name = "User Update")]
        [Required(ErrorMessage = "User Update is required")]
        public string userUpdate_id { get; set; }

        [Display(Name = "Date Registration")]
        [Required(ErrorMessage = "Date Registration is required")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateRegistration { get; set; }

        [Display(Name = "User Registration")]
        [Required(ErrorMessage = "User Registration is required")]
        public string userRegistration_id { get; set; }

        [Display(Name = "Date Sented")]
        [Required(ErrorMessage = "Date Sented email is required")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dateSented { get; set; }

        [Display(Name = "User Sented")]
        [Required(ErrorMessage = "User Sented is required")]
        public string userSented_id { get; set; }

        [Display(Name = "Date Submit")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? dateSubmit { get; set; }

        public static implicit operator FollowUpMessagesSentedHistory(FollowUpMessagesSentedHistoryViewModel obj)
        {
            return new FollowUpMessagesSentedHistory
            {
                followUpMessagesSentedHistory_id = obj.followUpMessagesSentedHistory_id,
                serviceOrder_id = obj.serviceOrder_id,
                dateToSend = obj.dateToSend,
                fromEmails = obj.fromEmails,
                toEmails = obj.toEmails,
                bccEmails = obj.bccEmails,
                ccEmails = obj.ccEmails,
                subject = obj.subject,
                textBody = obj.textBody,
                dateUpdate = obj.dateUpdate,
                userUpdate_id = obj.userUpdate_id,
                dateRegistration = obj.dateRegistration,
                userRegistration_id = obj.userRegistration_id,
                //dateSented                         = obj.dateSented            ,
                userSented_id = obj.userSented_id,

            };
        }

        public static implicit operator FollowUpMessagesSentedHistoryViewModel(FollowUpMessagesSentedHistory obj)
        {
            return new FollowUpMessagesSentedHistoryViewModel
            {
                followUpMessagesSentedHistory_id = obj.followUpMessagesSentedHistory_id,
                serviceOrder_id = obj.serviceOrder_id,
                dateToSend = obj.dateToSend,
                fromEmails = obj.fromEmails,
                toEmails = obj.toEmails,
                bccEmails = obj.bccEmails,
                ccEmails = obj.ccEmails,
                subject = obj.subject,
                textBody = obj.textBody,
                dateUpdate = obj.dateUpdate,
                userUpdate_id = obj.userUpdate_id,
                dateRegistration = obj.dateRegistration,
                userRegistration_id = obj.userRegistration_id,
                //dateSented                         = obj.dateSented            ,
                userSented_id = obj.userSented_id,

            };
        }
    }
}
