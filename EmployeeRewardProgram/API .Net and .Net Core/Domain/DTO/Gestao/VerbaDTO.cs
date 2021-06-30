using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Gestao
{
    public class VerbaDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Sequencial do registro na tabela (ordenação)
        /// </summary>
        public int sequencial { get; set; }

        [Required(ErrorMessage = "Informe o Gestor para Atribuição de Verba")]
        /// <summary>
        /// Identificador do colaborador gestor desta verba
        /// </summary>
        public string cs_colaborador { get; set; }

        /// <summary>
        /// Nome do colaborador gestor desta verba
        /// </summary>
        public string gestor { get; set; }

        [Required(ErrorMessage = "Informe os pontos para Atribuição de Verba")]
        /// <summary>
        /// Quantidade de pontos
        /// </summary>
        public decimal valor_pontos { get; set; }

        /// <summary>
        /// Data que foi atribuido os pontos
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// Observações
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// Valor em moeda
        /// </summary>
        public decimal valor_moeda { get; set; }

        /// <summary>
        /// Taxa de conversão entre moeda e Pontos no momento da atribuição
        /// </summary>
        public decimal taxa_conversao { get; set; }

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
}
