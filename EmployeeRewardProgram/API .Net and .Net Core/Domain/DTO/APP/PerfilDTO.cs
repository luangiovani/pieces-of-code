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
    /// Objeto de Transferência da Entidade Perfil
    /// </summary>
    public class PerfilDTO
    {
        public PerfilDTO()
        {
            listaPermissoesMenu = new List<PerfilMenuNavegacaoDTO>();
            listaMenus = new List<string>();
        }

        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

        /// <summary>
        /// Nome do perfil ou hierarquia exemplo: Administrador, Gestor, Técnico...
        /// </summary>
        [Required(ErrorMessage = "Informe um nome para o Perfil")]
        public string nome { get; set; }

        /// <summary>
        /// Descrição do perfil, uma breve informação ao usuário do que o perfil é ou o objetivo do perfil
        /// </summary>
        [Required(ErrorMessage = "Informe uma descrição para o Perfil")]
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

        public List<PerfilMenuNavegacaoDTO> listaPermissoesMenu { get; set; }

        public List<string> listaMenus { get; set; }
    }

    public class PerfilMenuNavegacaoDTO
    {
        /// <summary>
        /// Identificador único do registro na tabela
        /// </summary>
        public Guid? id { get; set; }

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
