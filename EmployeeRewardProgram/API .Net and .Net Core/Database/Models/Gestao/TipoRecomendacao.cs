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
    /// Classe para Mapeamento da TipoRecomendacao, nesta tabela serão armazenadas os tipos de recomendação para os Colaboradores
    /// </summary>
    public class TipoRecomendacao
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
        /// Nome do tipo de recomendação
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Descrição do Tipo de Recomendação
        /// </summary>
        public string descricao { get; set; }

        /// <summary>
        /// Tipo de pontuação 1 - Escolha ou 2 - Fixa
        /// </summary>
        public int tipo_pontuacao { get; set; }

        /// <summary>
        /// Quantidade fixa de pontuação que tem este Tipo de Recomendação
        /// </summary>
        public decimal pontos_fixos { get; set; }

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
