using System;

namespace Domain.DTO.Gestao
{
    public class ConfiguracaoExpiracaoPontosDTO
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
        /// Unidade de tempo de expiração
        /// </summary>
        public decimal? qtde_expiracao { get; set; }

        /// <summary>
        /// Tipo da Expiração dos pontos
        /// H horas, D dias, S semanas, M meses, A anos
        /// </summary>
        public string tipo_expiracao { get; set; }

        /// <summary>
        /// Unidade de tempo de expiração quando colaborador for desligado
        /// </summary>
        public decimal? qtde_expiracao_desligamento { get; set; }

        /// <summary>
        /// Tipo da Expiração dos pontos quando colaborador for desligado
        /// H horas, D dias, S semanas, M meses, A anos
        /// </summary>
        public string tipo_expiracao_desligamento { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }
    }
}
