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
    ///     Classe para Mapeamento da Tabela SituacaoCompra, nesta tabela serão armazenadas as situações que a compra pode ter
    ///     1 - Solicitação de Troca = Quando o Colaborador inicia o processo de troca, escolhe os produtos, pensa, e após confirma
    ///     2 - Em Análise da Loja = Quando a Loja recebe uma Solicitação de Troca, avalia a Opção de entrega, verifica o estoque e confirma ou não
    ///         (Trocas Pendentes)
    ///     3 - Efetivada = Confirmada a troca pela loja e aguarda o envio ou retirada do Colaborador
    ///         (Trocas Pendentes de Envio)
    ///     4 - Finalizada = Quando a loja providencia a entrega ou quando o colaborador recebe a mercadoria de alguma forma.
    ///     5 - Cancelada =  Quando a Loja informa que não há em estoque (Recusada por falta em estoque) ou que não tem como 
	///         realizar a troca e o cancelamento é efetivado ou quando o colaborador efetua o cancelamento enquanto não está Finalizada.
    ///     6 - Recusada por falta em estoque = A Loja informa o colaborador que não tem como efetuar a troca uma vez que não tem em estoque.
    /// </summary>
    public class SituacaoCompra
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
        /// Descrição da Situação da Compra
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