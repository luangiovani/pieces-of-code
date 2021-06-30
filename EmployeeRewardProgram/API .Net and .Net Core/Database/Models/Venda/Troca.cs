using System;

namespace Database.Models.Venda
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
    /// Classe para Mapeamento da tabela de Troca, nesta tabela serão armazenadas trocas que ocorrerem em uma compra
    /// </summary>
    public class Troca
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
        /// Identificador da Compra desta troca
        /// </summary>
        public string compra_id { get; set; }

        /// <summary>
        /// Identificador do item trocado
        /// </summary>
        public string item_compra_troca_id { get; set; }

        /// <summary>
        /// Identificador do item novo
        /// </summary>
        public string item_compra_novo_id { get; set; }

        /// <summary>
        /// Identificador da Situação da Troca
        /// </summary>
        public string situacao_troca_id { get; set; }

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
