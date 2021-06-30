using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Model.CBL
{
   public class ServiceOrderCloudViewModel
   {
      [Key]
      [Display(Name = "Service Order Cloud ID")]
      public int idServiceOrderCloud { get; set; }

      [Key]
      [Display(Name = "service Order ID")]
      public decimal serviceOrder_id { get; set; }

      [Key]
      [Display(Name = "Cloud ID")]
      public int idCloud { get; set; }

      [Key]
      [Display(Name = "Local Microcomputador ID")]
      public int idLocalMicrocomputador { get; set; }

      [Key]
      [Display(Name = "Vencimento ID")]
      public int idVencimentoCloud { get; set; }

      [Display(Name = "Data Envio Lista")]
      [ScaffoldColumn(false)]
      [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
      [Required(ErrorMessage = "Informe uma data de Envio da Lista")]
      public DateTime dtEnviaLista { get; set; }

      [Display(Name = "Data Liberação Link")]
      [ScaffoldColumn(false)]
      [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
      [Required(ErrorMessage = "Informe uma data de Liberação do Link")]
      public DateTime dtLiberacaoLinkCliente { get; set; }

      [Display(Name = "Data Bloqueio")]
      [ScaffoldColumn(false)]
      [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
      [Required(ErrorMessage = "Informe uma data de Bloqueio")]
      public DateTime dtBloqueio { get; set; }

      [Display(Name = "Usar Gratuidade?")]
      [Required(ErrorMessage = "Indique se usa gratuidade")]
      public bool indUsarGratuidade { get; set; }

      [Display(Name = "Tempo Gratuidade")]
      public int tempoGratuidade { get; set; }

      [Display(Name = "Usar Cópia?")]
      [Required(ErrorMessage = "Indique se usa Cópia")]
      public bool indCopia { get; set; }

      [Display(Name = "Usar Reindex?")]
      [Required(ErrorMessage = "Indique se usa Reindex")]
      public bool indReindex { get; set; }

      [Display(Name = "Usar Chow?")]
      [Required(ErrorMessage = "Indique se usa Chow")]
      public bool indChown { get; set; }

      [Display(Name = "Informações Adicionais")]
      public string informacoesAdicionais { get; set; }

      [Display(Name = "Register Date")]
      [ScaffoldColumn(false)]
      [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
      public DateTime dataCadastro { get; set; }

      public string userRegistration_id { get; set; }
      //public virtual ApplicationUser usuario { get; set; }
      public virtual CloudViewModel clouds { get; set; }

      public virtual LocalMicrocomputadorViewModel locaisMicrocomputador { get; set; }
      public virtual VencimentoCloudViewModel vencimentoCloud { get; set; }
   }

   /// <summary>
   /// View Model usado para carregar na tela de Service Order
   /// </summary>
   public class NewServiceOrderCloudViewModel : ServiceOrderCloudViewModel
   {
      public NewServiceOrderCloudViewModel()
      {
         serviceOrderClouds = new List<ServiceOrderCloudViewModel>();
      }
      public List<ServiceOrderCloudViewModel> serviceOrderClouds { get; set; }
   }
}
