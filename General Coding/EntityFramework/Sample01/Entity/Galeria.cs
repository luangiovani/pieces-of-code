using System;
using System.Collections.Generic;

namespace TAJ.Database.Entity
{
    public class Galeria
    {
        public Guid id_galeria { get; set; }
        public int id_filial { get; set; }
        public string titulo { get; set; }
        public string descritivo { get; set; }
        public DateTime data { get; set; }
        public DateTime dt_cadastro { get; set; }
        public virtual Filial filial { get; set; }
        public virtual ICollection<Foto> foto { get; set; }
    }
}