using Framework.Database.Entity.CBL;
using Framework.Domain.Utils;
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
    /// Mapeamento da Entidade Contact (Contatos), para gravação na tabela Contacts no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class ContactViewModel
    {
        private string telefone;
        private string celular;

        /// <summary>
        /// Contact ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Contact ID")]
        public int contact_id { get; set; }

        /// <summary>
        /// Este vem o ID de quem é o contato
        /// customer_id, agent_id, supplier_id, serviceOrder_id
        /// </summary>
        [ScaffoldColumn(false)]
        public int fromID { get; set; }

        /// <summary>
        /// Propriedade para demonstrar de Quem é este Contato
        /// </summary>
        [ScaffoldColumn(false)]
        public WhoIsContact? whoIsContact { get; set; }

        /// <summary>
        /// Type of Contact
        /// Tipo de contato (Customer, Supplier, Manufacturer, Service Order...)
        /// </summary>
        [Required(ErrorMessage = "Tipo de Contato Obrigatório")]///Type of Contact is Required
        [Display(Name = "Type of Contact")]
        public int typeContact_id { get; set; }

        /// <summary>
        /// Name
        /// Nome da Pessoa de Contato
        /// </summary>
        [Required(ErrorMessage = "Nome do Contato Obrigatório")]//Name(Person) of Contact is required
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// City ID
        /// Id interno da cidade que será utilizado nos relacionamentos
        /// </summary>
        [Display(Name="City")]
        public int? city_id { get; set; }

        /// <summary>
        /// Toll Free
        /// Número de telefone gratuito (0800)
        /// </summary>
        [Display(Name = "Toll Free")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string tollFree { get; set; }

        /// <summary>
        /// Telephone
        /// Telefone de Contato
        /// </summary>
        [Display(Name = "Telephone")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.", MinimumLength = 10)]
        public string telephone 
        { 
            get
            {
                if (telefone == null)
                {
                    telefone = String.Empty;
                }
                telefone = telefone.Replace(" ", "");
                return telefone;
            }
            set { telefone = value; }
        }

        /// <summary>
        /// Ext
        /// Ramal do telefone quando houver
        /// </summary>
        [Display(Name = "Extension")]
        [StringLength(6, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string extension { get; set; }

        /// <summary>
        /// Fax
        /// Número do Fax quando houver
        /// </summary>
        [Display(Name = "Fax")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string fax { get; set; }

        /// <summary>
        /// Mobile
        /// Número de Celular do Contato quando houver
        /// </summary>
        [Display(Name = "Mobile")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.", MinimumLength = 10)]
        public string mobile {
            get
            {
                if (celular == null)
                {
                    celular = String.Empty;
                }
                celular = celular.Replace(" ", "");
                return celular;
            }
            set { celular = value; }
        }

        /// <summary>
        /// Email
        /// E-mail de contato
        /// </summary>
        [Display(Name = "E-mail")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string email { get; set; }

        /// <summary>
        /// Other
        /// Outros tipos de contatos
        /// </summary>
        [Display(Name = "Other")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string other { get; set; }

        /// <summary>
        /// Notes
        /// Informações adicionais do contato
        /// </summary>
        [Display(Name = "Notes")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string notes { get; set; }

        /// <summary>
        /// Receive Notification
        /// Indicador se este contato recebe emails de notificações do sistema
        /// </summary>
        [Display(Name = "Notifications?")]
        [Required(ErrorMessage = "Please, set if Customer accepts or no receive system notifications (Email, SMS)")]
        public bool receiveNotifications { get; set; }

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

        //[Required(ErrorMessage = "CPF Obrigatório")]
        [CPFValid(ErrorMessage = "CPF Inválido!!!")]
        public string cpf { get; set; }
        //[Required(ErrorMessage = "Endereço Obrigatório")]
        public string address { get; set; }
        //[Required(ErrorMessage = "Número Endereço Obrigatório")]
        public string addressNumbers { get; set; }
        public string addressExtension { get; set; }
        public string district { get; set; }
        //[Required(ErrorMessage = "CEP Obrigatório")]
        public string postalZipCode { get; set; }
        public bool? indPrincipal { get; set; }

        private bool? AcompanharSMS = false;
        public bool indAcompanharSMS {
            get
            {                
                return AcompanharSMS ?? false;
            }
            set { AcompanharSMS = value; }
        }


        /// <summary>
        /// City
        /// Entidade Cidade carregada por lazy loading
        /// </summary>
        [Display(Name = "City")]
        public virtual CityViewModel city { get; set; }

        //[Display(Name = "Agent Contacts")]
        //public virtual ICollection<AgentContactViewModel> agentContacts { get; set; }

        //[Display(Name = "Customer Contacts")]
        //public virtual ICollection<CustomerContactViewModel> customerContacts { get; set; }

        //[Display(Name = "Service Order Contacts")]
        //public virtual ICollection<ServiceOrderContactViewModel> serviceOrderContacts { get; set; }

        //[Display(Name = "Supplier Contacts")]
        //public virtual ICollection<SuppliersContactViewModel> suppliersContacts { get; set; }
        public static implicit operator ContactViewModel(Contact obj)
        {
            if (obj != null)
            {
                return new ContactViewModel
                {
                    city = obj.city,
                    city_id = obj.city_id,
                    contact_id = obj.contact_id,
                    dateRegistration = obj.dateRegistration,
                    email = obj.email,
                    extension = obj.extension,
                    fax = obj.fax,
                    mobile = obj.mobile,
                    name = obj.name,
                    notes = obj.notes,
                    other = obj.other,
                    receiveNotifications = obj.receiveNotifications,
                    telephone = obj.telephone,
                    tollFree = obj.tollFree,
                    typeContact_id = obj.typeContact_id,
                    userRegistration_id = obj.userRegistration_id,
                    cpf  = obj.cpf,
                    address = obj.address,
                    addressNumbers  = obj.addressNumbers,
                    addressExtension  = obj.addressExtension,
                    district  = obj.district,
                    postalZipCode  = obj.postalZipCode,
                    indAcompanharSMS = obj.indAcompanharSMS ?? false,
                    indPrincipal = obj.indPrincipal,


                };
            }
            else
            {
                return null;
            }
        }

        public static implicit operator Contact(ContactViewModel obj)
        {
            if (obj != null)
            {
                return new Contact
                {
                    city = obj.city,
                    city_id = obj.city_id,
                    contact_id = obj.contact_id,
                    dateRegistration = obj.dateRegistration,
                    email = obj.email,
                    extension = obj.extension,
                    fax = obj.fax,
                    mobile = obj.mobile,
                    name = obj.name,
                    notes = obj.notes,
                    other = obj.other,
                    receiveNotifications = obj.receiveNotifications,
                    telephone = obj.telephone,
                    tollFree = obj.tollFree,
                    typeContact_id = obj.typeContact_id,
                    userRegistration_id = obj.userRegistration_id,
                    cpf = obj.cpf,
                    address = obj.address,
                    addressNumbers = obj.addressNumbers,
                    addressExtension = obj.addressExtension,
                    district = obj.district,
                    postalZipCode = obj.postalZipCode,
                    indAcompanharSMS = obj.indAcompanharSMS,
                    indPrincipal = obj.indPrincipal,


                };
            }
            else
            {
                return null;
            }
        }

    }

    public class ContactEditViewModel
    {
        public ContactEditViewModel(ContactViewModel model)
        {
            contact_id  = model.contact_id;
            whoIsContact  = WhoIsContact.Customer;
            typeContact_id  = model.typeContact_id;
            name  = model.name;
            city_id  = model.city_id;
            country_id = model.city != null ? model.city.country_id : 0;
            state_id  = model.city != null ? model.city.state_id : 0;
            tollFree  = model.tollFree;
            telephone  = model.telephone;
            extension  = model.extension;
            fax  = model.fax;
            mobile  = model.mobile;
            email  = model.email;
            other  = model.other;
            notes  = model.notes;
            receiveNotifications = model.receiveNotifications;
            cpf = model.cpf;
            address = model.address;
            addressNumbers = model.addressNumbers;
            addressExtension = model.addressExtension;
            district = model.district;
            postalZipCode = model.postalZipCode;
        }

        public int contact_id { get; set; }
        public WhoIsContact? whoIsContact { get; set; }
        public int typeContact_id { get; set; }
        public string name { get; set; }
        public int? city_id { get; set; }
        public int? country_id { get; set; }
        public int? state_id { get; set; }
        public string tollFree { get; set; }
        public string telephone { get; set; }
        public string extension { get; set; }
        public string fax { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string other { get; set; }
        public string notes { get; set; }
        public bool receiveNotifications { get; set; }

        
        public string cpf { get; set; }
        public string address { get; set; }
        public string addressNumbers { get; set; }
        public string addressExtension { get; set; }
        public string district { get; set; }



        [Required(ErrorMessage = "CEP Obrigatório")]
        public string postalZipCode { get; set; }

        
    }
    public class CPFValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var _Cpf = Convert.ToString(value);

            if (String.IsNullOrEmpty(_Cpf))
                return true;

            return Framework.Domain.Utils.Util.ValidaCPF(_Cpf); // Aqui chamada para sua função de validar CPF
        }
    }
}
