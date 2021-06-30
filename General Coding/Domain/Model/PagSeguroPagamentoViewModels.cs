using Framework.Domain.Model.CBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Model
{
    /// <summary>
    /// reverter as traduçoes das classes acima
    /// </summary>
    public class PagSeguroPagamentoViewModels
    {
        [Display(Name = "receiverEmail")]
        public string receiverEmail { get; set; }

        [Display(Name = "currency")]
        public string currency { get; set; }

        [Display(Name = "itemId1")]
        public string itemId1 { get; set; }

        [Display(Name = "itemDescription1")]
        public string itemDescription1 { get; set; }

        [Display(Name = "itemAmount1")]
        public string itemAmount1 { get; set; }

        [Display(Name = "itemQuantity1")]
        public string itemQuantity1 { get; set; }

        [Display(Name = "itemWeight1")]
        public string itemWeight1 { get; set; }

        [Display(Name = "reference")]
        public string reference { get; set; }

        [Display(Name = "senderName")]
        public string senderName { get; set; }

        [Display(Name = "senderAreaCode")]
        public string senderAreaCode { get; set; }

        [Display(Name = "senderPhone")]
        public string senderPhone { get; set; }

        [Display(Name = "senderEmail")]
        public string senderEmail { get; set; }

        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }


    }
    
}
