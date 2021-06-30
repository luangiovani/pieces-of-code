using Database.Models.APP;
using Domain.DTO.APP;
using Domain.Repositories.APP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services.APP
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Service do Middleware para operações entre Front e Backend
    /// </atividades>
    /// <summary>
    /// Implementação da Interface de Service para chamadas das operações de banco de dados
    /// </summary>
    public class MenuDeNavegacaoService : BaseService<MenuDeNavegacao>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly MenuDeNavegacaoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public MenuDeNavegacaoService(MenuDeNavegacaoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastrar um novo Menu ou nova opção de Menu no banco de dados
        /// </summary>
        /// <param name="dto">Objeto de Menu para manipulação de registros de Menu</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado no sistema</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto Menu cadastrado</returns>
        public MenuDTO Gravar(MenuDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (!string .IsNullOrEmpty(dto.nome) && !string .IsNullOrEmpty(cs_colaborador_logado) && string.IsNullOrEmpty(dto.id.ToString()))
            {
                MenuDeNavegacao objMenu = new MenuDeNavegacao()
                {
                    aplicacao_id = dto.aplicacao_id,
                    menu_superior_id = dto.menu_superior_id,
                    nome = dto.nome,
                    ordem = dto.ordem,
                    controller = dto.controller,
                    acao = dto.acao,
                    ajuda = dto.ajuda,
                    icone = dto.icone,
                    ativo = dto.ativo,
                    data_hora_criacao = DateTime.Now,
                    cs_colaborador_criacao = dto.cs_colaborador_criacao,
                    data_hora_alteracao = null,
                    cs_colaborador_alteracao = null
                };

                dto.id = _repository.Add(objMenu, logOperacaoId).id;
                return dto;
            }
            else if (!string .IsNullOrEmpty(dto.nome) && !string .IsNullOrEmpty(cs_colaborador_logado) && !string.IsNullOrEmpty(dto.id.ToString()))
            {

                var objMenu = _repository.FindByID(dto.id.ToString());

                if (objMenu != null)
                {
                    objMenu.acao = dto.acao;
                    objMenu.ajuda = dto.ajuda;
                    objMenu.aplicacao_id = dto.aplicacao_id;
                    objMenu.ativo = dto.ativo;
                    objMenu.controller = dto.controller;
                    objMenu.data_hora_alteracao = DateTime.Now;
                    objMenu.icone = dto.icone;
                    objMenu.id = dto.id.Value;
                    objMenu.menu_superior_id = dto.menu_superior_id;
                    objMenu.nome = dto.nome;
                    objMenu.ordem = dto.ordem;
                    objMenu.cs_colaborador_alteracao = cs_colaborador_logado;

                    _repository.Update(objMenu, logOperacaoId);

                    return dto;
                }
                else
                    throw new InvalidOperationException("Menu não encontrado para efetuar a atualização.");
            }
            else
                return null;

        }

        /// <summary>
        /// Obtém os Submenus da opção do Menu
        /// </summary>
        /// <param name="subMenuId">Identificador do Menu Superior</param>
        /// <param name="listaMenus">Lista dos Menus para procurar os submenus do Menu Superior informado</param>
        /// <returns>Lista de SubMenus do Menu Superior Informado</returns>
        private ICollection<MenuDTO> ObterSubMenus(string subMenuId, ICollection<MenuDTO> listaMenus)
        {
            var subs = listaMenus.Where(m => m.menu_superior_id == subMenuId).ToList();

            foreach (var item in subs)
            {
                if (listaMenus.Where(m => m.menu_superior_id == item.id.ToString()).Count() > 0)
                {
                    item.SubMenus = ObterSubMenus(item.id.ToString(), listaMenus).ToList();
                }
                else
                {
                    return subs;
                }
            }

            if (subs.Count() == 0)
                return null;
            else
                return subs;
        }

        /// <summary>
        /// Obtém todos os Menus Gravados no banco de dados
        /// </summary>
        /// <returns>Listagem de Menus do Sistema</returns>
        public ICollection<MenuDTO> ListarTodos()
        {
            return _repository.ListarTodosOsMenus().ToList();
        }
    }
}
