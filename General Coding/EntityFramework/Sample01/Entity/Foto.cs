using System;

namespace TAJ.Database.Entity
{
    public class Foto
    {
        public Guid id_foto { get; set; }
        public Guid id_galeria { get; set; }
        public string url { get; set; }
        public bool aprovado { get; set; }
        public DateTime dt_cadastro { get; set; }
        public virtual Galeria galeria { get; set; }
    }
}