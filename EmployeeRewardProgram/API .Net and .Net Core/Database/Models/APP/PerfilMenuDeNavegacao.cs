using System;

namespace Database.Models.APP
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
    /// Classe para Mapeamento da Tabela PerfilMenuDeNavegacao, nesta tabela serão armazenados as permissões de Perfil para Menu
    /// </summary>
    public class PerfilMenuDeNavegacao
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
        /// Identificador do Perfil
        /// </summary>
        public string perfil_id { get; set; }

        /// <summary>
        /// Identificador do Menu que este Perfil terá acesso
        /// </summary>
        public string menu_navegacao_id { get; set; }

        /// <summary>
        /// Ação de Visualizar informações para este perfil neste menu
        /// </summary>
        public bool visualizar { get; set; }

        /// <summary>
        /// Ações de Cadastrar e Atualizar para este perfil neste menu
        /// </summary>
        public bool cadastrar { get; set; }

        /// <summary>
        /// Ação de Excluir (Inativar) para este perfil neste menu
        /// </summary>
        public bool excluir { get; set; }

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
