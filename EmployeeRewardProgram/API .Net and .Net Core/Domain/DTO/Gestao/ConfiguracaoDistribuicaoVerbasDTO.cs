using System;

namespace Domain.DTO.Gestao
{
    public class ConfiguracaoDistribuicaoVerbasDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Sequencial do registro na tabela (ordenação)
        /// </summary>
        public int sequencial { get; set; }

        /// <summary>
        /// Pontuação mínima a ser distribuída de Verba aos Gestores
        /// </summary>
        public decimal? pontos_minimos { get; set; }

        /// <summary>
        /// Pontuação por Colaborador para Distruibuição de Verba aos Gestores
        /// </summary>
        public decimal? pontos_por_colaborador { get; set; }

        /// <summary>
        /// Pontuação por Área para Distribuiçãode Verba aos Gestores
        /// </summary>
        public decimal? pontos_por_area { get; set; }

        /// <summary>
        /// Data que a Verba passa a estar disponível
        /// </summary>
        public string dt_disponivel { get; set; }

        /// <summary>
        /// Data que a Verba passa a estar bloqueada
        /// </summary>
        public string dt_bloquear { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }
    }
}
