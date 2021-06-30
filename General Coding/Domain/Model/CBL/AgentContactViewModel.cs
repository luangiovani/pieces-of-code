using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class AgentContactViewModel
    {
        [Key]
        [Display(Name = "Agent Contact ID")]
        public int agentContact_id { get; set; }

        [Display(Name = "Agent ID")]
        public int agent_id { get; set; }

        [Key]
        [Display(Name = "Contact ID")]
        public int contact_id { get; set; }


        /// <summary>
        /// Password
        /// Senha do cliente
        /// </summary>
        [StringLength(20, ErrorMessage = "The field {0} must contain at least {2} and at most {1} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        /// <summary>
        /// Email
        /// Email do cliente
        /// </summary>
        [Required(ErrorMessage = "E-Mail is Required")]
        [Display(Name = "E-Mail")]
        /*[Remote("doesUserNameExist", "ServiceOrder", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.", AdditionalFields = "Id")]   */
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string email { get; set; }

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

        public virtual AgentViewModel agent { get; set; }

        public virtual ContactViewModel contact { get; set; }



        public string tokenCode { get; set; }
        public bool ativo { get; set; }


        public static implicit operator AgentContactViewModel(AgentContacts obj)
        {
            if (obj != null)
            {
                return new AgentContactViewModel
                {
                    agent = obj.agent,
                    agent_id = obj.agent_id,
                    agentContact_id = obj.agentContact_id,
                    contact = obj.contact,
                    contact_id = obj.contact_id,
                    dateRegistration = obj.dateRegistration,
                    email = obj.email,
                    password = obj.password,
                    userRegistration_id = obj.userRegistration_id,
                    tokenCode = obj.tokenCode,
                    ativo = obj.ativo
                };
            }
            else
            {
                return null;
            }
        }

        public static implicit operator AgentContacts(AgentContactViewModel obj)
        {
            if (obj != null)
            {
                return new AgentContacts
                {
                    agent = obj.agent,
                    agent_id = obj.agent_id,
                    agentContact_id = obj.agentContact_id,
                    contact = obj.contact,
                    contact_id = obj.contact_id,
                    dateRegistration = obj.dateRegistration,
                    email = obj.email,
                    password = obj.password,
                    userRegistration_id = obj.userRegistration_id,
                    tokenCode = obj.tokenCode,
                    ativo = obj.ativo
                };
            }
            else
            {
                return null;
            }
        }
    }
}
