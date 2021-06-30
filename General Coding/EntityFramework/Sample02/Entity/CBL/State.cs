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
    /// Mapeamento da Entidade State (Estados (UF)), para gravação na tabela State no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class State
    {
        /// <summary>
        /// State ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int state_id { get; set; }

        /// <summary>
        /// Country ID
        /// Id interno do País que será utilizado nos relacionamentos
        /// </summary>
        public int country_id { get; set; }

        /// <summary>
        /// Initials
        /// Sigla do Estado
        /// </summary>
        public string initials { get; set; }

        /// <summary>
        /// Name
        /// Nome do Estado
        /// </summary>
        public string name { get; set; }

        public int? id_old { get; set; }

        /// <summary>
        /// Entidade Country(País) carregada por lazy loading
        /// </summary>
        public virtual Country country { get; set; }

        /// <summary>
        /// Cities
        /// Cidades do Estado
        /// </summary>
        public virtual ICollection<City> cities { get; set; }
    }
}
