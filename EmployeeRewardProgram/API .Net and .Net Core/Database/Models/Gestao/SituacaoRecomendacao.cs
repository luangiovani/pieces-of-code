using System;

namespace Database.Models.Gestao
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Mapeamento de Modelo Lógico
    /// </atividades>
    /// <summary>
    /// Classe para Mapeamento da tabela Situacao Recomendacao
    /// Aprovada  = Recomendação aprovada e pontos contabilizados para o Colaborador
    /// Reprovada = Pontos não contabilizados para o Colaborador
    /// Em Análise = Com Avaliações Pendentes
    /// </summary>
    public class SituacaoRecomendacao
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Sequencial do registro na tabela (ordenação)
        /// </summary>
        public int sequencial { get; set; }

        /// <summary>
        /// Nome da Situacao da Recomendação, ou seja, as próprias Situacoes
        /// Em Análise = Aguardando análise do(s) Avaliador(es)
        /// Aprovada = Avaliação aprovada
        /// Reprovada = Avaliação reprovada
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Descrição para Situacao da Recomendação
        /// </summary>
        public string descricao { get; set; }

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
