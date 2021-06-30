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
    /// Mapeamento da Entidade City (Cidades utilizadas em endereços, contatos), para gravação na tabela City no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class City
    {
        /// <summary>
        /// City ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int city_id { get; set; }

        /// <summary>
        /// State ID
        /// Id interno do Estado que será utilizado nos relacionamentos
        /// </summary>
        public int state_id { get; set; }

        /// <summary>
        /// Country ID
        /// Id interno do País que será utilizado nos relacionamentos
        /// </summary>
        public int country_id { get; set; }

        /// <summary>
        /// Name
        /// Nome da Cidade
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// DDD
        /// DDD da cidade
        /// </summary>
        public int ddd { get; set; }

        public int? id_old { get; set; }

        /// <summary>
        /// State
        /// Estado (UF) da Cidade
        /// </summary>
        public virtual State state { get; set; }

        /// <summary>
        /// Country
        /// País da Cidade e Estado
        /// </summary>
        public virtual Country country { get; set; }

        /// <summary>
        /// Agents
        /// Parceiros CBL localizados na cidade
        /// </summary>
        public virtual ICollection<Agent> agents { get; set; }

        /// <summary>
        /// Locations
        /// Escritórios CBL localizados na cidade
        /// </summary>
        public virtual ICollection<Locations> locations { get; set; }

        /// <summary>
        /// Contacts
        /// Contatos localizados na cidade
        /// </summary>
        public virtual ICollection<Contact> contacts { get; set; }

        /// <summary>
        /// Suppliers
        /// Fornecedores localizados na cidade
        /// </summary>
        public virtual ICollection<Suppliers> suppliers { get; set; }

        public virtual ICollection<ServiceOrderBilling> serviceOrdersBilling { get; set; }

        public virtual ICollection<ServiceOrderShipping> serviceOrdersShip { get; set; }
    }
}
