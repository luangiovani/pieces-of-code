using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ReportsViewModel
    {
        public ReportsViewModel()
        {
            Medias = new List<MediasReportsViewModel>();
            Clouds = new List<ServiceOrderCloudsReportsViewModel>();
        }

        public string ServiceOrderId { get; set; }
        public string ServiceOrderDate { get; set; }
        public string ServiceOrderLocationReceived { get; set; }
        public string ServiceOrderLocation { get; set; }
        public string ServiceOrderStatus { get; set; }
        public string ServiceOrderSubStatus { get; set; }
        public string ServiceOrderReferredBy { get; set; }
        public string ServiceOrderDateQuoted { get; set; }
        public string ServiceOrderApprovalDate { get; set; }
        public string ServiceOrderRecoveryTime { get; set; }
        public string ServiceOrderStatusDate { get; set; }
        public string ServiceOrderDelay { get; set; }
        public string ServiceOrderNotes { get; set; }
        public string ServiceOrderNotesDate { get; set; }
        public string ServiceOrderNotesUser { get; set; }
        public string ServiceOrderLabNotes { get; set; }
        public string ServiceOrderLabNotesDate { get; set; }
        public string ServiceOrderLabNotesUser { get; set; }
        public string ServiceOrderTypeOfService { get; set; }
        public string ServiceOrderDateAssigned { get; set; }
        public string ServiceOrderUserAssigned { get; set; }
        public string ServiceOrderCustomer { get; set; }
        public string ServiceOrderCustomerCPFCnpj { get; set; }
        public string ServiceOrderHardError { get; set; }

        public IEnumerable<MediasReportsViewModel> Medias { get; set; }
        public IEnumerable<ServiceOrderCloudsReportsViewModel> Clouds { get; set; }
    }

    public class ServiceOrderCloudsReportsViewModel
    {
        public string dataVencimento { get; set; }
        public string tamanhoCloud { get; set; }
    }

    public class MediasReportsViewModel
    {
        public string ServiceOrderMediaId { get; set; }
        public string MediaId { get; set; }
        public string MediaMake { get; set; }
        public string MediaModel { get; set; }
        public string MediaStatus { get; set; }
        public string MediaError { get; set; }
    }

    public class ReportsFilter
    {
        [Display(Name = "Type Of Service")]
        public string TypeOfServiceFilter { get; set; }
        [Display(Name = "Service Order Status")]
        public string ServiceOrderStatusFilter { get; set; }
        [Display(Name = "Quoting SubStatus")]
        public string QuotingSubStatusFilter { get; set; }
        [Display(Name = "Order Date From")]
        public string DateFromFilter { get; set; }
        [Display(Name = "To")]
        public string DateToFilter { get; set; }
        [Display(Name = "Status Order Date From")]
        public string StatusDateFromFilter { get; set; }
        [Display(Name = "To")]
        public string StatusDateToFilter { get; set; }
        [Display(Name = "Customer CPF/CNPJ")]
        public string CustomerCPFCNPJFilter { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerNameFilter { get; set; }
        [Display(Name = "Type Of Dashboard")]
        public string TypeOfDashboardFilter { get; set; }
        [Display(Name = "Layout")]
        public string LayoutFilter { get; set; }
        [Display(Name = "Print")]
        public string PrintType { get; set; }
        [Display(Name = "Location")]
        public string LocationFilter { get; set; }
        [Display(Name = "Plano Comprado")]
        public string PlanoCompradoFilter { get; set; }
        [Display(Name = "Plano Adquirido")]
        public string PlanoAdquiridoFilter { get; set; }
        [Display(Name = "Local Microcomputador")]
        public string LocalMicrocomputadorFilter { get; set; }
    }
}
