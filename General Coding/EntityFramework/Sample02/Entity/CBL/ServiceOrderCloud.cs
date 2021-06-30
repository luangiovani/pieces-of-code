using System;
using System.Collections.Generic;

namespace Framework.Database.Entity.CBL
{
   public class ServiceOrderCloud
   {
      public int idServiceOrderCloud { get; set; }
      public decimal serviceOrder_id { get; set; }
      public int idCloud { get; set; }
      public int idLocalMicrocomputador { get; set; }
      public int idVencimentoCloud { get; set; }
      public DateTime dtEnviaLista { get; set; }
      public DateTime dtLiberacaoLinkCliente { get; set; }
      public DateTime dtBloqueio { get; set; }
      public bool indUsarGratuidade { get; set; }
      public int tempoGratuidade { get; set; }
      public bool indCopia { get; set; }
      public bool indReindex { get; set; }
      public bool indChown { get; set; }
      public string informacoesAdicionais { get; set; }
      public DateTime dataCadastro { get; set; }
      public string userRegistration_id { get; set; }
      //public virtual ApplicationUser usuario { get; set; }
      public virtual Cloud clouds { get; set; }

      public virtual LocalMicrocomputador locaisMicrocomputador { get; set; }
      public virtual VencimentosCloud vencimentoCloud { get; set; }
   }
}
