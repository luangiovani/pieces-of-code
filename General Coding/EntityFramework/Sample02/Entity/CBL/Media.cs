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
    public class Media 
    {
        /// <summary>
        /// Media ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        public int media_id { get; set; }

        /// <summary>
        /// Manufacturer ID
        /// Fabricante do Equipamento
        /// </summary>
        public int? manufacturer_id { get; set; }

        /// <summary>
        /// Supplier ID
        /// Fornecedor do Equipamento
        /// </summary>
        public int? supplier_id { get; set; }

        /// <summary>
        /// Location ID
        /// Location CBL que está o equipamento
        /// </summary>
        public int? location_id { get; set; }

        /// <summary>
        /// Media Status
        /// Indicador do Status descrito da Media
        /// </summary>
        public int mediaStatus_id { get; set; }

        /// <summary>
        /// Model
        /// Modelo do equipamento
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// Make
        /// Marca do Modelo do equipamento
        /// </summary>
        public string make { get; set; }

        /// <summary>
        /// Serial #
        /// Número serial do equipamento
        /// </summary>
        public string serial_no { get; set; }

        /// <summary>
        /// Part #
        /// Número de partições
        /// </summary>
        public string part_no { get; set; }

        /// <summary>
        /// Revision #
        /// Número da revisão
        /// </summary>
        public string revision_no { get; set; }

        /// <summary>
        /// Firmware #
        /// Número da Firmware ou versão
        /// </summary>
        public string firmware_no { get; set; }

        /// <summary>
        /// Size
        /// Tamanho ou capacidade de armazenamento do equipamento
        /// </summary>
        public string size { get; set; }

        /// <summary>
        /// Interface Type
        /// Tipo de interface de conexão (Interfaces)
        /// </summary>
        public string interfaceType { get; set; }

        /// <summary>
        /// PCB ID
        /// </summary>
        public string pcb_id { get; set; }

        /// <summary>
        /// Date Entered
        /// Data de entrada ou cadastro do equipamento
        /// </summary>
        
        public DateTime dateEntered { get; set; }

        /// <summary>
        /// MLC #
        /// Número do mlc
        /// </summary>
        public string mlc_no { get; set; }

        /// <summary>
        /// MFG Date
        /// Data do MFG
        /// </summary>
        
        public DateTime? mfgDate { get; set; }

        /// <summary>
        /// OEM #
        /// Número de OEM
        /// </summary>
        public string oem_no { get; set; }

        /// <summary>
        /// Up Level #
        /// Número de Up Level
        /// </summary>
        public string upLevel_no { get; set; }

        /// <summary>
        /// Series
        /// Versão
        /// </summary>
        public string series { get; set; }

        /// <summary>
        /// Condition
        /// Condições do equipamento
        /// </summary>
        public string condition { get; set; }

        /// <summary>
        /// Condition Information
        /// Descrição complementar da condição do equipamento
        /// </summary>
        public string conditionInformation { get; set; }

        /// <summary>
        /// DCM Site #
        /// Número DCM Site
        /// </summary>
        public string dcmSite_no { get; set; }

        /// <summary>
        /// Purchase From
        /// Adquirido de
        /// </summary>
        public string purchaseFrom { get; set; }

        /// <summary>
        /// Purchase Cost
        /// Custo de aquisição
        /// </summary>
        public decimal purchaseCost { get; set; }

        /// <summary>
        /// HDA
        /// </summary>
        public string hda { get; set; }

        /// <summary>
        /// PCB
        /// </summary>
        public string pcb { get; set; }

        /// <summary>
        /// Active
        /// Indicador de status (banco de dados)
        /// </summary>
        //public bool active { get; set; }

        public int? id_old { get; set; }

        public string extensionParts { get; set; }

        public string stockAddress { get; set; }

        public string madeIN { get; set; }

        public string modelInputByCustomer { get; set; }

        public string size_unit { get; set; }

        public decimal saleprice { get; set; }

        public bool indFisico { get; set; } 
        public bool indJuridico { get; set; }
        public bool indParceiro { get; set; }

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
        /// Media Status
        /// Status de equipamento
        /// </summary>
        public virtual MediaStatus mediaStatus { get; set; }

        /// <summary>
        /// Manufacturer
        /// Fabricante do equipamento
        /// </summary>
        public virtual Manufacturer manufacturer { get; set; }

        /// <summary>
        /// Locations
        /// Local atual (Escritório CBL) do equipamento
        /// </summary>
        public virtual Locations location { get; set; }

        /// <summary>
        /// Supplier
        /// Fornecedor do equipamento
        /// </summary>
        public virtual Suppliers supplier { get; set; }

        public virtual ICollection<PartNeeded> partsNeeded { get; set; }

        public virtual ICollection<ServiceOrderMedias> serviceOrderMedias { get; set; }
        
        public virtual ICollection<Stock> stockMovements { get; set; }

        public Media()
        {
            indFisico = false;
            indJuridico = false;
            indParceiro = false;
        }
    }
}
