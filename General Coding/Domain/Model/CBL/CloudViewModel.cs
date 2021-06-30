using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Model.CBL
{
    public class CloudViewModel
    {
        public CloudViewModel()
        {
            vencimentosClouds = new List<VencimentoCloudViewModel>();
            serviceOrderClouds = new List<ServiceOrderCloudViewModel>();
            //vencimentosNew = new List<VencimentoNew>();
            tempoGratuidade = 0;
        }

        [Key]
        [Display(Name = "Cloud ID")]
        public int idCloud { get; set; }
        [Display(Name = "Descrição")]
        public string descricao { get; set; }
        [Display(Name = "Tamanho")]
        public int tamanho { get; set; }
        [Display(Name = "Medida")]
        public string unidade_tamanho { get; set; }
        [Display(Name = "Tempo Gratuidade")]
        public int tempoGratuidade { get; set; }
        public bool indAtivo { get; set; }
        public DateTime dataCadastro { get; set; }
        public string userRegistration_id { get; set; }
        //public virtual ApplicationUser user { get; set; }
        public virtual ICollection<VencimentoCloudViewModel> vencimentosClouds { get; set; }
        public virtual ICollection<ServiceOrderCloudViewModel> serviceOrderClouds { get; set; }

        //[ScaffoldColumn(false)]
        //public virtual ICollection<VencimentoNew> vencimentosNew { get; set; }
    }

    //public class VencimentoNew
    //{
    //    public string tipo { get; set; }
    //    public string dia { get; set; }
    //    public string valor { get; set; }
    //    public bool padrao { get; set; }
    //}
}
