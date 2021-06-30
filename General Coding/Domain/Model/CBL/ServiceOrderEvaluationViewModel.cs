using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderEvaluationViewModel
    {
        public ServiceOrderEvaluationViewModel()
        {
            labNotes = new List<LabNotesViewModel>();
            novoLabNote = new LabNotesViewModel();
        }
        /// <summary>
        /// serviceOrder_id
        /// Id FK interno que será utilizado nos relacionamentos
        /// </summary>
        [Key]
        [Display(Name = "Service Order ID")]
        public decimal serviceOrder_id { get; set; }

        /// <summary>
        /// Date of Register
        /// Data da inclusão deste registro
        /// </summary>
        [Display(Name = "Register Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateRegistration { get; set; }

        /// <summary>
        /// User Registration Id
        /// Usuário que cadastrou o registro
        /// </summary>
        public string userRegistration_id { get; set; }

        /// <summary>
        /// Called About
        /// Chamado Sobre
        /// </summary>
        [Display(Name = "Called About")]
        public string calledAbout { get; set; }

        /// <summary>
        /// Estimated Given From
        /// Valor estimado de
        /// </summary>
        [Display(Name = "Estimated Given From")]
        public decimal estimatedGivenFrom { get; set; }

        /// <summary>
        /// Estimated Given To
        /// Valor estimado até
        /// </summary>
        [Display(Name = "To")]
        public decimal estimatedGivenTo { get; set; }

        /// <summary>
        /// make
        /// Fazer, faço
        /// </summary>
        [Display(Name = "Make")]
        public string make { get; set; }

        /// <summary>
        /// serial_no
        /// Número de série
        /// </summary>
        [Display(Name = "Serial No")]
        public string serial_no { get; set; }

        /// <summary>
        /// makeOfComputer
        /// marca de computador
        /// </summary>
        [Display(Name = "Make Of Computer")]
        public string makeOfComputer { get; set; }

        /// <summary>
        /// model
        /// Modelo
        /// </summary>
        [Display(Name = "Model")]
        public string model { get; set; }

        /// <summary>
        /// interfaceOfdevice
        /// Interface de dispositivo
        /// </summary>
        [Display(Name = "Interface Of device")]
        public string interfaceOfdevice { get; set; }

        /// <summary>
        /// operatingSystem
        /// Sistema operacional
        /// </summary>
        [Display(Name = "Operating System")]
        public string operatingSystem { get; set; }

        /// <summary>
        /// operatingSystemVersion
        /// Versão do sistema operacional
        /// </summary>
        [Display(Name = "Op. System Version")]
        public string operatingSystemVersion { get; set; }

        /// <summary>
        /// partitionInfo
        /// Informações da partição
        /// </summary>
        [Display(Name = "Partition Info")]
        public string partitionInfo { get; set; }

        /// <summary>
        /// RAID Type
        /// Tipo de RAID do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "Raid Type")]
        public string raidType { get; set; }

        /// <summary>
        /// Controlled Type
        /// Tipo de controle do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "Controlled Type")]
        public string controlledType { get; set; }

        /// <summary>
        /// Number Of Volumes
        /// Quantidade de volumes do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "# Of Volumes")]
        public int numberOfVolumes { get; set; }

        /// <summary>
        /// Block Size
        /// Tamanho de bloco do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "Block Size")]
        public decimal blockSize { get; set; }

        /// <summary>
        /// RAID Details
        /// Detalhes da RAID do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "RAID Details")]
        public string raidDetails { get; set; }

        /// <summary>
        /// Number of Tapes
        /// Número de fitas do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "# Of tapes")]
        public int numberOftapes { get; set; }

        /// <summary>
        /// Type of Backup System
        /// Tipo de sistema de backup do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "Backup System")]
        public string typeOfbackupSystem { get; set; }

        /// <summary>
        /// Number of Sessions
        /// Númedo de seções do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "# Of Sessions")]
        public int numberOfSessions { get; set; }

        /// <summary>
        /// Data Compression Type
        /// Tipo de compressão de dados do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "Data Compress Type")]
        public string dataCompressionType { get; set; }

        /// <summary>
        /// Tape Details
        /// Detalhes de fita do equipamento do item da Ordem de Serviço
        /// </summary>    
        [Display(Name = "Tape Details")]
        public string tapeDetails { get; set; }

        /// <summary>
        /// Failure or Malfunction
        /// Informações das falhas e malfuncionamento do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "Failure Mal function")]
        public string failureMalfunction { get; set; }

        /// <summary>
        /// Pre-Recovery Info
        /// Informações das partições do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "Pre Recovery Info")]
        public string preRecoveryInfo { get; set; }

        /// <summary>
        /// Critical Target Data
        /// Alvo de dados críticos do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "Critical Target Data")]
        public string criticalTargetData { get; set; }

        /// <summary>
        /// File Allocation Type
        /// Tipo de sistema de arquivos do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "File Allocation Type")]
        public string fileAllocationType { get; set; }

        /// <summary>
        /// Number of drives in System
        /// Quantidade de discos do equipamento do item da Ordem de Serviço
        /// </summary>
        [Display(Name = "# Of Drives")]
        public int numberOfDrivesInSystem { get; set; }

        [Display(Name = "Make of Controller")]
        public string makeOfController { get; set; }

        [Display(Name = "Fault Found")]
        public string faultFound { get; set; }

        [Display(Name = "Job Class")]
        public string jobClass { get; set; }

        [Display(Name = "Tech Team")]
        public string techTeam { get; set; }

        [Display(Name = "Processed Where")]
        public string processedWhere { get; set; }

        [Display(Name = "Tech Notes")]
        public string techNotes { get; set; }

        [Display(Name = "Diagnosis are Finished?")]
        public bool? diagnosisFinished { get; set; }
        //public virtual ServiceOrderViewModel serviceOrder { get; set; }

        public virtual LabNotesViewModel novoLabNote { get; set; }
        public virtual ICollection<LabNotesViewModel> labNotes { get; set; }



        public static implicit operator ServiceOrderEvaluationViewModel(ServiceOrderEvaluation obj)
        {
            if (obj != null)
            {
                return new ServiceOrderEvaluationViewModel
                {
                    blockSize = obj.blockSize,
                    calledAbout = obj.calledAbout,
                    controlledType = obj.controlledType,
                    criticalTargetData = obj.criticalTargetData,
                    dataCompressionType = obj.dataCompressionType,
                    dateRegistration = obj.dateRegistration,
                    diagnosisFinished = obj.diagnosisFinished,
                    estimatedGivenFrom = obj.estimatedGivenFrom,
                    estimatedGivenTo = obj.estimatedGivenTo,
                    failureMalfunction = obj.failureMalfunction,
                    faultFound = obj.faultFound,
                    fileAllocationType = obj.fileAllocationType,
                    interfaceOfdevice = obj.interfaceOfdevice,
                    jobClass = obj.jobClass,
                    make = obj.make,
                    makeOfComputer = obj.makeOfComputer,
                    makeOfController = obj.makeOfController,
                    model = obj.model,
                    numberOfDrivesInSystem = obj.numberOfDrivesInSystem,
                    numberOfSessions = obj.numberOfSessions,
                    numberOftapes = obj.numberOftapes,
                    numberOfVolumes = obj.numberOfVolumes,
                    operatingSystem = obj.operatingSystem,
                    operatingSystemVersion = obj.operatingSystemVersion,
                    partitionInfo = obj.partitionInfo,
                    preRecoveryInfo = obj.preRecoveryInfo,
                    processedWhere = obj.processedWhere,
                    raidDetails = obj.raidDetails,
                    raidType = obj.raidType,
                    serial_no = obj.serial_no,
                    serviceOrder_id = obj.serviceOrder_id,
                    tapeDetails = obj.tapeDetails,
                    techNotes = obj.techNotes,
                    techTeam = obj.techTeam,
                    typeOfbackupSystem = obj.typeOfbackupSystem,
                    userRegistration_id = obj.userRegistration_id

                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator ServiceOrderEvaluation(ServiceOrderEvaluationViewModel obj)
        {
            if (obj != null)
            {
                return new ServiceOrderEvaluation
                {

                    blockSize = obj.blockSize,
                    calledAbout = obj.calledAbout,
                    controlledType = obj.controlledType,
                    criticalTargetData = obj.criticalTargetData,
                    dataCompressionType = obj.dataCompressionType,
                    dateRegistration = obj.dateRegistration,
                    diagnosisFinished = obj.diagnosisFinished,
                    estimatedGivenFrom = obj.estimatedGivenFrom,
                    estimatedGivenTo = obj.estimatedGivenTo,
                    failureMalfunction = obj.failureMalfunction,
                    faultFound = obj.faultFound,
                    fileAllocationType = obj.fileAllocationType,
                    interfaceOfdevice = obj.interfaceOfdevice,
                    jobClass = obj.jobClass,
                    make = obj.make,
                    makeOfComputer = obj.makeOfComputer,
                    makeOfController = obj.makeOfController,
                    model = obj.model,
                    numberOfDrivesInSystem = obj.numberOfDrivesInSystem,
                    numberOfSessions = obj.numberOfSessions,
                    numberOftapes = obj.numberOftapes,
                    numberOfVolumes = obj.numberOfVolumes,
                    operatingSystem = obj.operatingSystem,
                    operatingSystemVersion = obj.operatingSystemVersion,
                    partitionInfo = obj.partitionInfo,
                    preRecoveryInfo = obj.preRecoveryInfo,
                    processedWhere = obj.processedWhere,
                    raidDetails = obj.raidDetails,
                    raidType = obj.raidType,
                    serial_no = obj.serial_no,
                    serviceOrder_id = obj.serviceOrder_id,
                    tapeDetails = obj.tapeDetails,
                    techNotes = obj.techNotes,
                    techTeam = obj.techTeam,
                    typeOfbackupSystem = obj.typeOfbackupSystem,
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
