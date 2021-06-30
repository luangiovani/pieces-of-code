using System.Collections.Generic;
namespace TAJ.Database.Entity
{
    public class Bairro
    {
        public int id_bairro { get; set; }
        public int id_cidade { get; set; }
        public string bairro { get; set; }
        public virtual Cidade cidade { get; set; }
        public virtual ICollection<Filial> filial { get; set; }
        public virtual ICollection<Cliente> cliente { get; set; }
    }
}