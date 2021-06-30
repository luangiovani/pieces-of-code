using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class Cloud
    {
        public int idCloud{ get; set; }
        public string descricao { get; set; }
        public int tamanho { get; set; }
        public string unidade_tamanho { get; set; }
        public int tempoGratuidade { get; set; }
        public bool indAtivo { get; set; }
        public DateTime dataCadastro { get; set; }
        public string userRegistration_id { get; set; }
        //public virtual ApplicationUser user { get; set; }
        public virtual ICollection<VencimentosCloud> vencimentosClouds { get; set; }
        public virtual ICollection<ServiceOrderCloud> serviceOrderClouds { get; set; }



    }
}
