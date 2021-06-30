using Framework.Database.Entity.CBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
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
    public class MediaViewModel 
    {
        public MediaViewModel()
        {
            dateEntered = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }
        /// <summary>
        /// Media ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Media ID")]
        public int media_id { get; set; }

        /// <summary>
        /// Manufacturer ID
        /// Fabricante do Equipamento
        /// </summary>
        [Display(Name = "Manufacturer/Make")]
        public int? manufacturer_id { get; set; }

        /// <summary>
        /// Supplier ID
        /// Fornecedor do Equipamento
        /// </summary>
        [Display(Name = "Supplier")]
        public int? supplier_id { get; set; }

        /// <summary>
        /// Location ID
        /// Location CBL que está o equipamento
        /// </summary>
        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location is Required")]
        public int? location_id { get; set; }

        /// <summary>
        /// Media Status
        /// Indicador do Status descrito da Media
        /// </summary>
        [Required(ErrorMessage = "Media Status is Required")]
        [Display(Name = "Media Status")]
        public int mediaStatus_id { get; set; }

        /// <summary>
        /// Model
        /// Modelo do equipamento
        /// </summary>
        [Display(Name = "Model")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Required(ErrorMessage = "Model is Required")]
        public string model { get; set; }

        /// <summary>
        /// Make
        /// Marca Modelo do equipamento
        /// </summary>
        [Display(Name = "Make")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string make { get; set; }

        /// <summary>
        /// Serial #
        /// Número serial do equipamento
        /// </summary>
        [Display(Name = "Serial #")]
        [StringLength(40, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Required(ErrorMessage = "Serial # is Required")]
        public string serial_no { get; set; }

        /// <summary>
        /// Part #
        /// Número de partições
        /// </summary>
        [Display(Name = "Part #")]
        [StringLength(40, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string part_no { get; set; }

        /// <summary>
        /// Revision #
        /// Número da revisão
        /// </summary>
        [Display(Name = "Revision #")]
        [StringLength(40, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string revision_no { get; set; }

        /// <summary>
        /// Firmware #
        /// Número da Firmware ou versão
        /// </summary>
        [Display(Name = "Firmware #")]
        [StringLength(40, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string firmware_no { get; set; }

        /// <summary>
        /// Size
        /// Tamanho ou capacidade de armazenamento do equipamento
        /// </summary>
        [Display(Name = "Size")]
        [StringLength(40, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string size { get; set; }

        [Display(Name = "Size Unit")]
        [StringLength(10, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string size_unit { get; set; }

        /// <summary>
        /// Interface Type
        /// Tipo de interface de conexão (Interfaces)
        /// </summary>
        [Display(Name = "Interface Type")]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string interfaceType { get; set; }

        /// <summary>
        /// PCB ID
        /// </summary>
        [Display(Name = "PCB ID")]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string pcb_id { get; set; }

        /// <summary>
        /// Date Entered
        /// Data de entrada ou cadastro do equipamento
        /// </summary>
        [Display(Name = "Date Entered")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]        
        [Required(ErrorMessage = "The field {0} is required.")]
        public DateTime dateEntered { get; set; }
        //public string dateEnteredString { get; set; }

        /// <summary>
        /// MLC #
        /// Número do mlc
        /// </summary>
        //[Display(Name = "MLC #")] - Solicitado para alterar - Email Gilberto em 19/01/2018
        [Display(Name = "Data Code")]
        [StringLength(40, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string mlc_no { get; set; }

        /// <summary>
        /// MFG Date
        /// Data do MFG
        /// </summary>
        [Display(Name = "MFG Date")]
        [ScaffoldColumn(false)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? mfgDate { get; set; }
        //public string mfgDateString { get; set; }

        /// <summary>
        /// OEM #
        /// Número de OEM
        /// </summary>
        [Display(Name = "OEM #")]
        [StringLength(40, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string oem_no { get; set; }

        /// <summary>
        /// Up Level #
        /// Número de Up Level
        /// </summary>
        //[Display(Name = "Up Level #")] - Solicitado para alterar - Email Gilberto em 19/01/2018
        [Display(Name = "MDL")]
        [StringLength(40, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string upLevel_no { get; set; }

        /// <summary>
        /// Series
        /// Versão
        /// </summary>
        [Display(Name = "Series")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string series { get; set; }

        /// <summary>
        /// Condition
        /// Condições do equipamento
        /// </summary>
        [Display(Name = "Condition")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string condition { get; set; }

        /// <summary>
        /// Condition Information
        /// Descrição complementar da condição do equipamento
        /// </summary>
        [Display(Name = "Condition Information")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string conditionInformation { get; set; }

        /// <summary>
        /// DCM Site #
        /// Número DCM Site
        /// </summary>
        [Display(Name = "DCM Site #")]
        [StringLength(40, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string dcmSite_no { get; set; }

        /// <summary>
        /// Purchase From
        /// Adquirido de
        /// </summary>
        [Display(Name = "Purchase From")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string purchaseFrom { get; set; }

        /// <summary>
        /// Purchase Cost
        /// Custo de aquisição
        /// </summary>
        [Display(Name = "Purchase Cost")]
        //[DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public decimal purchaseCost { get; set; }

        /// <summary>
        /// HDA
        /// </summary>
        [Display(Name = "HDA")]
        [StringLength(200, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string hda { get; set; }

        /// <summary>
        /// PCB
        /// </summary>
        [Display(Name = "PCB")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string pcb { get; set; }

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

        [Display(Name="Extra Devices (USB Cable, Flat Cable, HD Case...)")]
        [StringLength(4000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string extensionParts { get; set; }

        /// <summary>
        /// Stock Address
        /// Endereço do Equipamento no Estoque
        /// </summary>
        [Display(Name = "Stock Address")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string stockAddress { get; set; }

        [Display(Name = "Compatibility")]
        public string compatibility { get; set; }

        [Display(Name = "Made IN")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string madeIN { get; set; }

        /// <summary>
        /// Model do equipamento que foi inserido pelo Cliente na tela de Request
        /// </summary>
        [Display(Name = "Model Input By Customer")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string modelInputByCustomer { get; set; }

        [Display(Name = "Sale Price")]
        //[DisplayFormat(DataFormatString = "{0:#,##}", ApplyFormatInEditMode = true)]
        public decimal saleprice { get; set; }

        [Display(Name = "Portal do Representante")]
        public Boolean indFisico { get; set; }
        [Display(Name = "Portal do Físico")]
        public Boolean indJuridico { get; set; }
        [Display(Name = "Portal do Jurídico")]
        public Boolean indParceiro { get; set; }

        public string acao { get; set; }

        /// <summary>
        /// Media Status
        /// Status de equipamento
        /// </summary>
        [Display(Name = "Media Status")]
        public virtual MediaStatusViewModel mediaStatus { get; set; }

        /// <summary>
        /// Manufacturer
        /// Fabricante do equipamento
        /// </summary>
        [Display(Name = "Manufacturer")]
        public virtual ManufacturerViewModel manufacturer { get; set; }

        /// <summary>
        /// Locations
        /// Local atual (Escritório CBL) do equipamento
        /// </summary>
        [Display(Name = "Location")]
        public virtual LocationsViewModel location { get; set; }

        /// <summary>
        /// Supplier
        /// Fornecedor do equipamento
        /// </summary>
        [Display(Name="Supplier")]
        public virtual SuppliersViewModel supplier { get; set; }

        public virtual ICollection<PartNeededViewModel> partsNeeded { get; set; }

        [Display(Name = "Service Order")]
        public virtual ICollection<ServiceOrderMediasViewModel> serviceOrderMedias { get; set; }

        [Display(Name = "Stock Movements")]
        public virtual ICollection<StockViewModel> stockMovements { get; set; }

        

        

        public static implicit operator MediaViewModel(Media obj)
        {
            if (obj != null)
            {
                return new MediaViewModel
                {
                    media_id = obj.media_id,
                    manufacturer_id = obj.manufacturer_id,
                    supplier_id = obj.supplier_id,
                    location_id = obj.location_id,
                    mediaStatus_id = obj.mediaStatus_id,
                    model = obj.model,
                    make = obj.make,
                    serial_no = obj.serial_no,
                    part_no = obj.part_no,
                    revision_no = obj.revision_no,
                    firmware_no = obj.firmware_no,
                    size = obj.size,
                    interfaceType = obj.interfaceType,
                    pcb_id = obj.pcb_id,
                    dateEntered = obj.dateEntered,
                    mlc_no = obj.mlc_no,
                    mfgDate = obj.mfgDate,
                    oem_no = obj.oem_no,
                    upLevel_no = obj.upLevel_no,
                    series = obj.series,
                    condition = obj.condition,
                    conditionInformation = obj.conditionInformation,
                    dcmSite_no = obj.dcmSite_no,
                    purchaseFrom = obj.purchaseFrom,
                    purchaseCost = obj.purchaseCost,
                    hda = obj.hda,
                    pcb = obj.pcb,
                    dateRegistration = obj.dateRegistration,
                    extensionParts = obj.extensionParts,
                    stockAddress = obj.stockAddress,
                    madeIN = obj.madeIN,
                    modelInputByCustomer = obj.modelInputByCustomer,
                    saleprice = obj.saleprice,
                    indFisico = obj.indFisico,
                    indJuridico = obj.indJuridico,
                    indParceiro = obj.indParceiro,
                    size_unit = obj.size_unit,
                    userRegistration_id = obj.userRegistration_id


                };
            }
            else
            {
                return null;
            }
        }

        public static implicit operator Media(MediaViewModel obj)
        {
            if (obj != null)
            {
                return new Media
                {
                    media_id = obj.media_id,
                    manufacturer_id = obj.manufacturer_id,
                    supplier_id = obj.supplier_id,
                    location_id = obj.location_id,
                    mediaStatus_id = obj.mediaStatus_id,
                    model = obj.model,
                    make = obj.make,
                    serial_no = obj.serial_no,
                    part_no = obj.part_no,
                    revision_no = obj.revision_no,
                    firmware_no = obj.firmware_no,
                    size = obj.size,
                    interfaceType = obj.interfaceType,
                    pcb_id = obj.pcb_id,
                    dateEntered = obj.dateEntered,
                    mlc_no = obj.mlc_no,
                    mfgDate = obj.mfgDate,
                    oem_no = obj.oem_no,
                    upLevel_no = obj.upLevel_no,
                    series = obj.series,
                    condition = obj.condition,
                    conditionInformation = obj.conditionInformation,
                    dcmSite_no = obj.dcmSite_no,
                    purchaseFrom = obj.purchaseFrom,
                    purchaseCost = obj.purchaseCost,
                    hda = obj.hda,
                    pcb = obj.pcb,
                    dateRegistration = obj.dateRegistration,
                    extensionParts = obj.extensionParts,
                    stockAddress = obj.stockAddress,
                    madeIN = obj.madeIN,
                    modelInputByCustomer = obj.modelInputByCustomer,
                    saleprice = obj.saleprice,
                    indFisico = obj.indFisico,
                    indJuridico = obj.indJuridico,
                    indParceiro = obj.indParceiro,
                    size_unit = obj.size_unit,
                    userRegistration_id = obj.userRegistration_id


                };
            }
            else
            {
                return null;
            }
        }

    }
}