using Database.Models.APP;
using Domain.DTO.APP;
using Domain.Helpers;
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
    public class PerfilMenuDeNavegacaoService : BaseService<PerfilMenuDeNavegacao>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly PerfilMenuDeNavegacaoRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public PerfilMenuDeNavegacaoService(PerfilMenuDeNavegacaoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Cadastrar um novo Vínculo de Permissão de Acesso entre Perfil e Menu de Navegação
        /// </summary>
        /// <param name="dto">Objeto de Vínculo entre Perfil e Menu</param>
        /// <param name="cs_colaborador_logado">Id do usuário logado no sistema</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Objeto de Vínculo entre Perfil e Menu cadastrado</returns>
        public PerfilMenuNavegacaoDTO Gravar(PerfilMenuNavegacaoDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (dto != null &&
                (!string.IsNullOrEmpty(dto.menu_navegacao_id) &&
                 !string.IsNullOrEmpty(dto.perfil_id) &&
                 !string.IsNullOrEmpty(cs_colaborador_logado) &&
                 !string.IsNullOrEmpty(logOperacaoId.ToString())))
            {
                PerfilMenuDeNavegacao objPerfilMenuDeNavegacao;
                if (string.IsNullOrEmpty(dto.id.ToString()))
                {
                    var existente = _repository.VerificarSeExiste(dto.perfil_id, dto.menu_navegacao_id);
                    if (existente != null)
                    {
                        objPerfilMenuDeNavegacao = existente;
                        objPerfilMenuDeNavegacao.perfil_id = dto.perfil_id;
                        objPerfilMenuDeNavegacao.menu_navegacao_id = dto.menu_navegacao_id;
                        objPerfilMenuDeNavegacao.visualizar = dto.visualizar;
                        objPerfilMenuDeNavegacao.cadastrar = dto.cadastrar;
                        objPerfilMenuDeNavegacao.excluir = dto.excluir;
                        objPerfilMenuDeNavegacao.ativo = dto.ativo;
                        objPerfilMenuDeNavegacao.data_hora_alteracao = DateTime.Now;
                        objPerfilMenuDeNavegacao.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                    {
                        objPerfilMenuDeNavegacao = new PerfilMenuDeNavegacao()
                        {
                            perfil_id = dto.perfil_id,
                            menu_navegacao_id = dto.menu_navegacao_id,
                            visualizar = dto.visualizar,
                            cadastrar = dto.cadastrar,
                            excluir = dto.excluir,
                            ativo = dto.ativo,
                            data_hora_criacao = DateTime.Now,
                            cs_colaborador_criacao = cs_colaborador_logado,
                        };
                    }
                }
                else
                {
                    objPerfilMenuDeNavegacao = _repository.FindByID(dto.id.ToString());

                    if (objPerfilMenuDeNavegacao != null)
                    {
                        objPerfilMenuDeNavegacao.perfil_id = dto.perfil_id;
                        objPerfilMenuDeNavegacao.menu_navegacao_id = dto.menu_navegacao_id;
                        objPerfilMenuDeNavegacao.visualizar = dto.visualizar;
                        objPerfilMenuDeNavegacao.cadastrar = dto.cadastrar;
                        objPerfilMenuDeNavegacao.excluir = dto.excluir;
                        objPerfilMenuDeNavegacao.ativo = dto.ativo;
                        objPerfilMenuDeNavegacao.data_hora_alteracao = DateTime.Now;
                        objPerfilMenuDeNavegacao.cs_colaborador_alteracao = cs_colaborador_logado;
                    }
                    else
                        throw new InvalidOperationException("Vínculo entre Perfil e Menu não encontrado para efetuar a atualização.");
                }

                if (!dto.id.HasValue || string.IsNullOrEmpty(dto.id.ToString()))
                    dto.id = _repository.Add(objPerfilMenuDeNavegacao, logOperacaoId).id;
                else
                    _repository.Update(objPerfilMenuDeNavegacao, logOperacaoId);

                return dto;
            }
            else
            {
                throw new InvalidOperationException("Os campos necessários não foram informados.");
            }

        }

        /// <summary>
        /// Vincular / Desvincular (No cadastro ou atualização do Perfil) 
        /// </summary>
        /// <param name="menus_id">Lista de Menus que este perfil tem acesso</param>
        /// <param name="perfil_id">Identificador do Perfil</param>
        /// <param name="cs_colaborador_logado">CS do Colaborador Logado</param>
        /// <param name="logOperacaoId">Identificador do Log</param>
        public void VincularDesvincularMenuPerfil(List<string> menus_id, string perfil_id, string cs_colaborador_logado, Guid logOperacaoId)
        {
            if (menus_id.Count > 0 && !string.IsNullOrEmpty(perfil_id))
            {
                _repository.VincularDesvincular(menus_id, perfil_id, cs_colaborador_logado, logOperacaoId);
            }
            else
            {
                throw new InvalidOperationException("Os campos necessários não foram informados.");
            }
        }

        public IEnumerable<string> ListarMenusIdPerfil(string perfil_id)
        {
            return _repository.ListarMenusIdPerfil(perfil_id);
        }

        public bool ValidaAcesso(string perfil_id, string controller, string acao)
        {
            if (string.IsNullOrEmpty(perfil_id))
            {
                return false;
            }
            else
            {
                Dictionary<string, List<string>> controllersActions = new Dictionary<string, List<string>>();
                List<string> actionsList;

                if (perfil_id == PerfilAcessoEnum.AdminTecnico)
                {
                    return true;
                }
                else if (perfil_id == PerfilAcessoEnum.Admin)
                {
                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("ativar-inativar");
                    controllersActions.Add("cargo", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("atualizar-perfil");
                    actionsList.Add("gestores");
                    actionsList.Add("obter-saldo-pontos");
                    actionsList.Add("dashboard");
                    actionsList.Add("recomendacoes-recebidas");
                    actionsList.Add("minhas-trocas");
                    controllersActions.Add("colaborador", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    controllersActions.Add("perfil", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("obter-configuracao-expiracao");
                    actionsList.Add("cadastrar-atualizar-expiracao");
                    actionsList.Add("obter-configuracao-verba");
                    actionsList.Add("cadastrar-atualizar-verba");
                    controllersActions.Add("configurar", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    actionsList.Add("obter-colaboradores-loja");
                    actionsList.Add("gravar-vinculo-colaborador-loja");
                    actionsList.Add("desvincular-colaborador-loja");
                    controllersActions.Add("lojas", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    actionsList.Add("listar-por-loja");
                    controllersActions.Add("opcaoentrega", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    controllersActions.Add("produto", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("obter-atual");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    controllersActions.Add("taxaconversao", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    controllersActions.Add("verba", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("receber-produtos-troca");
                    actionsList.Add("cancelar-solicitacao-troca");
                    actionsList.Add("solicitar-troca-pontos");
                    actionsList.Add("pagar-faturamentos");
                    actionsList.Add("relatorio-trocas-efetuadas");
                    controllersActions.Add("compra", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("situacaocompra", actionsList);
                }
                else if (perfil_id == PerfilAcessoEnum.AdminGestor)
                {
                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("ativar-inativar");
                    controllersActions.Add("cargo", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("listar-colaboradores");
                    actionsList.Add("atualizar-perfil");
                    actionsList.Add("gestores");
                    actionsList.Add("obter-saldo-pontos");
                    actionsList.Add("dashboard");
                    actionsList.Add("recomendacoes-recebidas");
                    actionsList.Add("minhas-trocas");
                    actionsList.Add("consultar");
                    actionsList.Add("relatorio-pontuacao");
                    actionsList.Add("extrato-colaboradores-gestor");
                    actionsList.Add("relatorio-troca-pontos");
                    controllersActions.Add("colaborador", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    controllersActions.Add("perfil", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("obter-configuracao-expiracao");
                    actionsList.Add("cadastrar-atualizar-expiracao");
                    actionsList.Add("obter-configuracao-verba");
                    actionsList.Add("cadastrar-atualizar-verba");
                    controllersActions.Add("configurar", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    actionsList.Add("obter-colaboradores-loja");
                    actionsList.Add("gravar-vinculo-colaborador-loja");
                    actionsList.Add("desvincular-colaborador-loja");
                    controllersActions.Add("lojas", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    actionsList.Add("listar-por-loja");
                    controllersActions.Add("opcaoentrega", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    controllersActions.Add("produto", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("obter-atual");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    controllersActions.Add("taxaconversao", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    actionsList.Add("cadastrar");
                    actionsList.Add("ativar-inativar");
                    controllersActions.Add("verba", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("receber-produtos-troca");
                    actionsList.Add("cancelar-solicitacao-troca");
                    actionsList.Add("solicitar-troca-pontos");
                    actionsList.Add("relatorio-trocas-efetuadas");
                    actionsList.Add("pagar-faturamentos");
                    controllersActions.Add("compra", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("situacaocompra", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("efetivar");
                    actionsList.Add("listar-avaliacoes-realizadas");
                    controllersActions.Add("avaliar", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("cadastrar");
                    actionsList.Add("detalhe-recomendacao");
                    actionsList.Add("listar-status-recomendacoes");
                    controllersActions.Add("reconhecer", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    controllersActions.Add("tiporecomendacao", actionsList);

                }
                else if (perfil_id == PerfilAcessoEnum.Gestor)
                {
                    actionsList = new List<string>();
                    actionsList.Add("listar-colaboradores");
                    actionsList.Add("obter-saldo-pontos");
                    actionsList.Add("dashboard");
                    actionsList.Add("recomendacoes-recebidas");
                    actionsList.Add("minhas-trocas");
                    actionsList.Add("consultar");
                    actionsList.Add("relatorio-pontuacao");
                    actionsList.Add("extrato-colaboradores-gestor");
                    actionsList.Add("relatorio-troca-pontos");
                    controllersActions.Add("colaborador", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("receber-produtos-troca");
                    actionsList.Add("cancelar-solicitacao-troca");
                    actionsList.Add("solicitar-troca-pontos");
                    controllersActions.Add("compra", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("produto", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    controllersActions.Add("lojas", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("obter-atual");
                    actionsList.Add("listar");
                    controllersActions.Add("taxaconversao", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar-por-loja");
                    controllersActions.Add("opcaoentrega", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("efetivar");
                    actionsList.Add("listar-avaliacoes-realizadas");
                    controllersActions.Add("avaliar", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("cadastrar");
                    actionsList.Add("detalhe-recomendacao");
                    actionsList.Add("listar-status-recomendacoes");
                    controllersActions.Add("reconhecer", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    controllersActions.Add("tiporecomendacao", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("situacaoavaliacao", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("situacaorecomendacao", actionsList);
                }
                else if (perfil_id == PerfilAcessoEnum.Colaborador)
                {
                    actionsList = new List<string>();
                    actionsList.Add("obter-saldo-pontos");
                    actionsList.Add("dashboard");
                    actionsList.Add("recomendacoes-recebidas");
                    actionsList.Add("minhas-trocas");
                    controllersActions.Add("colaborador", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("receber-produtos-troca");
                    actionsList.Add("cancelar-solicitacao-troca");
                    actionsList.Add("solicitar-troca-pontos");
                    controllersActions.Add("compra", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("produto", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    controllersActions.Add("lojas", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("obter-atual");
                    controllersActions.Add("taxaconversao", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar-por-loja");
                    controllersActions.Add("opcaoentrega", actionsList);

                }
                else if (perfil_id == PerfilAcessoEnum.Loja)
                {
                    actionsList = new List<string>();
                    actionsList.Add("historico-trocas");
                    actionsList.Add("trocas-pendentes");
                    actionsList.Add("obter-solicitacao-pendente");
                    actionsList.Add("mudar-solicitacao-pendente");
                    controllersActions.Add("trocas", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("consultar");
                    actionsList.Add("consultar-colaborador-loja");
                    controllersActions.Add("colaborador", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("solicitar-troca-pontos");
                    actionsList.Add("relatorio-trocas-efetuadas");
                    actionsList.Add("faturar-compras");
                    controllersActions.Add("compra", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("situacaocompra", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("situacaotroca", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("produto", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("listar");
                    actionsList.Add("consultar");
                    controllersActions.Add("lojas", actionsList);

                    actionsList = new List<string>();
                    actionsList.Add("obter-atual");
                    controllersActions.Add("taxaconversao", actionsList);
                }
                else
                {
                    return false;
                }

                return controllersActions.Count(kv => kv.Key == controller && kv.Value.Contains(acao)) >= 1;
            }
        }
    }
}