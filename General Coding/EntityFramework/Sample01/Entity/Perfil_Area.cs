using System;
using System.Collections.Generic;

namespace TAJ.Database.Entity
{
    public class Perfil_Area
    {
        public string id_perfil { get; set; }
        public Guid id_area { get; set; }
        public bool ind_visualizar { get; set; }
        public bool ind_cadastrar { get; set; }
        public bool ind_excluir { get; set; }
        public DateTime dt_cadastro { get; set; }
        public virtual ApplicationRole perfil { get; set; }
        public virtual Area area { get; set; }
    }
}