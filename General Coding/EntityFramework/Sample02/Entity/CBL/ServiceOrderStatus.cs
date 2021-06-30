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
    /// Mapeamento da Entidade ServiceOrderStatus (Status da Ordem de Serviço), para gravação na tabela ServiceOrderStatus no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class ServiceOrderStatus
    {
        /// <summary>
        /// Service Order Status ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int serviceOrderStatus_id { get; set; }

        /// <summary>
        /// Service Order Status Parent ID
        /// Id interno que será utilizado nos relacionamentos
        /// Quando for um SubStatus, este campo não é nulo
        /// </summary>
        public int? serviceOrderStatusParent_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do Status da Ordem de Serviço
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Name Portal Client
        /// Nome do Status da Ordem de Serviço
        /// </summary>
        public string nameToClient { get; set; }

        /// <summary>
        /// Description
        /// Descrição do Status da ordem de serviço
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Order
        /// Ordem do Status, ordenação
        /// </summary>
        public int order { get; set; }

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
        /// Status parent do SubStatus
        /// </summary>
        public virtual ServiceOrderStatus serviceOrderStatusParent { get; set; }

        /// <summary>
        /// Lista de SubStatus vinculados como filhos deste Status
        /// </summary>
        public virtual ICollection<ServiceOrderStatus> serviceOrderStatusDependents { get; set; }

        //public virtual ICollection<ServiceOrder> serviceOrder { get; set; }
    }
}
