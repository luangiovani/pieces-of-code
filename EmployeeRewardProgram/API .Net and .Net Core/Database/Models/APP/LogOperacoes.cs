using System;

namespace Database.Models.APP
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
    /// Classe para Mapeamento da Tabela LogOperacoes, nesta tabela serão armazenadas os logs das operacoes realizadas no sistema
    /// </summary>
    public class LogOperacoes
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
        /// Colaborador que está logado
        /// </summary>
        public string cs_colaborador { get; set; }

        /// <summary>
        /// Indicador da aplicação em que foi realizada a operação
        /// </summary>
        public string aplicacao_id { get; set; }

        /// <summary>
        /// Data e Hora que iniciou a operação
        /// </summary>
        public DateTime data_hora_inicio { get; set; }

        /// <summary>
        /// Data e Hora que Finalizou a operação
        /// </summary>
        public DateTime? data_hora_fim { get; set; }

        /// <summary>
        /// Referência da Operação que está sendo executada
        /// </summary>
        public string operacao { get; set; }

        /// <summary>
        /// Observação da Operação
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// Indicador se ocorreu algum erro na operação
        /// </summary>
        public string erro { get; set; }
    }
}
