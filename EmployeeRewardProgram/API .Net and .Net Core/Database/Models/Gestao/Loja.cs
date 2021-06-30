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
    /// Classe para Mapeamento da Tabela Loja
    /// </summary>
    public class Lojas
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
        /// Nome da Loja
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Status da Loja
        /// </summary>
        public string status_loja { get; set; }

        /// <summary>
        /// Código da Loja
        /// </summary>
        public string codigo { get; set; }

        /// <summary>
        /// Observações da Loja
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// Complemento da Loja
        /// </summary>
        public string complemento { get; set; }

        /// <summary>
        /// Localização da Loja
        /// </summary>
        public string localizacao { get; set; }

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
