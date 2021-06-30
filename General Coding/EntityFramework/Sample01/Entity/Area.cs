using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TAJ.Database.Entity
{
    public class Area
    {
        public Guid id_area { get; set; }
        public Guid? id_area_mae { get; set; }
        public string nome { get; set; }
        public int ordem { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string help { get; set; }
        public bool ativo { get; set; }
        public DateTime dt_cadastro { get; set; }
        public virtual Area area_mae { get; set; }
        public virtual ICollection<Area> area_filha { get; set; }
        public virtual ICollection<Perfil_Area> perfil_area { get; set; }
    }
}