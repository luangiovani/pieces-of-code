using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    /// <task_url>https://esfera.teamworkpm.net/tasks/7054873</task_url>
    /// <autor>Luan Fernandes</autor>
    /// <date>11/08/2016</date>
    /// <sumary>
    /// Mapeamento da Entidade Media (Equipamento), para gravação na tabela Media no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class EquipmentsSell
    {
        /// <summary>
        /// equipament_id
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int equipament_id { get; set; }

        /// <summary>
        /// Amount
        /// Quantidade
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Make
        /// Fornecedor do Equipamento
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Model
        /// modelo  do equipamento
        /// </summary>
         public string Model { get; set; }

        /// <summary>
         /// Size
        /// tamanho do equipamento
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// SalePrice
        /// preço de venda
        /// </summary>
        public string SalePrice { get; set; }

        /// <summary>
        /// media_id
        /// relacionamento
        /// </summary>
        public int media_id { get; set; }

        /// <summary>
        /// serviceOrder_id
        /// relacionamento
        /// </summary>
        public decimal serviceOrder_id { get; set; }

        

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


        

        

    }
}
