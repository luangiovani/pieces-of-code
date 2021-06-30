using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Domain.Utils;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Database.Entity.CBL;

namespace Framework.Domain.Model.CBL
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Mapeamento da Entidade Customer (Cliente), para gravação na tabela Customer no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            contatoNovo = new ContactViewModel();
        }
        /// <summary>
        /// Customer ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name="Customer ID")]
        public int customer_id { get; set; }

        /// <summary>
        /// Email
        /// Email do cliente
        /// </summary>
        [Required(ErrorMessage="E-Mail is Required")]
        [Display(Name = "E-Mail")]
        /*[Remote("doesUserNameExist", "ServiceOrder", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.", AdditionalFields = "Id")]   */    
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string email { get; set; }

        /// <summary>
        /// Name
        /// Nome do cliente
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string name { get; set; }

        /// <summary>
        /// Address
        /// Endereço completo do cliente (logradouro, número e complemento)
        /// </summary>
        [Display(Name = "Address")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string address { get; set; }

        [Display(Name = "Address Number")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string addressNumber { get; set; }

        [Display(Name = "Address Extension")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string addressExtension { get; set; }

        /// <summary>
        /// District
        /// Bairro do cliente
        /// </summary>
        [Display(Name = "District")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string district { get; set; }

        /// <summary>
        /// Postal / Zip
        /// CEP ou outro código postal do cliente
        /// </summary>
        [Display(Name = "Postal Zip/Code")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string postalZipCode { get; set; }

        /// <summary>
        /// Web Site
        /// Url do endereço virtual do cliente
        /// </summary>
        [Display(Name = "Web Site")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string website { get; set; }

        /// <summary>
        /// Date Registration
        /// Data e Hora de Cadastro do Cliente
        /// </summary>
        [Display(Name = "Date Registration")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateRegistration { get; set; }

        public string userRegistration_id { get; set; }

        /// <summary>
        /// Hear Of Us
        /// informação de onde o cliente soube da empresa
        /// </summary>
        [Display(Name = "Hear Of Us")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string hearOfUs { get; set; }

        /// <summary>
        /// Point Of Contact
        /// Motivo do contato do cliente
        /// </summary>
        [Display(Name = "Point Of Contact")]
        [StringLength(150, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string pointOfContact { get; set; }

        /// <summary>
        /// Credit Terms
        /// Termos de crédito concedido ao cliente
        /// </summary>
        [Display(Name = "Credit Terms")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string creditTerms { get; set; }

        /// <summary>
        /// Password
        /// Senha do cliente
        /// </summary>
        [StringLength(20, ErrorMessage = "The field {0} must contain at least {2} and at most {1} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        /// <summary>
        /// Percentage
        /// Percentual aplicado ao cliente
        /// </summary>
        [Display(Name="Percentage")]
        public decimal? percentage { get; set; }

        /// <summary>
        /// Tax # 1
        /// CPF ou CNPJ do cliente quando nacional ou outra informação relacionada Internacional
        /// </summary>
        [Display(Name = "CPF / CNPJ")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string cpfCnpj { get; set; }

        /// <summary>
        /// Tax # 1
        /// CPF  do cliente quando nacional ou outra informação relacionada Internacional
        /// </summary>
        [Display(Name = "CPF")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string cpf { get; set; }

        /// <summary>
        /// Tax # 1
        /// CPF ou CNPJ do cliente quando nacional ou outra informação relacionada Internacional
        /// </summary>
        [Display(Name = "CNPJ")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string cnpj { get; set; }

        /// <summary>
        /// Tax # 2 
        /// RG do cliente ou ID Estadual dele quando internacional, podendo ser Social Number por exemplo
        /// </summary>
        [Display(Name = "RG / IE")]
        [StringLength(20, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string rgIdState { get; set; }

        /// <summary>
        /// Notes
        /// Notas ou observações relacionadas ao cliente
        /// </summary>
        [Display(Name = "Notes")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string notes { get; set; }

        [Display(Name = "Business Type")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string business_type { get; set; }

        public string tokenCode { get; set; }

        public DateTime? dateLastAccessPortal { get; set; }

        public DateTime? dateLastUpdateInformations { get; set; } 

        /// <summary>
        /// Contacts
        /// Lista de Contatos do cliente.
        /// </summary>
        [Display(Name="Contacts")]
        public virtual ICollection<CustomerContactViewModel> contacts { get; set; }

        /// <summary>
        /// Service Orders
        /// Lista de Ordens de Serviço do Cliente
        /// </summary>
        //[Display(Name = "Service Orders")]
        //public virtual ICollection<ServiceOrderViewModel> serviceOrders { get; set; }

        public ICollection<ServiceOrdersOfCustomerViewModel> ordersOfCustomer { get; set; }

        public int QuantityOfOrders { get; set; }

        public string getTelephone()
        {
            if (contacts.Count > 0)
            {
                var conts = contacts.ToList();
                if (conts.Count > 0)
                {
                    string telefone = "";
                    if (conts.Where(c => c.contact.typeContact_id == (int)TypeOfContactEnum.Principal).Count() > 0)
                    {
                        telefone = conts.Where(c => c.contact.typeContact_id == (int)TypeOfContactEnum.Principal).FirstOrDefault().contact.telephone;
                    }
                    else
                    {
                        telefone = conts.FirstOrDefault().contact.telephone;
                    }
                    if (telefone == null)
                    {
                        telefone = String.Empty;
                    }
                    telefone = telefone.Replace(" ", "");
                    return telefone;
                }
                else
                    return "";
            }
            else
                return "";
        }

        public string getMobile()
        {
            if (contacts.Count > 0)
            {
                var conts = contacts.ToList();
                if (conts.Count > 0)
                {
                    string celular = "";
                    if (conts.Where(c => c.contact.typeContact_id == (int)TypeOfContactEnum.Principal).Count() > 0)
                    {
                        celular = conts.Where(c => c.contact.typeContact_id == (int)TypeOfContactEnum.Principal).FirstOrDefault().contact.mobile;
                    }
                    else
                    {
                        celular = conts.FirstOrDefault().contact.mobile;
                    }

                    if (celular == null)
                    {
                        celular = String.Empty;
                    }
                    celular = celular.Replace(" ", "");
                    return celular;
                }
                else
                    return "";
            }
            else
                return "";
        }

        public int? city_id { get; set; }

        /// <summary>
        /// Adicionado para inserção na tela de cadastro quando novo cliente
        /// </summary>
        [ScaffoldColumn(true)]
        public virtual ContactViewModel contatoNovo { get; set; }
        public string tipoContato { get; set; }
        public string telephone { get; set; }


        
        public static implicit operator CustomerViewModel(Customer obj)
        {
            if (obj != null)
            {
                var contatos = obj.contacts;
                var listContatos = new List<CustomerContactViewModel>();
                foreach (var item in contatos)
                {
                    listContatos.Add(item); 
                }
                
                return new CustomerViewModel
                {
                    address = obj.address,
                    addressExtension = obj.addressExtension,
                    addressNumber = obj.addressNumber,
                    business_type = obj.business_type,
                    cnpj = obj.cpfCnpj,
                    contacts = listContatos,
                    //contacts = new List<CustomerContactViewModel>().AddRange(),
                    //(ICollection<CustomerContactViewModel>)(obj.contacts.ToList()),
                    //contacts = obj.contacts.Cast<CustomerContactViewModel>().ToList(),
                    cpf = obj.cpfCnpj,
                    cpfCnpj = obj.cpfCnpj,
                    creditTerms = obj.creditTerms,
                    customer_id = obj.customer_id,
                    dateLastAccessPortal = obj.dateLastAccessPortal,
                    dateLastUpdateInformations = obj.dateLastUpdateInformations,
                    dateRegistration = obj.dateRegistration,
                    district = obj.district,
                    email = obj.email,
                    hearOfUs = obj.hearOfUs,
                    name = obj.name,
                    notes = obj.notes,
                    password = obj.password,
                    percentage = obj.percentage,
                    pointOfContact = obj.pointOfContact,
                    postalZipCode = obj.postalZipCode,
                    rgIdState = obj.rgIdState,
                    tokenCode = obj.tokenCode,
                    userRegistration_id = obj.userRegistration_id,
                    website = obj.website
                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator Customer(CustomerViewModel obj)
        {
            if (obj != null)
            {
                var contatos = obj.contacts;
                var listContatos = new List<CustomerContact>();
                if(contatos != null)
                {                   
                    foreach (var item in contatos)
                    {
                        listContatos.Add(item);
                    }
                }
                else if (obj.contatoNovo != null)
                {
                    //listContatos.Add((CustomerContact)obj.contatoNovo);
                }
                
                return new Customer
                {
                    address = obj.address,
                    addressExtension = obj.addressExtension,
                    addressNumber = obj.addressNumber,
                    business_type = obj.business_type,
                    cpfCnpj = obj.cpfCnpj,
                    contacts = listContatos,
                    creditTerms = obj.creditTerms,
                    customer_id = obj.customer_id,
                    dateLastAccessPortal = obj.dateLastAccessPortal,
                    dateLastUpdateInformations = obj.dateLastUpdateInformations,
                    dateRegistration = obj.dateRegistration,
                    district = obj.district,
                    email = obj.email,
                    hearOfUs = obj.hearOfUs,
                    name = obj.name,
                    notes = obj.notes,
                    password = obj.password,
                    percentage = obj.percentage,
                    pointOfContact = obj.pointOfContact,
                    postalZipCode = obj.postalZipCode,
                    rgIdState = obj.rgIdState,
                    tokenCode = obj.tokenCode,
                    userRegistration_id = obj.userRegistration_id,
                    website = obj.website
                };
            }
            else
            {
                return null;
            }

        }
        //*/
    }

    public class ServiceOrdersOfCustomerViewModel
    {
        [Display(Name = "Service Order #")]
        public decimal serviceOrderId { get; set; }

        [Display(Name = "Date")]
        public DateTime date { get; set; }

        [Display(Name = "Status")]
        public string serviceOrderStatus { get; set; }

        [Display(Name = "Location")]
        public string location { get; set; }

        [Display(Name = "Responsible")]
        public string responsible { get; set; }

        [Display(Name="OS Series")]
        public string OS_Series { get; set; }
    }
}
