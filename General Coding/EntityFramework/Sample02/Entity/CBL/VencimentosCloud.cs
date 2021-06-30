using System;
using System.Collections.Generic;

namespace Framework.Database.Entity.CBL
{
    public class VencimentosCloud
    {
        public int idVencimentosCloud { get; set; }
        public int idCloud { get; set; }
        public string tipo { get; set; }
        public int dias { get; set; }
        public decimal valor { get; set; }
        public bool indPadrao { get; set; }
        public virtual Cloud cloud { get; set; }
        public virtual ICollection<ServiceOrderCloud> serviceOrderClouds { get; set; }
    }
}
