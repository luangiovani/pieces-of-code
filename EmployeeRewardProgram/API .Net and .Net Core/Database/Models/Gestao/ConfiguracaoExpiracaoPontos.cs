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
    /// Classe para Mapeamento da tabela ConfiguracaoExpiracaoPontos, nesta tabela serão armazenadas as 
    /// Configuração para expiração automática de pontos de Colaboradores
    public class ConfiguracaoExpiracaoPontos
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
        /// Unidade de tempo de expiração
        /// </summary>
        public decimal qtde_expiracao { get; set; }

        /// <summary>
        /// Tipo da Expiração dos pontos
        /// H horas, D dias, S semanas, M meses, A anos
        /// </summary>
        public string tipo_expiracao { get; set; }

        /// <summary>
        /// Unidade de tempo de expiração quando colaborador for desligado
        /// </summary>
        public decimal qtde_expiracao_desligamento { get; set; }

        /// <summary>
        /// Tipo da Expiração dos pontos quando colaborador for desligado
        /// H horas, D dias, S semanas, M meses, A anos
        /// </summary>
        public string tipo_expiracao_desligamento { get; set; }

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
