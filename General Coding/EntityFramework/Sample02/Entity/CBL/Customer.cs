using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
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
    public class Customer
    {
        /// <summary>
        /// Customer ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int customer_id { get; set; }

        /// <summary>
        /// Email
        /// Email do cliente
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Name
        /// Nome do cliente
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Address
        /// Endereço completo do cliente (logradouro, número e complemento)
        /// </summary>
        public string address { get; set; }

        public string addressNumber { get; set; }

        public string addressExtension { get; set; }

        /// <summary>
        /// District
        /// Bairro do cliente
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// Postal / Zip
        /// CEP ou outro código postal do cliente
        /// </summary>
        public string postalZipCode { get; set; }

        /// <summary>
        /// Web Site
        /// Url do endereço virtual do cliente
        /// </summary>
        public string website { get; set; }

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

        /// <summary>
        /// Hear Of Us
        /// informação de onde o cliente soube da empresa
        /// </summary>
        public string hearOfUs { get; set; }

        /// <summary>
        /// Point Of Contact
        /// Motivo do contato do cliente
        /// </summary>
        public string pointOfContact { get; set; }

        /// <summary>
        /// Credit Terms
        /// Termos de crédito concedido ao cliente
        /// </summary>
        public string creditTerms { get; set; }

        /// <summary>
        /// Password
        /// Senha do cliente
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Percentage
        /// Percentual aplicado ao cliente
        /// </summary>
        public decimal? percentage { get; set; }

        /// <summary>
        /// Tax # 1
        /// CPF ou CNPJ do cliente quando nacional ou outra informação relacionada Internacional
        /// </summary>
        public string cpfCnpj { get; set; }

        /// <summary>
        /// Tax # 2 
        /// RG do cliente ou ID Estadual dele quando internacional, podendo ser Social Number por exemplo
        /// </summary>
        public string rgIdState { get; set; }

        /// <summary>
        /// Notes
        /// Notas ou observações relacionadas ao cliente
        /// </summary>
        public string notes { get; set; }

        /// <summary>
        /// Active
        /// Indicador de status do Cliente
        /// </summary>
        //public bool active { get; set; }

        public int? id_old { get; set; }

        public string business_type { get; set; }

        public string tokenCode { get; set; }

        public DateTime? dateLastAccessPortal { get; set; }

        public DateTime? dateLastUpdateInformations { get; set; } 

        /// <summary>
        /// Contacts
        /// Lista de Contatos do cliente.
        /// </summary>
        public virtual ICollection<CustomerContact> contacts { get; set; }

        /// <summary>
        /// Service Orders
        /// Lista de Ordens de Serviço do Cliente
        /// </summary>
        public virtual ICollection<ServiceOrder> serviceOrders { get; set; }

        public virtual ICollection<ServiceOrderPayments> serviceOrderPayments { get; set; }
    }

    public class paginacaoCustomer
    {
        public int pagina { get; set; }
        public int idMenor { get; set; }
        public int idMaior { get; set; }
    }
}
