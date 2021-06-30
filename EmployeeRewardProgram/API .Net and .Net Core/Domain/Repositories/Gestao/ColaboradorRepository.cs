using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Database.Models.Gestao;
using Database.Queries.Gestao;
using Domain.DTO.Gestao;
using Domain.DTO.Loja;
using Database.Queries.Loja;
using Domain.DTO.APP;
using System;
using Database.Models.APP;
using Domain.Helpers;

namespace Domain.Repositories.Gestao
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Repositórios para operações de banco de dados
    /// </atividades>
    /// <summary>
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Colaborador
    /// </summary>
    public class ColaboradorRepository : BaseRepository<Colaborador>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public ColaboradorRepository(IConfiguration configuration, ColaboradorQueries queries)
            : base(configuration, queries) { }

        public IEnumerable<Colaborador> ListarColaboradores(string gestorId, bool meuTime)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Colaborador>(new ColaboradorQueries().LISTAR_POR_GESTOR, new
                {
                    cs_superior_imediato = gestorId,
                    meuTime
                }).ToList();
            }
        }

        public IEnumerable<Colaborador> ListarColaboradoresComTrocas(string gestorId, bool meuTime)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Colaborador>(new ColaboradorQueries().LISTAR_COM_TROCAS_POR_GESTOR, new
                {
                    cs_superior_imediato = gestorId,
                    meuTime
                }).ToList();
            }
        }

        public IEnumerable<Colaborador> ListarColaboradores()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Colaborador>(new ColaboradorQueries().LISTAR).ToList();
            }
        }
        
        public IEnumerable<GestoresDTO> ListarGestores()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<GestoresDTO>(new ColaboradorQueries().SELECT_GESTORES).ToList();
            }
        }

        /// <summary>
        /// Obter registro pelo ID informado
        /// </summary>
        /// <param name="id">Id do registro que se deseja obter o detalhamento</param>
        /// <returns>Objeto Model localizado pelo Id Informado</returns>
        public Colaborador FindByCS(string cs)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = new ColaboradorQueries().SELECT_WHERE_CS;
                dbConnection.Open();
                return dbConnection.Query<Colaborador>(sQuery, new { cs }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Obtém o relatório de pontuação de colaboradores, somente dos subordinados do Gestor ou aqueles que o gestor inseriu a recomendação
        /// </summary>
        /// <param name="cs_gestor">Código CS do Gestor</param>
        /// <returns>Lista de relatório de pontuação por co0laborador</returns>
        public IEnumerable<RelatorioPontuacaoDTO> RelatorioPontuacao(string cs_gestor)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = new ColaboradorQueries().RELATORIO_PONTUACAO_COLABORADOR;
                dbConnection.Open();
                return dbConnection.Query<RelatorioPontuacaoDTO>(sQuery, new { cs_gestor });
            }
        }

        public RelatorioPontuacaoDTO DetalheRelatorioPontuacao(string cs_colaborador, string cs_gestor)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = new ColaboradorQueries().RELATORIO_PONTUACAO_COLABORADOR;
                dbConnection.Open();
                return dbConnection.Query<RelatorioPontuacaoDTO>(sQuery, new { cs_gestor }).Where(r => r.cs_colaborador == cs_colaborador).FirstOrDefault();
            }
        }

        public IEnumerable<ExtratoColaboradorDTO> ExtratoColaboradoresGestor(string cs_gestor)
        {
            var listaPontosColaboradores = new List<ExtratoColaboradorDTO>();

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().SELECT_EXTRATO_COLABORADORES_GESTOR;
                dbConnection.Open();
                listaPontosColaboradores = dbConnection.Query<ExtratoColaboradorDTO>(sQuery, new { cs_gestor }).ToList();
            }

            return listaPontosColaboradores;
        }

        public ExtratoColaboradorDTO ExtratoColaborador(string cs_colaborador)
        {
            var extrato = new ExtratoColaboradorDTO();

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().SELECT_EXTRATO_COLABORADOR;
                dbConnection.Open();
                extrato = dbConnection.Query<ExtratoColaboradorDTO>(sQuery, new { cs_colaborador }).FirstOrDefault();

                if (extrato != null && !string.IsNullOrEmpty(extrato.id.ToString()))
                {
                    sQuery = new ColaboradorQueries().SELECT_TROCAS_COLABORADOR;
                    extrato.Trocas = dbConnection.Query<TrocasExtratoColaboradorDTO>(sQuery, new { cs_colaborador });
                }
            }

            return extrato;
        }

        public IEnumerable<RecomendacoesColaboradorDTO> ObterRecomendacoes(string cs_colaborador)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().SELECT_RECOMENDACOES_COLABORADOR;
                dbConnection.Open();
                return dbConnection.Query<RecomendacoesColaboradorDTO>(sQuery, new { cs_colaborador });
            }
        }

        public ColaboradorTrocaDTO ObterExtratoParaTroca(string cs_colaborador)
        {
            var objColaborador = new ColaboradorTrocaDTO();

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().SELECT_EXTRATO_COLABORADOR;
                dbConnection.Open();
                objColaborador = dbConnection.Query<ColaboradorTrocaDTO>(sQuery, new { cs_colaborador }).FirstOrDefault();

                if (objColaborador != null && !string.IsNullOrEmpty(objColaborador.id.ToString()))
                {
                    sQuery = new ProdutoQueries().SELECT_LISTAR;
                    objColaborador.Produtos = dbConnection.Query<ProdutosColaboradorTrocaDTO>(sQuery, new { cs_colaborador }).OrderBy(p => p.pontos);
                }
            }

            return objColaborador;
        }

        public UsuarioDTO ObterUsuarioOpcoesMenu(string cs, string aplicacao_id)
        {
            UsuarioDTO objUsuario;

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().SELECT_LOGIN;
                dbConnection.Open();
                objUsuario = dbConnection.Query<UsuarioDTO>(sQuery, new { cs }).FirstOrDefault();

                if (objUsuario != null)
                {
                    sQuery = new ColaboradorQueries().SELECT_MENUS_USUARIOS;

                    var listaMenus = dbConnection.Query<MenuUsuarioDTO>(sQuery, new { cs, aplicacao_id });
                    if (listaMenus.Count() > 0)
                    {
                        objUsuario.Menus = ListaMenus(listaMenus);
                        if (aplicacao_id.ToUpper() == AplicacaoEnum.Mobile)
                        {
                            var lstMenus = objUsuario.Menus.ToList();
                            lstMenus.Add(new MenuUsuarioDTO()
                            {
                                menu_opcao = "Sair",
                                acao = "",
                                cadastrar = true,
                                controller = "",
                                excluir = true,
                                icone = "exit_to_app",
                                menu_superior_id = null,
                                ordem = 9999,
                                visualizar = true
                            });

                            objUsuario.Menus = lstMenus.OrderBy(l => l.ordem);
                        }
                    }
                }
                else
                {
                    throw new Exception("Colaborador não encontrado.");
                }
            }

            return objUsuario;
        }

        public IEnumerable<MenuUsuarioDTO> ListaMenus(IEnumerable<MenuUsuarioDTO> listaMenus)
        {
            List<MenuUsuarioDTO> listaRetorno = new List<MenuUsuarioDTO>();

            foreach (var menuSuperior in listaMenus.Where(m => m.menu_superior_id.ToString() == ""))
            {
                if (listaMenus.Where(m => m.menu_superior_id == menuSuperior.menu_id).Count() > 0)
                {
                    menuSuperior.SubMenus = ListaSubMenus(menuSuperior.menu_id.ToString(), listaMenus);
                    listaRetorno.Add(menuSuperior);
                }
                else
                {
                    listaRetorno.Add(menuSuperior);
                }
            }

            return listaRetorno;
        }

        public IEnumerable<MenuUsuarioDTO> ListaSubMenus(string menu_superior_id, IEnumerable<MenuUsuarioDTO> listaMenus)
        {
            List<MenuUsuarioDTO> listaRetorno = new List<MenuUsuarioDTO>();

            foreach (var menu in listaMenus.Where(m => m.menu_superior_id.ToString() == menu_superior_id))
            {
                if (listaMenus.Where(m => m.menu_superior_id == menu.menu_id).Count() > 0)
                {
                    menu.SubMenus = ListaSubMenus(menu.menu_id.ToString(), listaMenus);
                    listaRetorno.Add(menu);
                }
                else
                {
                    listaRetorno.Add(menu);
                }
            }

            return listaRetorno;
        }

        public void AtualizarPerfil(PerfilColaboradorDTO dto, string cs_colaborador, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().UPDATE_PERFIL_COLABORADOR;
                dbConnection.Open();

                var log = new LogTransacoes()
                {
                    id = Guid.NewGuid(),
                    log_operacao_id = logOperacaoId.ToString(),
                    data_hora_inicio = DateTime.Now,
                    data_hora_fim = null,
                    observacao = "ATUALIZAR PERFIL DE COLABORADOR",
                    transacao = sQuery
                };

                dbConnection.Execute(sQuery, new { dto.perfil_id, cs_colaborador, id = dto.colaborador_id });

                log.data_hora_fim = DateTime.Now;
                sQuery = _logQueries.INSERT;
                dbConnection.Execute(sQuery, log);
            }
        }

        public void AtualizarPontosColaborador(Colaborador colab, string cs_colaborador, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().UPDATE_PONTOS_COLABORADOR;
                dbConnection.Open();

                var log = new LogTransacoes()
                {
                    id = Guid.NewGuid(),
                    log_operacao_id = logOperacaoId.ToString(),
                    data_hora_inicio = DateTime.Now,
                    data_hora_fim = null,
                    observacao = "ATUALIZAR PONTOS DE COLABORADOR",
                    transacao = sQuery
                };

                dbConnection.Execute(sQuery, new { colab.quantidade_pontos, cs_colaborador, colab.id });

                log.data_hora_fim = DateTime.Now;
                sQuery = _logQueries.INSERT;
                dbConnection.Execute(sQuery, log);
            }
        }

        public IEnumerable<GestorAvaliadorDTO> ListarGestoresAvaliadores(string cs_colaborador)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().SELECT_GESTORES_AVALIADORES;
                dbConnection.Open();
                return dbConnection.Query<GestorAvaliadorDTO>(sQuery, new { cs_colaborador });
            }
        }

        public IEnumerable<ComprasRealizadasCompradorDTO> ListarComprasColaboradoresGestor(string cs_gestor)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().SELECT_COMPRAS_COLABORADOR_GESTOR;
                dbConnection.Open();
                return dbConnection.Query<ComprasRealizadasCompradorDTO>(sQuery, new { cs_gestor });
            }
        }

        public IEnumerable<ComprasRealizadasCompradorDTO> ListarComprasColaboradores(string cs)
        {
            var listaRetorno = new List<ComprasRealizadasCompradorDTO>();
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().SELECT_COMPRAS_COLABORADOR;
                dbConnection.Open();
                var listObjCompras = dbConnection.Query<ComprasRealizadasCompradorDTO>(sQuery, new { cs });
                sQuery = new ColaboradorQueries().SELECT_ITEMS_COMPRAS_COLABORADOR;
                foreach (var objCompra in listObjCompras)
                {
                    objCompra.Items = dbConnection.Query<ItemCompraDTO>(sQuery, new { compra_id = objCompra.id.ToString() }).ToList();
                    listaRetorno.Add(objCompra);
                }
            }
            return listaRetorno;
        }

        public Colaborador ObterColaboradorLoja(string cs, string loja_id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new ColaboradorQueries().SELECT_COLABORADOR_LOJA;
                dbConnection.Open();
                return dbConnection.Query<Colaborador>(sQuery, new { cs, loja_id }).FirstOrDefault();
            }
        }
    }
}