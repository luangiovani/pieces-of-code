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
    /// Classe para Mapeamento da Tabela ItemCompra, nesta tabela serão armazenadas os itens das compras realizadas nas lojas pelos Colaboradores
    /// </summary>
    public class ItemCompra
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
        /// Identificador da Compra
        /// </summary>
        public string compra_id { get; set; }

        /// <summary>
        /// Nome do Produto
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Descricao do Produto
        /// </summary>
        public string descricao { get; set; }

        /// <summary>
        /// Valor do Produto em pontos TKU
        /// </summary>
        public decimal valor_pontos { get; set; }

        /// <summary>
        /// Valor em Dinheiro do Produto
        /// </summary>
        public decimal valor_monetario { get; set; }

        /// <summary>
        /// Observações do Produto
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// Id do produto que o colaborador escolheu
        /// </summary>
        public string produto_id { get; set; }

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
