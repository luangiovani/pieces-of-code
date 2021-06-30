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
    /// Mapeamento da Entidade Contact (Contatos), para gravação na tabela Contacts no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class Contact
    {
        /// <summary>
        /// Contact ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int contact_id { get; set; }

        /// <summary>
        /// Type of Contact
        /// Tipo de contato (Customer, Supplier, Manufacturer, Service Order...)
        /// </summary>
        public int typeContact_id { get; set; }

        /// <summary>
        /// Name
        /// Nome da Pessoa de Contato
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// City ID
        /// Id interno da cidade que será utilizado nos relacionamentos
        /// </summary>
        public int? city_id { get; set; }

        /// <summary>
        /// Toll Free
        /// Número de telefone gratuito (0800)
        /// </summary>
        public string tollFree { get; set; }

        /// <summary>
        /// Telephone
        /// Telefone de Contato
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// Ext
        /// Ramal do telefone quando houver
        /// </summary>
        public string extension { get; set; }

        /// <summary>
        /// Fax
        /// Número do Fax quando houver
        /// </summary>
        public string fax { get; set; }

        /// <summary>
        /// Mobile
        /// Número de Celular do Contato quando houver
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// Email
        /// E-mail de contato
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Other
        /// Outros tipos de contatos
        /// </summary>
        public string other { get; set; }

        /// <summary>
        /// Notes
        /// Informações adicionais do contato
        /// </summary>
        public string notes { get; set; }

        /// <summary>
        /// Active
        /// Indicador do status do contato
        /// </summary>
        //public bool active { get; set; }

        /// <summary>
        /// Receive Notification
        /// Indicador se este contato recebe emails de notificações do sistema
        /// </summary>
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

        public int? id_old { get; set; }

        /// <summary>
        /// City
        /// Entidade Cidade carregada por lazy loading
        /// </summary>
        public virtual City city { get; set; }

        public string cpf { get; set; }
        public string address { get; set; }
        public string addressNumbers { get; set; }
        public string addressExtension { get; set; }
        public string district { get; set; }
        public string postalZipCode { get; set; }

        public bool? indPrincipal { get; set; }

        public bool? indAcompanharSMS { get; set; }

        /// <summary>
        /// Type of Contact
        /// Tipo de Contato (Primário, Secundário, Recados....)
        /// </summary>
        public virtual TypeOfContact typeOfContact { get; set; }

        public virtual ICollection<AgentContacts> agentContacts { get; set; }

        public virtual ICollection<CustomerContact> customerContacts { get; set; }

        public virtual ICollection<ServiceOrderContact> serviceOrderContacts { get; set; }

        public virtual ICollection<SuppliersContact> suppliersContacts { get; set; }
    }
}
