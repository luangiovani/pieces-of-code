using System;

namespace TAJ.Database.Entity
{
    public class Programacao
    {
        public int id_programacao { get; set; }
        public int id_filial { get; set; }
        public string dia_semana { get; set; }
        public string nome { get; set; }
        public string descritivo { get; set; }
        public DateTime dt_cadastro { get; set; }
        public virtual Filial filial { get; set; }
    }
}