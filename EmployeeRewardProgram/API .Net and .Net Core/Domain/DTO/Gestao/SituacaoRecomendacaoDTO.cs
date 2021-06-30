using System;

namespace Domain.DTO.Gestao
{
    /// <summary>
    /// DTO com informações para informações das Recomendações recebidas ou realizadas
    /// </summary>
    public class SituacaoRecomendacaoDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Sequencial do registro na tabela (ordenação)
        /// </summary>
        public int sequencial { get; set; }

        public decimal qtde_pontos { get; set; }

        /// <summary>
        /// Nome da Situacao da Avaliacao, ou seja, as próprias Situacoes
        /// Pendente = Aguardando análise do Avaliador
        /// Aprovada = Avaliação aprovada
        /// Reprovada = Avaliação reprovada
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Descrição para Situacao da Avaliacao
        /// </summary>
        public string descricao { get; set; }

        /// <summary>
        /// CS do Colaborador que Recebeu a Recomendação
        /// </summary>
        public string cs_colaborador { get; set; }

        /// <summary>
        /// Nome do Colaborador que Recebeu a Recomendação
        /// </summary>
        public string colaborador { get; set; }

        /// <summary>
        /// Nome do Gestor do Colaborador que Recebeu a Recomendação
        /// </summary>
        public string gestor { get; set; }

        /// <summary>
        /// Status que está a Recomendação
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Motivo da Recomendação para o Colaborador, Tipo de Recomendação
        /// </summary>
        public string motivo { get; set; }

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
