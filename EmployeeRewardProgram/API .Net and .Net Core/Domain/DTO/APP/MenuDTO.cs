using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.APP
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Implementação dos Objetos para Abstração entre a Camada de Serviços e a Camada de Acesso a Dados
    /// </atividades>
    /// <summary>
    /// Objeto de Transferência da Entidade MenuDeNavegacao
    /// </summary>
    public class MenuDTO
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public MenuDTO()
        {
            SubMenus = new List<MenuDTO>();
        }

        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Sequencial do Registro no banco de dados
        /// </summary>
        public int sequencial { get; set; }

        /// <summary>
        /// Identificador único da aplicação a que este Menu pertentece
        /// </summary>
        [Required(ErrorMessage = "Informe de qual aplicação é este menu")]
        public string aplicacao_id { get; set; }

        /// <summary>
        /// Nome da aplicação a que este Menu pertentece
        /// </summary>
        public string aplicacao { get; set; }

        /// <summary>
        /// Identificador único do registro que é superior a este, quando houver
        /// </summary>
        public string menu_superior_id { get; set; }

        /// <summary>
        /// Nome ou Label do Menu que é superior deste
        /// </summary>
        public string nome_menu_superior { get; set; }

        /// <summary>
        /// Nome ou Label deste Menu
        /// </summary>
        [Required(ErrorMessage = "O nome ou label para o menu é obrigatório")]
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

        /// <summary>
        /// SubMenus do Menu
        /// </summary>
        public ICollection<MenuDTO> SubMenus { get; set; }
    }

    /// <summary>
    /// DTO para a Transferência das Opções de acesso a menus de opção de um colaborador
    /// </summary>
    public class MenuUsuarioDTO
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public MenuUsuarioDTO()
        {
            SubMenus = new List<MenuUsuarioDTO>();
        }

        /// <summary>
        /// Id da opção de menu
        /// </summary>
        public Guid menu_id { get; set; }

        /// <summary>
        /// Id da opção de menu superior a este item de menu
        /// </summary>
        public Guid? menu_superior_id { get; set; }

        /// <summary>
        /// Nome da opção de menu
        /// </summary>
        public string menu_opcao { get; set; }

        /// <summary>
        /// Ícone da opção do Menu
        /// </summary>
        public string icone { get; set; }

        /// <summary>
        /// Controller da opção de menu
        /// </summary>
        public string controller { get; set; }

        /// <summary>
        /// Ação da opção de menu
        /// </summary>
        public string acao { get; set; }

        /// <summary>
        /// Ordem da opção de Menu
        /// </summary>
        public int ordem { get; set; }

        /// <summary>
        /// Indica se este perfil pode visualizar nesta opção de Menu
        /// </summary>
        public bool visualizar { get; set; }

        /// <summary>
        /// Indica se este perfil pode cadastrar nesta opção de Menu
        /// </summary>
        public bool cadastrar { get; set; }

        /// <summary>
        /// Indica se este perfil pode excluir nesta opção de Menu
        /// </summary>
        public bool excluir { get; set; }

        /// <summary>
        /// Lista de SubMenus
        /// </summary>
        public IEnumerable<MenuUsuarioDTO> SubMenus { get; set; }
    }
}
