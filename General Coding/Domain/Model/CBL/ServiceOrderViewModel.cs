using Framework.Database.Entity.CBL;
using Framework.Domain.Utils;
using System;
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
    /// Mapeamento da Entidade ServiceOrder (Ordem de Serviço, Job, Project), para gravação na tabela ServiceOrder no Banco de Dados
    /// </sumary>
    /// <last_modified_task_url>https://esfera.teamworkpm.net/tasks/7054873</last_modified_task_url>
    /// <last_modified_date>3108/2016</last_modified_date>
    /// <last_modified_programmer>Luan Fernandes</last_modified_programmer>
    /// <last_modified_sumary>Ajustes após remodelagem de dados</last_modified_sumary>
    public class ServiceOrderViewModel
    {
        public ServiceOrderViewModel()
        {
            contacts = new List<ServiceOrderContactViewModel>();
            agentComissions = new List<AgentCommissionsViewModel>();
            labNotes = new List<LabNotesViewModel>();
            serviceOrderNotes = new List<ServiceOrderNotesViewModel>();
            novaMidia = new MediaViewModel();
            novoLabNote = new LabNotesViewModel();
            serviceOrderEvaluation = new ServiceOrderEvaluationViewModel();
            serviceOrderRecoveryFollowUp = new ServiceOrderRecoveryFollowUpViewModel();
            serviceOrderBilling = new ServiceOrderBillingViewModel();
            serviceOrderShipping = new ServiceOrderShippingViewModel();
            novoAgentCommission = new AgentCommissionsViewModel();
            novoContatoDaOrder = new ContactViewModel();
            novoContatoDaOrder.whoIsContact = WhoIsContact.ServiceOrder;
            serviceOrderMedias = new List<ServiceOrderMediasViewModel>();
            mediasNewOrder = new List<MediaFromNewOrder>();
        }
        /// <summary>
        /// Service Order ID
        /// Id interno que será utilizado nos relacionamentos
        /// </summary>]
        [Key]
        [Display(Name="Service Order ID")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal serviceOrder_id { get; set; }
        
        /// <summary>
        /// Date
        /// Data da ordem de serviço
        /// </summary>
        [Display(Name = "Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date { get; set; }

        /// <summary>
        /// User ID
        /// Id do usuário que criou a Ordem de Serviço
        /// </summary>
        [Display(Name = "User")]
        [StringLength(128, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string user_id { get; set; }

        /// <summary>
        /// Customer ID
        /// Id do Cliente da Ordem de Serviço
        /// </summary>
        [Display(Name = "Customer")]
        public int? customer_id { get; set; }

       

        /// <summary>
        /// Customer Contact ID
        /// Id do Cliente da Ordem de Serviço
        /// </summary>
        [Display(Name = "Customer Contact")]
        public int? customerContact_id { get; set; }

        [Display(Name = "Customer Contact Name")]
        public string customerContactName { get; set; }

        [Display(Name = "Email")]
        public string customerContactEmail { get; set; }

        [Display(Name = "Telephone")]
        public string customerContactTelephone { get; set; }

        [Display(Name = "Mobile")]
        public string customerContactMobile { get; set; }

        [Display(Name = "SMS")]
        public bool? customerContactIndSMS { get; set; }

        /// <summary>
        /// Referred By
        /// Referido Por
        /// </summary>
        [Display(Name = "Referred By")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string referredBy { get; set; }

        /// <summary>
        /// Date
        /// Data para quem a Ordem de Serviço foi designada
        /// </summary>
        [Display(Name = "Date Assigned")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dateAssigned { get; set; }

        /// <summary>
        /// Usuer Assigned ID
        /// Usuário para quem a Ordem de Serviço foi designada
        /// </summary>
        [Display(Name = "User Assigned")]
        [StringLength(128, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string userAssigned_id { get; set; }

        /// <summary>
        /// Status
        /// Status da Ordem de Serviço
        /// </summary>
        [Required(ErrorMessage = "Status is Required")]
        [Display(Name = "Status")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string status { get; set; }

        /// <summary>
        /// Extension Status
        /// Descrição complementar do Status da ordem de serviço
        /// </summary>
        [Display(Name = "Extension Status")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string extensionStatus { get; set; }

        /// <summary>
        /// Taken By
        /// Feito Por
        /// </summary>
        [Display(Name = "Taken By")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string takenBy { get; set; }

        /// <summary>
        /// Taken By
        /// Feito Por
        /// </summary>
        [Display(Name = "CSR")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string CSR { get; set; }

        /// <summary>
        /// Taken By
        /// Feito Por
        /// </summary>
        [Display(Name = "Type Of Service")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        [Required(ErrorMessage = "Type Of Service is Required")]
        public string typeOfService { get; set; }

        [Display(Name = "Service To Execute")]
        [StringLength(2000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string serviceToExecute { get; set; }

        [Display(Name = "Most important files")]
        [StringLength(2000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string mostImportantFilesToRecovery { get; set; }

        /// <summary>
        /// Estimate
        /// Valor estimado
        /// </summary>
        [Display(Name = "Estimate")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public decimal? estimate { get; set; }

        /// <summary>
        /// Location Received
        /// Location que recebeu a Ordem de Serviço
        /// </summary>
        [Display(Name = "Location Received")]
        [Required(ErrorMessage = "The field {0} is Required.")]
        public int locationReceived_id { get; set; }

        /// <summary>
        /// Location
        /// Location que está a Ordem de Serviço
        /// </summary>
        [Display(Name = "Currently Location")]
        public int? location_id { get; set; }

        /// <summary>
        /// Arrived By
        /// Recebido Por
        /// </summary>
        [Display(Name = "Arrived By")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string arrivedBy { get; set; }
        
        /// <summary>
        /// Way Bill Number
        /// Numero de Encaminhamento
        /// </summary>
        [Display(Name = "Way Bill #")]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string wayBillNumber { get; set; }
        
        /// <summary>
        /// Package Condition
        /// Condição do Pacote
        /// </summary>
        [Display(Name = "Package Condition")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string packageCondidition { get; set; }

        /// <summary>
        /// Smart Number
        /// Número rápido
        /// </summary>
        [Display(Name = "Smart #")]
        [StringLength(100, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string smartNumber { get; set; }

        /// <summary>
        /// Techs Name
        /// Nome do Técnico
        /// </summary>
        [Display(Name = "Techs Name")]
        [StringLength(250, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string techsName { get; set; }

        /// <summary>
        /// Origem da Ordem de Serviço
        /// (Site, Pessoalmente, Telefone, Outros) 
        /// </summary>
        [Display(Name = "Origin")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string originOfServiceOrder { get; set; }

        [Display(Name = "Note")]
        [StringLength(5000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string note { get; set; }

        public int? id_old { get; set; }

        public int? idNoteUpdate { get; set; }

        [Display(Name = "Note")]
        [StringLength(5000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string novaNota { get; set; }

        [Display(Name = "Sms")]
        [StringLength(5000, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string novaSms { get; set; }

        [Display(Name = "Approval Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? approvalDate { get; set; }

        [Display(Name = "Status Date")]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime statusDate { get; set; }

        [Display(Name = "Status Reason")]
        public string reasonCloseDeclineOS { get; set; }

        [Display(Name = "SubStatus")]
        public string subStatus { get; set; }

        [Display(Name = "In Transfer")]
        public bool? inTransfer { get; set; }


        [Display(Name = "Transfer Note")]
        public string transferNote { get; set; }


        [Display(Name = "Url Contrato Upload")]
        public string urlUploadContrato { get; set; }
        public string dtaAprovacaoContrato { get; set; }

        /// <summary>
        /// Location que está a Ordem de Serviço
        /// </summary>
        public virtual LocationsViewModel location { get; set; }

        /// <summary>
        /// Location que foi recebida a Ordem de Serviço
        /// </summary>
        public virtual LocationsViewModel locationReceived { get; set; }

        /// <summary>
        /// User
        /// Usuário da ordem de serviço
        /// </summary>
        public virtual UsuarioViewModel user { get; set; }

        /// <summary>
        /// Assigned To
        /// Usuário para quem a Ordem de Serviço foi designada
        /// </summary>
        public virtual UsuarioViewModel userAssigned { get; set; }

        /// <summary>
        /// Customer
        /// Cliente da Ordem de serviço
        /// </summary>
        public virtual CustomerViewModel customer { get; set; }

        /// <summary>
        /// Contacts
        /// Lista de contatos da ordem de serviço
        /// </summary>
        public virtual ICollection<ServiceOrderContactViewModel> contacts { get; set; }

        /// <summary>
        /// Items
        /// Itens da Ordem de Serviço
        /// </summary>
        public virtual ICollection<AgentCommissionsViewModel> agentComissions { get; set; }

        /// <summary>
        /// Lab Notes
        /// Notas do Laboratório da O.S.
        /// </summary>
        public virtual ICollection<LabNotesViewModel> labNotes { get; set; }

        public virtual ICollection<ServiceOrderNotesViewModel> serviceOrderNotes { get; set; }
        public virtual ICollection<ServiceOrderSmsViewModel> serviceOrderSms { get; set; }

        public virtual ServiceOrderBillingViewModel serviceOrderBilling { get; set; }

        public virtual ServiceOrderEvaluationViewModel serviceOrderEvaluation { get; set; }

        public virtual ServiceOrderInquiryFollowUpViewModel serviceOrderInquiryFollowUp { get; set; }

        public virtual ServiceOrderQuotingViewModel serviceOrderQuoting { get; set; }

        public virtual ServiceOrderRecoveryFollowUpViewModel serviceOrderRecoveryFollowUp { get; set; }

        public virtual ServiceOrderShippingViewModel serviceOrderShipping { get; set; }

        public virtual MediaViewModel novaMidia { get; set; }

        public virtual ICollection<ServiceOrderMediasViewModel> serviceOrderMedias { get; set; }

        public virtual LabNotesViewModel novoLabNote { get; set; }

        public virtual AgentCommissionsViewModel novoAgentCommission { get; set; }

        public virtual ContactViewModel novoContatoDaOrder { get; set; }

        [ScaffoldColumn(false)]
        public virtual ICollection<MediaFromNewOrder> mediasNewOrder { get; set; }


        public string telefonecontato { get; set; }
        public string celularcontato { get; set; }
        public bool indsmscontato { get; set; }




        public static implicit operator ServiceOrderViewModel(ServiceOrder obj)
        {
            if (obj != null)
            {


                var objeach = obj.serviceOrderMedias;
                var listMedias = new List<ServiceOrderMediasViewModel>();
                foreach (var item in objeach)
                {
                    listMedias.Add(item);
                }


                return new ServiceOrderViewModel
                {
                   //agentComissions = obj.agentComissions,
                   approvalDate = obj.approvalDate,
                   arrivedBy = obj.arrivedBy,
                   contacts = obj.contacts.Cast<ServiceOrderContactViewModel>().ToList(),
                   //contacts = (ICollection<ServiceOrderContactViewModel>)obj.contacts,
                   CSR = obj.CSR,
                   customer = obj.customer,
                   customer_id = obj.customer_id,
                   customerContact_id = obj.customerContact_id,
                   customerContactEmail = obj.customerContactEmail,
                   customerContactMobile = obj.customerContactMobile,
                   customerContactName = obj.customerContactName,
                   customerContactTelephone = obj.customerContactTelephone,
                   date = obj.date,
                   dateAssigned = obj.dateAssigned,
                   //dtaAprovacaoContrato = obj.dtaAprovacaoContrato,
                   estimate = obj.estimate,
                   extensionStatus = obj.extensionStatus,
                   id_old = obj.id_old,
                   inTransfer = obj.inTransfer,
                   //labNotes = obj.labNotes,
                   location = obj.location,
                   location_id = obj.location_id,
                   locationReceived = obj.locationReceived,
                   locationReceived_id = obj.locationReceived_id,
                   mostImportantFilesToRecovery = obj.mostImportantFilesToRecovery,
                   note = obj.note,
                   originOfServiceOrder = obj.originOfServiceOrder,
                   packageCondidition = obj.packageCondidition,
                   referredBy = obj.referredBy,
                   serviceOrder_id = obj.serviceOrder_id,
                   serviceOrderBilling = obj.serviceOrderBilling,
                   serviceOrderEvaluation = obj.serviceOrderEvaluation,
                   serviceOrderInquiryFollowUp = obj.serviceOrderInquiryFollowUp,
                   serviceOrderMedias = listMedias,
                    //serviceOrderNotes = (List<ServiceOrderNotesViewModel>)obj.serviceOrderNotes,
                   serviceOrderQuoting = obj.serviceOrderQuoting,
                   serviceOrderRecoveryFollowUp = obj.serviceOrderRecoveryFollowUp,
                   serviceOrderShipping = obj.serviceOrderShipping,
                   serviceToExecute = obj.serviceToExecute,
                   smartNumber = obj.smartNumber,
                   status = obj.status,
                    statusDate = obj.statusDate,
                    subStatus = obj.subStatus,
                    takenBy = obj.takenBy,
                    techsName = obj.techsName,
                    transferNote = obj.transferNote,
                    typeOfService = obj.typeOfService,
                    urlUploadContrato = obj.urlUploadContrato,
                    //user = obj.user,
                    user_id = obj.user_id,
                    //userAssigned = obj.userAssigned,
                    userAssigned_id = obj.userAssigned_id,
                    wayBillNumber = obj.wayBillNumber

                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator ServiceOrder(ServiceOrderViewModel obj)
        {
            if (obj != null)
            {
                var objeach = obj.serviceOrderMedias;
                var listMedias = new List<ServiceOrderMedias>();
                foreach (var item in objeach)
                {
                    listMedias.Add(item);
                }

                return new ServiceOrder
                {
                    //agentComissions = obj.agentComissions,
                    approvalDate = obj.approvalDate,
                    arrivedBy = obj.arrivedBy,
                    contacts = obj.contacts.Cast<ServiceOrderContact>().ToList(),
                    //contacts = (ICollection<ServiceOrderContact>)obj.contacts,
                    CSR = obj.CSR,
                    customer = obj.customer,
                    customer_id = obj.customer_id,
                    customerContact_id = obj.customerContact_id,
                    customerContactEmail = obj.customerContactEmail,
                    customerContactMobile = obj.customerContactMobile,
                    customerContactName = obj.customerContactName,
                    customerContactTelephone = obj.customerContactTelephone,
                    date = obj.date,
                    dateAssigned = obj.dateAssigned,
                    //dtaAprovacaoContrato = obj.dtaAprovacaoContrato,
                    estimate = obj.estimate,
                    extensionStatus = obj.extensionStatus,
                    id_old = obj.id_old,
                    inTransfer = obj.inTransfer,
                    //labNotes = obj.labNotes,
                    location = obj.location,
                    location_id = obj.location_id,
                    locationReceived = obj.locationReceived,
                    locationReceived_id = obj.locationReceived_id,
                    mostImportantFilesToRecovery = obj.mostImportantFilesToRecovery,
                    note = obj.note,
                    originOfServiceOrder = obj.originOfServiceOrder,
                    packageCondidition = obj.packageCondidition,
                    referredBy = obj.referredBy,
                    serviceOrder_id = obj.serviceOrder_id,
                    serviceOrderBilling = obj.serviceOrderBilling,
                    serviceOrderEvaluation = obj.serviceOrderEvaluation,
                    serviceOrderInquiryFollowUp = obj.serviceOrderInquiryFollowUp,
                    serviceOrderMedias = listMedias,
                    //serviceOrderNotes = (List<ServiceOrderNotes>)obj.serviceOrderNotes,
                    serviceOrderQuoting = obj.serviceOrderQuoting,
                    serviceOrderRecoveryFollowUp = obj.serviceOrderRecoveryFollowUp,
                    serviceOrderShipping = obj.serviceOrderShipping,
                    serviceToExecute = obj.serviceToExecute,
                    smartNumber = obj.smartNumber,
                    status = obj.status,
                    statusDate = obj.statusDate,
                    subStatus = obj.subStatus,
                    takenBy = obj.takenBy,
                    techsName = obj.techsName,
                    transferNote = obj.transferNote,
                    typeOfService = obj.typeOfService,
                    urlUploadContrato = obj.urlUploadContrato,
                    //user = obj.user,
                    user_id = obj.user_id,
                    //userAssigned = obj.userAssigned,
                    userAssigned_id = obj.userAssigned_id,
                    wayBillNumber = obj.wayBillNumber


                };
            }
            else
            {
                return null;
            }
        }





    }

    public class MediaFromNewOrder
    {
        public string make { get; set; }
        public string model { get; set; }
        public string serial { get; set; }
        public string status { get; set; }
    }
}
