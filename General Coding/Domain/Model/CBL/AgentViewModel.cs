using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Mapeamento da Entidade Agents (Parceiros da CBL), para gravação na tabela Agents no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class AgentViewModel
    {
        /// <summary>
        /// Agent ID
        /// Id interno do parceiro que será utilizado nos relacionamentos e controle único
        /// </summary>
        [Key]
        [Display(Name = "Agent ID")]
        public int agent_id { get; set; }

        /// <summary>
        /// City ID
        /// Id interno da cidade do endereço do parceiro
        /// </summary>
        [Required(ErrorMessage="City is Required for Agent.")]
        [Display(Name = "Agent City Address")]
        public int city_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do parceiro
        /// </summary>
        [Required(ErrorMessage = "Name is Required for Agent.")]
        [Display(Name = "Agent Name")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// Company Name
        /// Nome da empresa do parceiro
        /// </summary>
        [Display(Name = "Agent Company Name")]
        [StringLength(800, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string companyName { get; set; }

        /// <summary>
        /// Address
        /// Endereço completo do parceiro (logradouro, número e complemento)
        /// </summary>
        [Display(Name = "Agent Address(Complete with number)")]
        [StringLength(400, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string address { get; set; }

        /// <summary>
        /// Postal / Zip
        /// CEP ou outro código postal do parceiro
        /// </summary>
        [Display(Name = "Agent Postal Zip/Code")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string postalZipCode { get; set; }

        /// <summary>
        /// Web Site
        /// Url do endereço eletrônico do parceiro
        /// </summary>
        [Display(Name = "Agent URL Address website")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string website { get; set; }

        /// <summary>
        /// User
        /// Usuario de acesso do parceiro
        /// </summary>
        [Display(Name = "Agent UserName")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string userName { get; set; }

        /// <summary>
        /// Password
        /// Senha de acesso do parceiro
        /// </summary>
        [Display(Name = "Agent Password")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string password { get; set; }

        /// <summary>
        /// Forwarded To
        /// Encaminhamento do parceiro
        /// </summary>
        [Display(Name = "Forwarded To")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string forwardedTo { get; set; }

        /// <summary>
        /// Demarcation #
        /// Número de demarcação do parceiro
        /// </summary>
        [Display(Name = "Demarcation #")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string demarcation_no { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste parceiro
        /// </summary>
        [Display(Name = "Register Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dateRegistration { get; set; }

        /// <summary>
        /// Date Added
        /// Data de adição da Nota
        /// </summary>
        [Display(Name = "Date Added")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dateProgramInformation { get; set; }

        /// <summary>
        /// Program Information type
        /// Tipo do programa de parceria no cadastro da nota
        /// </summary>
        [Display(Name = "Program Information Type")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string programInformationType { get; set; }

        /// <summary>
        /// Program Information notes
        /// Notas do programa de parceria
        /// </summary>
        [Display(Name = "Program Information Notes")]
        [StringLength(2000, ErrorMessage = "The field {0} should not exceed {1} characters.")] 
        public string programInformationNotes { get; set; }

        /// <summary>
        /// City
        /// Cidade do Endereço do Parceiro, retorna Estado e País, DDD e DDI
        /// </summary>
        [Display(Name = "City")]
        public virtual CityViewModel city { get; set; }

        /// <summary>
        /// Contacts
        /// Lista de Contatos do Parceiro
        /// </summary>
        [Display(Name = "Contacts")]
        public virtual ICollection<ContactViewModel> contacts { get; set; }

        /// <summary>
        /// Program Information notes
        /// Notas do programa de parceria
        /// </summary>
        [Display(Name = "Status")]
        public string active { get; set; }

        /// <summary>
        /// Commissions
        /// Lista de Comissões do Agente por O.S.
        /// </summary>
        //[Display(Name = "Agent Commissions")]
        //public virtual ICollection<AgentCommissionsViewModel> agentCommissions { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
        /// 


        public string userRegistration_id { get; set; }

        public static implicit operator AgentViewModel(Agent obj)
        {
            if (obj != null)
            {
                return new AgentViewModel
                {
                    address = obj.address,
                    agent_id = obj.agent_id,
                    city = obj.city,
                    city_id = obj.city_id,
                    companyName = obj.companyName,
                    //contacts = (ContactViewModel)obj.contacts,
                    
                    dateProgramInformation = obj.dateProgramInformation,
                    //dateRegistration = obj.dateRegistration,
                    demarcation_no = obj.demarcation_no,
                    forwardedTo = obj.forwardedTo,
                    name = obj.name,
                    password = obj.password,
                    postalZipCode= obj.postalZipCode,
                    programInformationNotes = obj.programInformationNotes,
                    programInformationType = obj.programInformationType,
                    userName = obj.userName,
                    userRegistration_id = obj.userRegistration_id,
                    website = obj.website

                };
            }
            else
            {
                return null;
            }
        }

        public static implicit operator Agent(AgentViewModel obj)
        {
            if (obj != null)
            {
                return new Agent
                {
                    address = obj.address,
                    agent_id = obj.agent_id,
                    city = obj.city,
                    city_id = obj.city_id,
                    companyName = obj.companyName,
                    //contacts = (ICollection<AgentContacts>)obj.contacts,
                    dateProgramInformation = obj.dateProgramInformation,
                    //dateRegistration = obj.dateRegistration,
                    demarcation_no = obj.demarcation_no,
                    forwardedTo = obj.forwardedTo,
                    name = obj.name,
                    password = obj.password,
                    postalZipCode = obj.postalZipCode,
                    programInformationNotes = obj.programInformationNotes,
                    programInformationType = obj.programInformationType,
                    userName = obj.userName,
                    userRegistration_id = obj.userRegistration_id,
                    website = obj.website

                };
            }
            else
            {
                return null;
            }
        }
    }
}
