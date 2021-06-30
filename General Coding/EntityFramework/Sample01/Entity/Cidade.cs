using System.Collections.Generic;

namespace TAJ.Database.Entity
{
    public class Cidade
    {
        public int id_cidade { get; set; }
        public int id_estado { get; set; }
        public string cidade { get; set; }
        public virtual Estado estado { get; set; }
        public ICollection<Bairro> bairro { get; set; }
    }
}