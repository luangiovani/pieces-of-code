using System;
using System.Collections.Generic;

namespace TAJ.Database.Entity
{
    public class Filial
    {
        public int id_filial { get; set; }
        public int id_bairro { get; set; }
        public string nome { get; set; }
        public string cep { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public string descricao { get; set; }
        public DateTime dt_cadastro { get; set; }
        public string id_facebook { get; set; }
        public string telefonereserva { get; set; }
        public string textoreserva { get; set; }
        public string emailreserva { get; set; }
        public string url_galeria { get; set; }
        public virtual ICollection<Usuario_Filial> usuario_filial { get; set; }
        public virtual ICollection<Programacao> programacao { get; set; }
        public virtual ICollection<Promocao> promocao { get; set; }
        public virtual ICollection<Pharmacy> pharmacy { get; set; }
        public virtual ICollection<Galeria> galeria { get; set; }
        public virtual ICollection<Novidade> novidade { get; set; }
        public virtual Bairro bairro { get; set; }
    }
}