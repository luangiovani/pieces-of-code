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
    /// Classe para Mapeamento da tabela Avaliacao, nesta tabela serão armazenadas as aprovações/reprovações de recomendações para os Colaboradores
    /// </summary>
    public class Avaliacao
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
        /// Identificador da Recomendação que esta avaliação pertence
        /// </summary>
        public string recomendacao_id { get; set; }

        /// <summary>
        /// Identificador do colaborador gestor que irá aprovar/reprovar a recomendação
        /// </summary>
        public string cs_colaborador_avaliador { get; set; }

        /// <summary>
        /// Email do Colaborador Avaliador
        /// </summary>
        public string email_colaborador_avaliador { get; set; }

        /// <summary>
        /// Status da Avaliação
        /// </summary>
        public string situacao_avaliacao_id { get; set; }

        /// <summary>
        /// Data que foi efetuada a avaliação
        /// </summary>
        public DateTime? data_avaliacao { get; set; }

        /// <summary>
        /// Informa uma justificativa para a avaliação
        /// </summary>
        public string justificativa { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }

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
