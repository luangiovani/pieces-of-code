using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class CustomerContactViewModel
    {
        public CustomerContactViewModel()
        {
            contact = new ContactViewModel();
        }

        [Key]
        [Display(Name = "Customer Contact ID")]
        public int customerContact_id { get; set; }

        [Key]
        [Display(Name = "Customer ID")]
        public int customer_id { get; set; }

        [Key]
        [Display(Name = "Contact ID")]
        public int contact_id { get; set; }
       
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

        public virtual ContactViewModel contact { get; set; }


        public static implicit operator CustomerContactViewModel(CustomerContact obj)
        {
            if (obj != null)
            {
                return new CustomerContactViewModel
                {
                    contact = obj.contact,
                    contact_id = obj.contact_id,
                    customer_id = obj.customer_id,
                    customerContact_id = obj.customerContact_id,
                    dateRegistration = obj.dateRegistration,
                    userRegistration_id = obj.userRegistration_id

                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator CustomerContact(CustomerContactViewModel obj)
        {
            if (obj != null)
            {
                return new CustomerContact
                {
                    contact = obj.contact,
                    contact_id = obj.contact_id,
                    customer_id = obj.customer_id,
                    customerContact_id = obj.customerContact_id,
                    dateRegistration = obj.dateRegistration,
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
