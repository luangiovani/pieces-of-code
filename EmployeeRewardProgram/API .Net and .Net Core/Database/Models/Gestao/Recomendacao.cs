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
    /// Classe para Mapeamento da tabela Recomendacao, nesta tabela serão armazenadas as recomendações para os Colaboradores
    /// </summary>
    public class Recomendacao
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
        /// Identificador do Colaborador recomendado
        /// </summary>
        public string cs_colaborador { get; set; }

        /// <summary>
        /// Identificador do Colaborador que solicitou a recomendação para o outro colaborador
        /// </summary>
        public string cs_colaborador_solicitante { get; set; }

        /// <summary>
        /// Indica se este colaborador é subordinado do solicitante, ou seja, se o Solicitante é o Gestor responsável pelo Colaborador
        /// </summary>
        public bool subordinado { get; set; }

        /// <summary>
        /// Identificador do Tipo de Recomendação
        /// </summary>
        public string tipo_recomendacao_id { get; set; }

        /// <summary>
        /// Informa uma justificativa para a avaliação
        /// </summary>
        public string justificativa { get; set; }

        /// <summary>
        /// Quantidade de Pontos para atribuição
        /// </summary>
        public decimal qtde_pontos { get; set; }

        /// <summary>
        /// Indica o status da Recomendação
        /// Em Análise (Ainda tem Avaliações Pendentes)
        /// Aprovada
        /// Reprovada
        /// </summary>
        public string situacao_recomendacao_id { get; set; }

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
