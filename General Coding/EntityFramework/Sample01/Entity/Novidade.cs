using System;

namespace TAJ.Database.Entity
{
    public class Novidade
    {
        public int id_novidade { get; set; }
        public int id_filial { get; set; }
        public string titulo { get; set; }
        public string descritivo { get; set; }
        public string imagem { get; set; }
        public DateTime dt_cadastro { get; set; }
        public string id_post_facebook { get; set; }
        public bool ver_no_mobile { get; set; }
        public virtual Filial filial { get; set; }
    }
}