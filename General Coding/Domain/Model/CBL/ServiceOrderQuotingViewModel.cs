using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ServiceOrderQuotingViewModel
    {
        public ServiceOrderQuotingViewModel()
        {
            quoteDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            serviceOrderNotes = new List<ServiceOrderNotesViewModel>();
        }

        /// <summary>
        /// serviceOrder_id
        /// Id interno que será utilizado nos relacionamentos
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
        [Display(Name = "User Registration Id")]
        public string userRegistration_id { get; set; }

        /// <summary>
        /// Part Needed Id
        /// Parte necessária Id
        /// </summary>
        [Display(Name = "Part Needed Id")]
        public int? partNeeded_id { get; set; }

        /// <summary>
        /// quoteEstimate
        /// Citação Estimate
        /// </summary>     
        [Display(Name = "Original Quote")]
        public decimal quoteEstimate { get; set; }

        /// <summary>
        /// quotedAmount
        /// Citar, denominar montante
        /// </summary>     
        [Display(Name = "Final Quote")]
        public decimal quotedAmount { get; set; }

        /// <summary>
        /// discountGivem
        /// Desconto dado
        /// </summary>     
        [Display(Name = "Discount Given")]
        public decimal discountGivem { get; set; }

        /// <summary>
        /// nextDepotAmount
        /// Próxima Quantia Depot
        /// </summary>     
        [Display(Name = "Next Depot Amount")]
        public decimal nextDepotAmount { get; set; }

        /// <summary>
        /// Currency
        /// Moeda
        /// </summary>     
        [Display(Name = "Currency")]
        public string currency { get; set; }

        /// <summary>
        /// timeNeeded
        /// Tempo Necessário
        /// </summary>     
        [Display(Name = "Complement")]
        public string timeNeeded { get; set; }

        /// <summary>
        /// dueDate
        /// Data de Vencimento
        /// </summary>     
        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode=true)]
        public DateTime? dueDate { get; set; }

        /// <summary>
        /// destination
        /// Destino
        /// </summary>     
        [Display(Name = "Destination")]
        public string destination { get; set; }

        /// <summary>
        /// quoteLines
        /// Linhas de cotação
        /// </summary>     
        [Display(Name = "Quote Lines")]
        public string quoteLines { get; set; }

        /// <summary>
        /// Date of Quoting
        /// Data do orçamento
        /// </summary>
        [Display(Name = "Quoting Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? quoteDate { get; set; }

        [Display(Name = "Quoting Finished?")]
        public bool? quotedFinished { get; set; }

        [Display(Name = "Status Quoting")]
        [StringLength(50, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string statusQuoting { get; set; }

        public int? quotingNoteId { get; set; }

        [Display(Name = "Note")]
        [StringLength(500, ErrorMessage = "The field {0} should not exceed {1} characters.")]
        public string quotingNote { get; set; }

        [Display(Name = "Days")]
        public int? quoteDays { get; set; }

        public virtual ICollection<ServiceOrderNotesViewModel> serviceOrderNotes { get; set; }

        /// <summary>
        /// serviceOrder
        /// Ordem de serviço
        /// </summary>   
        //public virtual ServiceOrderViewModel serviceOrder { get; set; }

        /// <summary>
        /// PartNeeded
        /// Parte Necessária
        /// </summary>   
        //public virtual ICollection<PartNeededViewModel> partsNeeded { get; set; }


        public static implicit operator ServiceOrderQuotingViewModel(ServiceOrderQuoting obj)
        {
            if (obj != null)
            {
                return new ServiceOrderQuotingViewModel
                {
                    currency = obj.currency,
                    dateRegistration = obj.dateRegistration,
                    destination = obj.destination,
                    discountGivem = obj.discountGivem,
                    dueDate = obj.dueDate,
                    nextDepotAmount = obj.nextDepotAmount,
                    partNeeded_id = obj.partNeeded_id,                    
                    quotedAmount = obj.quotedAmount,
                    quoteDate = obj.quoteDate,
                    quoteDays = obj.quoteDays,
                    quotedFinished = obj.quotedFinished,
                    quoteEstimate = obj.quoteEstimate,
                    quoteLines = obj.quoteLines,
                    serviceOrder_id = obj.serviceOrder_id,
                    statusQuoting = obj.statusQuoting,
                    timeNeeded = obj.timeNeeded,
                    userRegistration_id = obj.userRegistration_id
                };
            }
            else
            {
                return null;
            }

        }
        public static implicit operator ServiceOrderQuoting(ServiceOrderQuotingViewModel obj)
        {
            if (obj != null)
            {
                return new ServiceOrderQuoting
                {

                    currency = obj.currency,
                    dateRegistration = obj.dateRegistration,
                    destination = obj.destination,
                    discountGivem = obj.discountGivem,
                    dueDate = obj.dueDate,
                    nextDepotAmount = obj.nextDepotAmount,
                    partNeeded_id = obj.partNeeded_id,
                    quotedAmount = obj.quotedAmount,
                    quoteDate = obj.quoteDate,
                    quoteDays = obj.quoteDays,
                    quotedFinished = obj.quotedFinished,
                    quoteEstimate = obj.quoteEstimate,
                    quoteLines = obj.quoteLines,
                    serviceOrder_id = obj.serviceOrder_id,
                    statusQuoting = obj.statusQuoting,
                    timeNeeded = obj.timeNeeded,
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
