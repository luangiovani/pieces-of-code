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
    /// Mapeamento da Entidade Country (Países das cidades e estados utilizadas em endereços, contatos), para gravação na tabela Country no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class Country
    {
        /// <summary>
        /// Country ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int country_id { get; set; }

        /// <summary>
        /// Initials
        /// Sigla do País
        /// </summary>
        public string initials { get; set; }

        /// <summary>
        /// Name
        /// Nome do País
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// IDD
        /// DDI do País
        /// </summary>
        public int idd { get; set; }

        public int? id_old { get; set; }

        /// <summary>
        /// Cities
        /// Lista de Cidades do País
        /// </summary>
        public virtual ICollection<City> cities { get; set; }

        /// <summary>
        /// States
        /// Lista de Estados do País
        /// </summary>
        public virtual ICollection<State> states { get; set; }
    }
}
