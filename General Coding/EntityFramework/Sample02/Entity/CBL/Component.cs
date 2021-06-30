using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <task_url>https://esfera.teamworkpm.net/#tasks/7016888</task_url>
/// <autor>Fabricio Kikina</autor>
/// <date>23/11/2016</date>
/// <sumary>
/// Mapeamento da Entidade Component, para gravação na tabela Component no Banco de Dados
/// </sumary>

namespace Framework.Database.Entity.CBL
{

    public class Component
    {
        /// <summary>
        /// ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int component_id { get; set; }

        /// <summary>
        /// Name
        /// descricao do componente
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Name
        /// cor do componente
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// Active
        /// Indicador de Status
        /// </summary>
        //public bool active { get; set; }

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

        public string stockAddress { get; set; }

        public virtual ICollection<Stock> stockMovements { get; set; }
    }
}
