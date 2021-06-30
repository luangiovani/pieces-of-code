using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderContactViewModel
    {
        /// <summary>
        /// serviceOrderContact_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Service Order Contact ID")]
        public int serviceOrderContact_id { get; set; }

        /// <summary>
        /// serviceOrder_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "service Order ID")]
        public decimal serviceOrder_id { get; set; }

        /// <summary>
        /// contact_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Contact ID")]
        public int contact_id { get; set; }

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

        //public virtual ServiceOrderViewModel serviceOrder { get; set; }

        public virtual ContactViewModel contact { get; set; }



        public static implicit operator ServiceOrderContactViewModel(ServiceOrderContact obj)
        {
            if (obj != null)
            {
                return new ServiceOrderContactViewModel
                {
                    contact = obj.contact,
                    contact_id = obj.contact_id,
                    dateRegistration = obj.dateRegistration,
                    serviceOrder_id = obj.serviceOrder_id,
                    serviceOrderContact_id = obj.serviceOrderContact_id,
                    userRegistration_id = obj.userRegistration_id


                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator ServiceOrderContact(ServiceOrderContactViewModel obj)
        {
            if (obj != null)
            {
                return new ServiceOrderContact
                {
                    contact = obj.contact,
                    contact_id = obj.contact_id,
                    dateRegistration = obj.dateRegistration,
                    serviceOrder_id = obj.serviceOrder_id,
                    serviceOrderContact_id = obj.serviceOrderContact_id,
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
