using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class LastIncomingJobsViewModel
    {
        [Key]
        [Display(Name="# Order")]
        public decimal serviceOrderId { get; set; }

        [Display(Name="Customer")]
        public string customerName { get; set; }

        [Display(Name = "OS Series")]
        public string OS_Series { get; set; }

        [Display(Name = "Rush Service?")]
        public bool isRush { get; set; }
    }

    public class RecentInteractionsViewModel
    {
        [Key]
        [Display(Name = "# Order")]
        public decimal serviceOrderId { get; set; }

        [Display(Name = "OS Series")]
        public string OS_Series { get; set; }

        [Display(Name = "User")]
        public string userName { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date { get; set; }

        [Display(Name = "Customer")]
        public string customerName { get; set; }

        [Display(Name = "Location")]
        public string location { get; set; }

        [Display(Name = "Rush Service?")]
        public bool isRush { get; set; }
    }

    public class HomeViewModel
    {
        public HomeViewModel()
        {
            lastIncomingJobs = new List<LastIncomingJobsViewModel>();
            recentInteractions = new List<RecentInteractionsViewModel>();
            followUpToSend = new List<FollowUpMessagesToSendViewModel>();
        }
        [Display(Name = "New Jobs")]
        public int newJobs { get; set; }

        [Display(Name = "Delay Jobs")]
        public int delayJobs { get; set; }

        [Display(Name = "Unassigned Jobs")]
        public int unassignedJobs { get; set; }

        [Display(Name = "Go Ahead Jobs")]
        public int goAheadJobs { get; set; }

        [Display(Name = "Follow Up Jobs")]
        public int followUpJobs { get; set; }

        [Display(Name = "Wait Quoting")]
        public int waitQuotingJobs { get; set; }

        [Display(Name = "Quoted")]
        public int quotedJobs { get; set; }

        [Display(Name = "Orders in Rush")]
        public int rushOrders { get; set; }

        public AreaViewModel areasDash { get; set; }

        public List<LastIncomingJobsViewModel> lastIncomingJobs { get; set; }

        public List<RecentInteractionsViewModel> recentInteractions { get; set; }

        public List<FollowUpMessagesToSendViewModel> followUpToSend { get; set; }
    }
}
