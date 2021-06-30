using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class FollowUpMessagesBodyViewModel
    {
        /// <summary>
        /// ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "ID")]
        public int followUpMessages_id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string description { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject is required")]
        public string subject { get; set; }

        [Display(Name = "Text Body")]
        [Required(ErrorMessage = "Text Body is required")]
        public string textBody { get; set; }

        public string textBodyAux { get; set; }

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


        public static implicit operator FollowUpMessagesBodyViewModel(FollowUpMessagesBody obj)
        {
            if (obj != null)
            {
                return new FollowUpMessagesBodyViewModel
                {
                    dateRegistration = obj.dateRegistration,
                    dateUpdate = obj.dateUpdate,
                    description = obj.description,
                    followUpMessages_id = obj.followUpMessages_id,
                    name = obj.name,
                    subject = obj.subject,
                    textBody = obj.textBody,
                    userRegistration_id = obj.userRegistration_id,
                    userUpdate_id = obj.userUpdate_id

                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator FollowUpMessagesBody(FollowUpMessagesBodyViewModel obj)
        {
            if (obj != null)
            {
                return new FollowUpMessagesBody
                {

                    dateRegistration = obj.dateRegistration,
                    dateUpdate = obj.dateUpdate,
                    description = obj.description,
                    followUpMessages_id = obj.followUpMessages_id,
                    name = obj.name,
                    subject = obj.subject,
                    textBody = obj.textBody,
                    userRegistration_id = obj.userRegistration_id,
                    userUpdate_id = obj.userUpdate_id


                };
            }
            else
            {
                return null;
            }
        }
    }
}
