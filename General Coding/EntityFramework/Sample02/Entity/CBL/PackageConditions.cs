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
    /// Mapeamento da Entidade PackageConditions (Condições dos pacotes), para gravação na tabela PackageConditions no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class PackageConditions
    {
        /// <summary>
        /// Package Conditions ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int packageConditions_id { get; set; }

        /// <summary>
        /// Name
        /// Nome das condições do pacote
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Description
        /// Descrição das condições do pacote
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Active
        /// Indicador de Status das condições do pacote
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
    }
}
