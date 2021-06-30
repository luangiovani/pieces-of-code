using System;

namespace TAJ.Database.Entity
{
    public class Promocao
    {
        public int id_promocao { get; set; }
        public int id_filial { get; set; }
        public string tipo { get; set; }
        public string nome { get; set; }
        public string descritivo { get; set; }
        public string imagem { get; set; }
        public DateTime dt_cadastro { get; set; }
        public virtual Filial filial { get; set; }
        public DateTime? dataHoraPush { get; set; }
        public string tags { get; set; }
        public bool pushEnviado { get; set; }
    }
}