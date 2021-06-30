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
    /// Classe para Mapeamento da Tabela LogTransacoes, nesta tabela serão armazenadas os logs das transações das operacoes realizadas no sistema
    /// </summary>
    public class LogTransacoes
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
        /// Operação a que esta transação pertence
        /// </summary>
        public string log_operacao_id { get; set; }

        /// <summary>
        /// Data e Hora que iniciou a operação
        /// </summary>
        public DateTime data_hora_inicio { get; set; }

        /// <summary>
        /// Data e Hora que Finalizou a operação
        /// </summary>
        public DateTime? data_hora_fim { get; set; }

        /// <summary>
        /// Comando SQL da transação
        /// </summary>
        public string transacao { get; set; }

        /// <summary>
        /// Observações da Transação
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// Indicador se ocorreu algum erro na transação
        /// </summary>
        public string erro { get; set; }
    }
}
