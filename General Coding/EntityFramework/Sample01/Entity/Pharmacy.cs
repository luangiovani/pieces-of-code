using System;

namespace TAJ.Database.Entity
{
    public class Pharmacy
    {
        public int id_pharmacy { get; set; }
        public int id_filial { get; set; }
        public string nome { get; set; }
        public string descritivo { get; set; }
        public string imagem { get; set; }
        public DateTime dt_cadastro { get; set; }
        public virtual Filial filial { get; set; } 
    }
}