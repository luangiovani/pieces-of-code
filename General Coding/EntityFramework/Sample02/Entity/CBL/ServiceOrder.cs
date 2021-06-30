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
   /// Mapeamento da Entidade ServiceOrder (Ordem de Serviço, Job, Project), para gravação na tabela ServiceOrder no Banco de Dados
   /// </sumary>
   /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
   /// <last_modified_date>3108/2016</last_modified_date>
   /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
   /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
   public class ServiceOrder
   {
      /// <summary>
      /// Service Order ID
      /// Id interno que será utilizado nos relacionamentos
      /// </summary>
      public decimal serviceOrder_id { get; set; }

      /// <summary>
      /// Date
      /// Data da ordem de serviço
      /// </summary>
      public DateTime date { get; set; }

      /// <summary>
      /// Usuer ID
      /// Id do usuário que criou a Ordem de Serviço
      /// </summary>
      public string user_id { get; set; }

      /// <summary>
      /// Customer ID
      /// Id do Cliente da Ordem de Serviço
      /// </summary>
      public int? customer_id { get; set; }

      /// <summary>
      /// Customer Contact ID
      /// Id do Contato do Cliente da Ordem de Serviço
      /// </summary>
      public int? customerContact_id { get; set; }

      public string customerContactName { get; set; }

      public string customerContactEmail { get; set; }

      public string customerContactTelephone { get; set; }

      public string customerContactMobile { get; set; }


      /// <summary>
      /// Referred By
      /// Referido Por
      /// </summary>
      public string referredBy { get; set; }

      /// <summary>
      /// Date
      /// Data para quem a Ordem de Serviço foi designada
      /// </summary>
      public DateTime? dateAssigned { get; set; }

      /// <summary>
      /// Usuer Assigned ID
      /// Usuário para quem a Ordem de Serviço foi designada
      /// </summary>
      public string userAssigned_id { get; set; }

      /// <summary>
      /// Status
      /// Status da Ordem de Serviço
      /// </summary>
      public string status { get; set; }

      /// <summary>
      /// Extension Status
      /// Descrição complementar do Status da ordem de serviço
      /// </summary>
      public string extensionStatus { get; set; }

      /// <summary>
      /// Taken By
      /// Feito Por
      /// </summary>
      public string takenBy { get; set; }

      /// <summary>
      /// CSR
      /// Técnico da Ordem de Serviço
      /// </summary>
      public string CSR { get; set; }

      public string typeOfService { get; set; }

      public string serviceToExecute { get; set; }

      public string mostImportantFilesToRecovery { get; set; }
      /// <summary>
      /// Estimate
      /// Estimativa em valor para execução do serviço
      /// </summary>
      public decimal? estimate { get; set; }

      /// <summary>
      /// Location Received
      /// Location que recebeu a Ordem de Serviço
      /// </summary>
      public int locationReceived_id { get; set; }

      /// <summary>
      /// Location
      /// Location que está a Ordem de Serviço
      /// </summary>
      public int? location_id { get; set; }

      /// <summary>
      /// Arrived By
      /// Recebido Por
      /// </summary>
      public string arrivedBy { get; set; }

      /// <summary>
      /// Way Bill Number
      /// Número do Encaminhamento
      /// </summary>
      public string wayBillNumber { get; set; }

      /// <summary>
      /// Package Condition
      /// Condição do Pacote
      /// </summary>
      public string packageCondidition { get; set; }

      /// <summary>
      /// Smart Number
      /// Número rápido
      /// </summary>
      public string smartNumber { get; set; }

      /// <summary>
      /// Techs Name
      /// Nome do Técnico
      /// </summary>
      public string techsName { get; set; }

      public int? id_old { get; set; }

      public string note { get; set; }

      /// <summary>
      /// Origem da Ordem de Serviço
      /// (Site, Pessoalmente, Telefone)
      /// </summary>
      public string originOfServiceOrder { get; set; }

      public DateTime? approvalDate { get; set; }

      public string subStatus { get; set; }

      public bool? inTransfer { get; set; }

      public string transferNote { get; set; }

      /// <summary>
      /// Data de alteração do status
      /// </summary>
      public DateTime statusDate { get; set; }


      public string codigoRastreio { get; set; }
      public string urlUploadContrato { get; set; }
      public string idPagamentoPagSeguro { get; set; }
      public DateTime? dtaAprovacaoContrato { get; set; }

      /// <summary>
      /// Location que está a Ordem de Serviço
      /// </summary>
      public virtual Locations location { get; set; }



      /// <summary>
      /// Location que foi recebida a Ordem de Serviço
      /// </summary>
      public virtual Locations locationReceived { get; set; }

      /// <summary>
      /// User
      /// Usuário da ordem de serviço
      /// </summary>
      public virtual ApplicationUser user { get; set; }

      /// <summary>
      /// Assigned To
      /// Usuário para quem a Ordem de Serviço foi designada
      /// </summary>
      public virtual ApplicationUser userAssigned { get; set; }

      /// <summary>
      /// Customer
      /// Cliente da Ordem de serviço
      /// </summary>
      public virtual Customer customer { get; set; }

      /// <summary>
      /// Contacts
      /// Lista de contatos da ordem de serviço
      /// </summary>
      public virtual ICollection<ServiceOrderContact> contacts { get; set; }

      /// <summary>
      /// Items
      /// Itens da Ordem de Serviço
      /// </summary>
      public virtual ICollection<AgentCommissions> agentComissions { get; set; }

      /// <summary>
      /// Lab Notes
      /// Notas do Laboratório da O.S.
      /// </summary>
      public virtual ICollection<LabNotes> labNotes { get; set; }

      public virtual ICollection<ServiceOrderNotes> serviceOrderNotes { get; set; }

      public virtual ICollection<ServiceOrderPayments> serviceOrderPayments { get; set; }

      public virtual ServiceOrderBilling serviceOrderBilling { get; set; }

      public virtual ServiceOrderEvaluation serviceOrderEvaluation { get; set; }

      public virtual ServiceOrderInquiryFollowUp serviceOrderInquiryFollowUp { get; set; }

      public virtual ServiceOrderQuoting serviceOrderQuoting { get; set; }

      public virtual ServiceOrderRecoveryFollowUp serviceOrderRecoveryFollowUp { get; set; }

      public virtual ServiceOrderShipping serviceOrderShipping { get; set; }

      public virtual ICollection<ServiceOrderMedias> serviceOrderMedias { get; set; }

      //public virtual ICollection<ServiceOrderAdditionalServices> additionalService { get; set; }


      // public virtual ServiceOrderStatus serviceOrderStatus { get; set; }
      //public virtual ICollection<ServiceOrderCloud> serviceOrderClouds { get; set; }

   }
}
