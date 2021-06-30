using System.Collections.Generic;

namespace TAJ.Database.Entity
{
    public class Estado
    {
        public int id_estado { get; set; }
        public int id_pais { get; set; }
        public string estado { get; set; }
        public string sigla { get; set; }
        public virtual Pais pais { get; set; }
        public ICollection<Cidade> cidade { get; set; }
    }
}