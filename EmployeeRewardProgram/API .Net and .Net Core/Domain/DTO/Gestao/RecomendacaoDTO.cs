using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Domain.DTO.Loja;

namespace Domain.DTO.Gestao
{
    public class RecomendacaoDTO
    {
        public Guid? id { get; set; }

        [Required(ErrorMessage = "Informe o Colaborador que irá receber a Recomendação.")]
        public string cs { get; set; }

        [Required(ErrorMessage = "Informe o Colaborador que está solicitando a Recomendação.")]
        public string cs_solicitante { get; set; }

        [Required(ErrorMessage = "Informe uma justificativa para Recomendação.")]
        public string justificativa { get; set; }

        [Required(ErrorMessage = "Informe a quantidade de pontos para Recomendação")]
        public string qtde_pontos { get; set; }

        [Required(ErrorMessage = "Informe um tipo para Recomendação.")]
        public string tipo_recomendacao { get; set; }
    }

    public class DetalhesStatusRecomendacaoModelDTO
    {
        public DetalhesStatusRecomendacaoModelDTO()
        {
            Avaliacoes = new List<AvaliacaoDTO>();
        }

        public Guid id { get; set; }

        public int sequencial { get; set; }

        public string colaborador { get; set; }

        public string cs_colaborador { get; set; }

        public string cs_gestor_colaborador { get; set; }

        public string gestor_colaborador { get; set; }

        public string cs_gestor_solicitante { get; set; }

        public string gestor_solicitante { get; set; }

        public string status { get; set; }

        public string tipo_recomendacao { get; set; }

        public string qtde_pontos { get; set; }

        public string justificativa { get; set; }

        public IEnumerable<AvaliacaoDTO> Avaliacoes { get; set; }
    }

    public class IndicadorQuantitativoRecomendacoesDTO
    {
        public IndicadorQuantitativoRecomendacoesDTO()
        {
            ProdutosMaisTrocados = new List<ProdutosMaisTrocadosDTO>();
        }

        public int mes { get; set; }

        public int quantidade_mes { get; set; }

        public int aguardando { get; set; }

        public int aprovadas { get; set; }

        public virtual decimal media_dia_mes {
            get
            {
                return (Convert.ToDecimal(quantidade_mes) / Convert.ToDecimal(DateTime.Now.Day));
            }
        }

        public IEnumerable<ProdutosMaisTrocadosDTO> ProdutosMaisTrocados { get; set; }
    }

    public class RecomendacoesColaboradorDTO
    {
        public Guid id { get; set; }

        public string tipo_recomendacao { get; set; }

        public decimal pontos { get; set; }

        public string data { get; set; }

        public string cs_gestor_solicitante { get; set; }

        public string gestor_solicitante { get; set; }
    }
}
