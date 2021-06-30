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
    /// Classe para Mapeamento da tabela ConfiguracaoDistribuicaoVerbas, nesta tabela serão armazenadas as 
    /// Configuração para distribuição automática de pontos de Verbas para Colaboradores
    /// </summary>
    public class ConfiguracaoDistribuicaoVerbas
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
        /// Pontuação mínima a ser distribuída de Verba aos Gestores
        /// </summary>
        public decimal pontos_minimos { get; set; }

        /// <summary>
        /// Pontuação por Colaborador para Distruibuição de Verba aos Gestores
        /// </summary>
        public decimal pontos_por_colaborador { get; set; }

        /// <summary>
        /// Pontuação por Área para Distribuiçãode Verba aos Gestores
        /// </summary>
        public decimal pontos_por_area { get; set; }

        /// <summary>
        /// Data que a Verba passa a estar disponível
        /// </summary>
        public DateTime? disponivel_apartir { get; set; }

        /// <summary>
        /// Data que a Verba passa a estar bloqueada
        /// </summary>
        public DateTime? bloquear_apartir { get; set; }

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
