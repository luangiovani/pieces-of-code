using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class ServiceOrderEvaluation
    {
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

        /// <summary>
        /// Called About
        /// Chamado Sobre
        /// </summary>
        public string calledAbout { get; set; }

        /// <summary>
        /// Estimated Given From
        /// Valor estimado de
        /// </summary>
        public decimal estimatedGivenFrom { get; set; }

        /// <summary>
        /// Estimated Given To
        /// Valor estimado até
        /// </summary>
        public decimal estimatedGivenTo { get; set; }

        public string make { get; set; }

        public string serial_no { get; set; }

        public string makeOfComputer { get; set; }

        public string model { get; set; }

        public string interfaceOfdevice { get; set; }

        public string operatingSystem { get; set; }

        public string operatingSystemVersion { get; set; }

        public string partitionInfo { get; set; }

        /// <summary>
        /// RAID Type
        /// Tipo de RAID do equipamento do item da Ordem de Serviço
        /// </summary>
        public string raidType { get; set; }

        /// <summary>
        /// Controlled Type
        /// Tipo de controle do equipamento do item da Ordem de Serviço
        /// </summary>
        public string controlledType { get; set; }

        /// <summary>
        /// Number Of Volumes
        /// Quantidade de volumes do equipamento do item da Ordem de Serviço
        /// </summary>
        public int numberOfVolumes { get; set; }

        /// <summary>
        /// Block Size
        /// Tamanho de bloco do equipamento do item da Ordem de Serviço
        /// </summary>
        public decimal blockSize { get; set; }

        /// <summary>
        /// RAID Details
        /// Detalhes da RAID do equipamento do item da Ordem de Serviço
        /// </summary>
        public string raidDetails { get; set; }

        /// <summary>
        /// Number of Tapes
        /// Número de fitas do equipamento do item da Ordem de Serviço
        /// </summary>
        public int numberOftapes { get; set; }

        /// <summary>
        /// Type of Backup System
        /// Tipo de sistema de backup do equipamento do item da Ordem de Serviço
        /// </summary>
        public string typeOfbackupSystem { get; set; }

        /// <summary>
        /// Number of Sessions
        /// Númedo de seções do equipamento do item da Ordem de Serviço
        /// </summary>
        public int numberOfSessions { get; set; }

        /// <summary>
        /// Data Compression Type
        /// Tipo de compressão de dados do equipamento do item da Ordem de Serviço
        /// </summary>
        public string dataCompressionType { get; set; }

        /// <summary>
        /// Tape Details
        /// Detalhes de fita do equipamento do item da Ordem de Serviço
        /// </summary>
        public string tapeDetails { get; set; }

        /// <summary>
        /// Failure or Malfunction
        /// Informações das falhas e malfuncionamento do equipamento do item da Ordem de Serviço
        /// </summary>
        public string failureMalfunction { get; set; }

        /// <summary>
        /// Pre-Recovery Info
        /// Informações das partições do equipamento do item da Ordem de Serviço
        /// </summary>
        public string preRecoveryInfo { get; set; }

        /// <summary>
        /// Critical Target Data
        /// Alvo de dados críticos do equipamento do item da Ordem de Serviço
        /// </summary>
        public string criticalTargetData { get; set; }

        /// <summary>
        /// File Allocation Type
        /// Tipo de sistema de arquivos do equipamento do item da Ordem de Serviço
        /// </summary>
        public string fileAllocationType { get; set; }

        /// <summary>
        /// Number of drives in System
        /// Quantidade de discos do equipamento do item da Ordem de Serviço
        /// </summary>
        public int numberOfDrivesInSystem { get; set; }

        public string makeOfController { get; set; }

        public string faultFound { get; set; }

        public string jobClass { get; set; }

        public string techTeam { get; set; }

        public string processedWhere { get; set; }

        public string techNotes { get; set; }

        public int? id_old { get; set; }

        public bool? diagnosisFinished { get; set; }

        public virtual ServiceOrder serviceOrder { get; set; }

    }
}
