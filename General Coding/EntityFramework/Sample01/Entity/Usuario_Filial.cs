using System;

namespace TAJ.Database.Entity
{
    public class Usuario_Filial
    {
        public string id_usuario { get; set; }
        public int id_filial { get; set; }
        public DateTime dt_cadastro { get; set; }
        public virtual ApplicationUser usuario { get; set; }
        public virtual Filial filial { get; set; }
    }
}