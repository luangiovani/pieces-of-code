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
    /// Classe para Mapeamento da Tabela MenuDeNavegacao, nesta tabela serão armazenados os menus das aplicações
    /// </summary>
    public class MenuDeNavegacao
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
        /// Identificador único da aplicação a que este Menu pertentece
        /// </summary>
        public string aplicacao_id { get; set; }

        /// <summary>
        /// Identificador único do registro que é superior a este, quando houver
        /// </summary>
        public string menu_superior_id { get; set; }

        /// <summary>
        /// Nome ou Label deste Menu
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// Ordenação deste Menu para organizar em tela
        /// </summary>
        public int ordem { get; set; }

        /// <summary>
        /// Controladora que este menu irá redirecionar (quando houver)
        /// </summary>
        public string controller { get; set; }

        /// <summary>
        /// Ação que este menu irá executar (quando houver)
        /// </summary>
        public string acao { get; set; }

        /// <summary>
        /// Texto de ajuda do menu (quando houver)
        /// </summary>
        public string ajuda { get; set; }

        /// <summary>
        /// Ícone de exibição do menu (quando houver)
        /// </summary>
        public string icone { get; set; }

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
