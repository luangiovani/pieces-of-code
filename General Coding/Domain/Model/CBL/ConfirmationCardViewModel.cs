using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class ConfirmationCardViewModel
    {
        public ConfirmationCardViewModel()
        {
            Medias = new List<ConfirmationCardMediasViewModel>();
        }

        public string ServiceOrderId { get; set; }
        public string ServiceOrderDate { get; set; }

        public string LocationName { get; set; }
        public string LocationCnpj { get; set; }
        public string LocationIE { get; set; }
        public string LocationAddress { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationCEP { get; set; }
        public string LocationTollFree { get; set; }
        public string LocationWebSite { get; set; }
        public string LocationPhone { get; set; }
        public string LocationExtension { get; set; }
        public string LocationSacContactName { get; set; }
        public string LocationSacContactEmail { get; set; }

        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerContactCity { get; set; }
        public string CustomerContactState { get; set; }
        public string CustomerContactCEP { get; set; }
        public string CustomerContactCountry { get; set; }
        public string CustomerContactName { get; set; }
        public string CustomerContactTel { get; set; }
        public string CustomerContactExtension { get; set; }
        public string CustomerContactFax { get; set; }
        public string CustomerContactMobile { get; set; }
        public string CustomerContactEmail { get; set; }

        public virtual List<ConfirmationCardMediasViewModel> Medias { get; set; }
    }

    public class ConfirmationCardMediasViewModel
    {
        public int Order { get; set; }
	    public string ManufacturerName { get; set; }
	    public string Model { get; set; }
        public string Serial_No { get; set; }
    }
}
