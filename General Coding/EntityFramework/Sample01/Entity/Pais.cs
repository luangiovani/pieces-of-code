using System.Collections.Generic;

namespace TAJ.Database.Entity
{
    public class Pais
    {
        public int id_pais { get; set; }
        public string pais { get; set; }
        public string sigla { get; set; }
        public ICollection<Estado> estado { get; set; }
    }
}