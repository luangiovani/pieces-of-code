using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
    public class VencimentoCloudViewModel
    {
        [Key]
        [Display(Name = "Cloud ID")]
        public int idVencimentosCloud { get; set; }
        public int idCloud { get; set; }
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
        [Display(Name = "Dias")]
        public int dias { get; set; }
        [Display(Name = "Valor")]
        public decimal valor { get; set; }
        [Display(Name = "Padrão")]
        public bool indPadrao { get; set; }
        public virtual CloudViewModel cloud { get; set; }
        public virtual ICollection<ServiceOrderCloudViewModel> serviceOrderClouds { get; set; }
    }
}
