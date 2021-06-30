using System;

namespace Domain.DTO.Gestao
{
    public class TaxaConversaoDTO
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
        /// Total de Moeda para conversão em TKU'S  
        /// </summary>
        public decimal valor_moeda { get; set; }

        /// <summary>
        /// Fator multiplicador para conversão de TKU's em moeda
        /// </summary>
        public decimal fator { get; set; }

        /// <summary>
        /// Nome da taxa de conversão
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }

        /// <summary>
        /// Data e Hora da Criação do registro
        /// </summary>
        public DateTime data_hora_criacao { get; set; }

        /// <summary>
        /// Usuário de Criação do registro
        /// </summary>
        public string cs_colaborador_criacao { get; set; }

        /// <summary>
        /// Data e Hora da Alteração do registro
        /// </summary>
        public DateTime? data_hora_alteracao { get; set; }

        /// <summary>
        /// Usuário de Alteração do registro
        /// </summary>
        public string cs_colaborador_alteracao { get; set; }

    }

    public class TaxaConversaoPostDTO
    {
        public decimal vlrTaxa { get; set; }
    }
}
