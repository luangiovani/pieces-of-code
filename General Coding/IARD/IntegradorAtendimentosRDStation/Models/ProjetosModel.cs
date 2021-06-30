using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace IntegradorAtendimentosRDStation.Models
{
    public class ProjetosModel
    {
        [DisplayName("Projeto")]
        public string Nome { get; set; }
        [DisplayName("CODSOL")]
        public string CODSOL { get; set; }
        [DisplayName("Cód. Projeto")]
        public string COD_PROJETO_SGE { get; set; }
        [DisplayName("Ações")]
        public IList<AcoesModel> Acoes { get; set; }
    }
}