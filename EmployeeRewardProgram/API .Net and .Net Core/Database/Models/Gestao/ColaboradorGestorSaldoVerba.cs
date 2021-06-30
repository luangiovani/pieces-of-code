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
    /// Classe para Mapeamento da tabela ColaboradorGestorSaldoVerba, nesta tabela será armazenado o Saldo de Pontos dos Gestores
    /// </summary>
    public class ColaboradorGestorSaldoVerba
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
        /// Identificador do colaborador gestor
        /// </summary>
        public string cs_colaborador_gestor { get; set; }

        /// <summary>
        /// Quantidade de Pontos de Saldo de Verba para o Gestor
        /// </summary>
        public decimal quantidade_pontos { get; set; }

        /// <summary>
        /// Data e Hora da Criação do registro
        /// </summary>
        public DateTime? data_hora_criacao { get; set; }

        /// <summary>
        /// Usuário de Criação do registro
        /// </summary>
        public string cs_colaborador_criacao { get; set; }
    }
}
