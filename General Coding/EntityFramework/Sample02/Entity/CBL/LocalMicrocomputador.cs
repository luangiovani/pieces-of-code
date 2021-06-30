using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Database.Entity.CBL
{
    public class LocalMicrocomputador
    {
        public int idLocalMicrocomputador { get; set; }
        public string descricao { get; set; }
        public DateTime dataCadastro { get; set; }
        public bool indAtivo { get; set; }
        public string userRegistration_id { get; set; }
        public virtual ICollection<ServiceOrderCloud> serviceOrderClouds { get; set; }

    }
}
