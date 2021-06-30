using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class FollowUpMessagesToSendViewModel
    {
        /// <summary>
        /// ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "ID")]
        public int followUpMessagesToSend_id { get; set; }

        [Display(Name = "# Order")]
        [Required(ErrorMessage="Service Order Number Is Required")]
        [DisplayFormat(DataFormatString = "{0:#########}")]
        public decimal serviceOrder_id { get; set; }

        [Display(Name = "O.S. Series")]
        [ScaffoldColumn(false)]
        public string OS_Series { get; set; }

        [Display(Name = "O.S. Status")]
        [ScaffoldColumn(false)]
        public string OS_Status { get; set; }

        [Display(Name = "Customer")]
        [ScaffoldColumn(false)]
        public string Customer { get; set; }

        [Display(Name = "Date to Send")]
        [Required(ErrorMessage = "Date to Send email is required")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
        public DateTime dateToSend { get; set; }

        [Display(Name = "From Emails -> For More Than One, separeted by ;")]
        [Required(ErrorMessage = "From Emails is required")]
        public string fromEmails { get; set; }

        [Display(Name = "To Emails -> For More Than One, separeted by ;")]
        [Required(ErrorMessage = "To Emails is required")]
        public string toEmails { get; set; }

        [Display(Name = "BCC Emails -> For More Than One, separeted by ;")]
        public string bccEmails { get; set; }

        [Display(Name = "CC Emails -> For More Than One, separeted by ;")]
        public string ccEmails { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject is required")]
        public string subject { get; set; }

        [Display(Name = "Body Message")]
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

    }
}
