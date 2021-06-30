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
    /// Mapeamento da Entidade Agents (Parceiros da CBL), para gravação na tabela Agents no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class Agent
    {
        /// <summary>
        /// Agent ID
        /// Id interno do parceiro que será utilizado nos relacionamentos e controle único
        /// </summary>
        public int agent_id { get; set; }

        /// <summary>
        /// City ID
        /// Id interno da cidade do endereço do parceiro
        /// </summary>
        public int city_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do parceiro
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Company Name
        /// Nome da empresa do parceiro
        /// </summary>
        public string companyName { get; set; }

        /// <summary>
        /// Address
        /// Endereço completo do parceiro (logradouro, número e complemento)
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Postal / Zip
        /// CEP ou outro código postal do parceiro
        /// </summary>
        public string postalZipCode { get; set; }

        /// <summary>
        /// Web Site
        /// Url do endereço eletrônico do parceiro
        /// </summary>
        public string website { get; set; }

        /// <summary>
        /// User
        /// Usuario de acesso do parceiro
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Password
        /// Senha de acesso do parceiro
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Forwarded To
        /// Encaminhamento do parceiro
        /// </summary>
        public string forwardedTo { get; set; }

        /// <summary>
        /// Demarcation #
        /// Número de demarcação do parceiro
        /// </summary>
        public string demarcation_no { get; set; }

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
        /// Date Added
        /// Data de adição da Nota
        /// </summary>
        public DateTime? dateProgramInformation { get; set; }

        /// <summary>
        /// Program Information type
        /// Tipo do programa de parceria no cadastro da nota
        /// </summary>
        public string programInformationType { get; set; }

        /// <summary>
        /// Program Information notes
        /// Notas do programa de parceria
        /// </summary>
        public string programInformationNotes { get; set; }

        public int? id_old { get; set; }

        /// <summary>
        /// City
        /// Cidade do Endereço do Parceiro, retorna Estado e País, DDD e DDI
        /// </summary>
        public virtual City city { get; set; }

        /// <summary>
        /// Contacts
        /// Lista de Contatos do Parceiro
        /// </summary>
        public virtual ICollection<AgentContacts> contacts { get; set; }

        /// <summary>
        /// Commissions
        /// Lista de Comissões do Agente por O.S.
        /// </summary>
        public virtual ICollection<AgentCommissions> agentCommissions { get; set; }


    }
}
