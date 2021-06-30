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
    /// Mapeamento da Entidade Suppliers (Fornecedores), para gravação na tabela Suppliers no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class Suppliers
    {
        /// <summary>
        /// Supplier ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int supplier_id { get; set; }

        /// <summary>
        /// Name
        /// Nome do Fornecedor
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Address
        /// Endereço completo do Fornecedor
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Postal Zip/Code
        /// CEP ou código postal do endereço do fornecedor
        /// </summary>
        public string postalZipCode { get; set; }

        /// <summary>
        /// City
        /// Cidade do Endereço do Fornecedor
        /// </summary>
        public int city_id { get; set; }

        /// <summary>
        /// Web Site
        /// Endereço eletrônico do fornecedor
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

        public int? id_old { get; set; }

        /// <summary>
        /// City
        /// Cidade do Endereço do Fornecedor
        /// </summary>
        public virtual City city { get; set; }

        /// <summary>
        /// Contacts
        /// Lista de Contatos do Fornecedor
        /// </summary>
        public virtual ICollection<SuppliersContact> contacts { get; set; }

        /// <summary>
        /// Medias
        /// Lista de equipamentos do Fornecedor
        /// </summary>
        public virtual ICollection<Media> medias { get; set; }

        public virtual ICollection<PartNeeded> parts { get; set; }
    }
}
