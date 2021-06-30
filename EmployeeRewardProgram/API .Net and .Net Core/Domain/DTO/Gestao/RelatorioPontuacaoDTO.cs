using System.Collections.Generic;

namespace Domain.DTO.Gestao
{
    public class RelatorioPontuacaoDTO
    {
        public RelatorioPontuacaoDTO()
        {
            Relatorio = new List<DetalhesStatusRecomendacaoModelDTO>();
        }

        public int sequencial { get; set; }

        public string data { get; set; }

        public string colaborador { get; set; }

        public string cs_colaborador { get; set; }

        public string gestor_colaborador { get; set; }

        public string cs_gestor_colaborador { get; set; }

        public string tipo_recomendacao { get; set; }

        public string qtde_pontos { get; set; }

        public string trocas { get; set; }

        public virtual IEnumerable<DetalhesStatusRecomendacaoModelDTO> Relatorio { get; set; }
    }
}
