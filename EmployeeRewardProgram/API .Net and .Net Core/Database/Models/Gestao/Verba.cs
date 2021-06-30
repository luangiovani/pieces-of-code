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
    /// Classe para Mapeamento da Tabela Verba, nesta tabela serão armazenadas as verbas dos colaboradores gestores
    /// </summary>
    public class Verba
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
        /// Identificador do colaborador gestor desta verba
        /// </summary>
        public string cs_colaborador { get; set; }

        /// <summary>
        /// Quantidade de pontos
        /// </summary>
        public decimal valor_pontos { get; set; }

        /// <summary>
        /// Data que foi atribuido os pontos
        /// </summary>
        public DateTime data_atribuicao { get; set; }

        /// <summary>
        /// Observações
        /// </summary>
        public string observacao { get; set; }

        /// <summary>
        /// Valor em moeda
        /// </summary>
        public decimal valor_moeda { get; set; }

        /// <summary>
        /// Taxa de conversão entre moeda e TKU
        /// </summary>
        public decimal taxa_conversao { get; set; }

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
