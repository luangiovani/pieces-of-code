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
    /// Classe para Mapeamento da Tabela Compra, nesta tabela serão armazenadas as compras realizadas nas lojas pelos Colaboradores
    /// </summary>
    public class Compra
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
        /// Opção de Entrega que o Colaborador escolheu
        /// </summary>
        public string opcao_entrega_id { get; set; }

        /// <summary>
        /// Situação que a compra está
        /// </summary>
        public string situacao_compra_id { get; set; }

        /// <summary>
        /// Meio que ocorreu a compra
        /// </summary>
        public string meio_de_compra_id { get; set; }

        /// <summary>
        /// Justificativa adicionais para a compra
        /// </summary>
        public string justificativa { get; set; }

        /// <summary>
        /// Informações complementares para o Colaborador
        /// </summary>
        public string informacoes_complementares { get; set; }

        /// <summary>
        /// Colaborador que realizou a troca
        /// </summary>
        public string cs_colaborador { get; set; }

        /// <summary>
        /// Colaborador que atendeu ou vendeu na Loja
        /// </summary>
        public string cs_colaborador_loja { get; set; }

        /// <summary>
        /// Quantidade total de pontos da compra
        /// </summary>
        public decimal total_pontos { get; set; }

        /// <summary>
        /// Valor total da compra
        /// </summary>
        public decimal total_valor { get; set; }

        /// <summary>
        /// Taxa de conversão de pontos em valor
        /// </summary>
        public decimal taxa_conversao { get; set; }

        /// <summary>
        /// Id da Loja que o Colaborador escolheu para retirar o produto
        /// </summary>
        public string loja_id { get; set; }

        /// <summary>
        /// Indica se o registro está Ativo(1) ou Inativo(0)
        /// </summary>
        public bool ativo { get; set; }

        /// <summary>
        /// Data e Hora da Criação do registro
        /// </summary>
        public DateTime? data_hora_criacao { get; set; }

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