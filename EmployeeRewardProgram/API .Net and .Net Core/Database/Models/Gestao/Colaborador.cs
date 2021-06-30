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
    /// Classe para Mapeamento da Tabela Colaborador, nesta tabela serão armazenados os colaboradores
    /// </summary>
    public class Colaborador
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
        /// Código CS do colaborador (Id)
        /// </summary>
        public string cs { get; set; }

        /// <summary>
        /// Código do superior imediato do colaborador
        /// </summary>
        public string cs_superior_imediato { get; set; }

        /// <summary>
        /// Identificador do cargo do colaborador
        /// </summary>
        public string cd_cargo { get; set; }

        /// <summary>
        /// UO do Colaborador
        /// </summary>
        public string UO { get; set; }

        /// <summary>
        /// Identificador do Perfil do colaborador
        /// Liga o perfil automaticamente de acordo com o cargo
        /// Perfil no MySql é a tabela Gestor, após ajustar para ver como ficarão, de acordo com os níveis
        /// </summary>
        public string perfil_id { get; set; }

        /// <summary>
        /// Nomenclatura do Perfil do Colaborador
        /// </summary>
        public string perfil { get; set; }

        /// <summary>
        /// Nome do Colaborador
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Email principal do colaborador
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Data em que o colaborador iniciou suas atividades na empresa
        /// </summary>
        public DateTime data_admissao { get; set; }

        /// <summary>
        /// Unidade em que o colaborador atua ou é alocado
        /// </summary>
        public string local_trabalho { get; set; }

        /// <summary>
        /// Quantidade de pontos que o Colaborador possui para trocas
        /// </summary>
        public decimal quantidade_pontos { get; set; }

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

        #region Helpers

        /// <summary>
        /// Nomenclatura do Cargo
        /// </summary>
        public virtual string cargo { get; set; }

        /// <summary>
        /// Indica se o cargo do colaborador é elegível pela regra (Sim/Não)
        /// </summary>
        public virtual string elegivel { get; set; }

        /// <summary>
        /// Loja do Colaborador
        /// </summary>
        public virtual string loja_id { get; set; }

        #endregion
    }
}
