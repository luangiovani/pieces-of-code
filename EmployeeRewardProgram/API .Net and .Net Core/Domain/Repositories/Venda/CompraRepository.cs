using Dapper;
using Database.Models.APP;
using Database.Models.Venda;
using Database.Queries.Venda;
using Domain.DTO.Loja;
using Domain.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories.Venda
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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Compra
    /// </summary>
    public class CompraRepository : BaseRepository<Compra>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public CompraRepository(IConfiguration configuration, CompraQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Recupera uma listagem com os produtos mais trocados
        /// </summary>
        /// <returns>Lista de Objetos com os produtos mais trocados</returns>
        public IEnumerable<ProdutosMaisTrocadosDTO> ProdutosMaisTrocados()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<ProdutosMaisTrocadosDTO>(new CompraQueries().SELECT_PRODUTOS_MAIS_COMPRADOS);
            }
        }

        public IEnumerable<CompraTrocaDTO> TrocasPendentes()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<CompraTrocaDTO>(new CompraQueries().SELECT_COMPRAS_PENDENTES);
            }
        }

        public IEnumerable<CompraTrocaDTO> TrocasPendentesEnvio()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<CompraTrocaDTO>(new CompraQueries().SELECT_COMPRAS_PENDENTES_ENVIO);
            }
        }

        internal CombosTrocaDTO ObterOpcoesCombos()
        {
            throw new NotImplementedException();
        }

        public DetalheTrocaDTO DetalheTroca(string id)
        {
            var objDetalhe = new DetalheTrocaDTO();
            var listaItems = new List<DetalheTrocaItemsDTO>();

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                objDetalhe = dbConnection.Query<DetalheTrocaDTO>(new CompraQueries().SELECT_DETALHE_COMPRA, new { compra_id = id }).FirstOrDefault();
                var itensCompra = dbConnection.Query<DetalheTrocaItemsDTO>(new CompraQueries().SELECT_DETALHE_ITENS_COMPRA, new { compra_id = id });
                int conta = 1;
                foreach (var item in itensCompra)
                {
                    listaItems.Add(new DetalheTrocaItemsDTO()
                    {
                        id = item.id,
                        nome = item.nome,
                        ordem = conta,
                        sequencial = item.sequencial,
                        observacao = item.observacao,
                        valor_monetario = item.valor_monetario,
                        valor_pontos = item.valor_pontos,
                        imagem = item.imagem
                    });
                    conta++;
                }

                objDetalhe.Items = listaItems;
            }

            return objDetalhe;
        }

        public IEnumerable<DetalheTrocaDTO> HistoricoTrocasRealizadas()
        {
            var listaRetorno = new List<DetalheTrocaDTO>();

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                var trocas = dbConnection.Query<DetalheTrocaDTO>(new CompraQueries().SELECT_HISTORICO_COMPRAS_REALIZADAS);
                foreach (var troca in trocas)
                {
                    troca.Items = dbConnection.Query<DetalheTrocaItemsDTO>(new CompraQueries().SELECT_DETALHE_ITENS_COMPRA, new { compra_id = troca.id });
                    listaRetorno.Add(troca);
                }
            }

            return listaRetorno;
        }

        public IEnumerable<DetalheTrocaDTO> HistoricoTrocasColaborador(string cs_colaborador)
        {
            var listaRetorno = new List<DetalheTrocaDTO>();

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                var trocas = dbConnection.Query<DetalheTrocaDTO>(new CompraQueries().SELECT_HISTORICO_COLABORADOR, new { cs_colaborador });
                foreach (var troca in trocas)
                {
                    troca.Items = dbConnection.Query<DetalheTrocaItemsDTO>(new CompraQueries().SELECT_DETALHE_ITENS_COMPRA, new { compra_id = troca.id });
                    listaRetorno.Add(troca);
                }
            }

            return listaRetorno;
        }

        public CombosTrocaDTO ObterOpcoesCombos(string produto_id)
        {
            var opcoes = new CombosTrocaDTO();

            //using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            //{
            //    dbConnection.Open();
            //    opcoes.Lojas = dbConnection.Query<LojasDTO>(new CompraQueries().SELECT_HISTORICO_COLABORADOR);
            //    opcoes.Opcoes = dbConnection.Query<OpcaoEntregaDTO>(new CompraQueries().SELECT_DETALHE_ITENS_COMPRA);

            //    var ListaValores = new List<VariacoesProdutoDTO>();

            //    var listValores = dbConnection.Query<VariacoesProdutoDTO>(new VariacaoProdutoQueries().SELECT_VARIACOES, new { produto_id });

            //    if (listValores.Count() > 0)
            //    {
            //        foreach (var item in listValores)
            //        {
            //            var variacao = item;
            //            variacao.Valores = dbConnection.Query<VariacoesProdutoValoresDTO>(new VariacaoProdutoQueries().SELECT_VARIACOES_VALORES, new { produto_id, item.id });

            //            ListaValores.Add(variacao);
            //        }   
            //    }

            //    opcoes.VariacoesProduto = ListaValores;
            //}

            return opcoes;
        }

        public string MudarSituacaoCompra(MudaSituacaoDTO dto, string cs_colaborador, Guid logOperacaoId)
        {
            var objCompra = FindByID(dto.compra_id);
            if (objCompra != null)
            {
                objCompra.situacao_compra_id = dto.situacao_compra_id;

                if (!string.IsNullOrEmpty(dto.justificativa))
                    objCompra.justificativa = dto.justificativa;

                if (!string.IsNullOrEmpty(dto.informacoes_complementares))
                    objCompra.informacoes_complementares = dto.informacoes_complementares;

                objCompra.cs_colaborador_alteracao = cs_colaborador;
                objCompra.data_hora_alteracao = DateTime.Now;
                Update(objCompra, logOperacaoId);
            }

            return SituacaoCompraEnum.GetName(dto.situacao_compra_id);
        }

        public List<RelatorioComprasDTO> ObterRelatorioTrocas(DateTime dataDe, DateTime dataAte, string situacao, int pago, string loja_id)
        {
            List<RelatorioComprasDTO> listaRetorno = new List<RelatorioComprasDTO>();
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string querie = new CompraQueries().SELECT_RELATORIO_TROCAS;

                if (!string.IsNullOrEmpty(situacao))
                {
                    querie += " AND C.situacao_compra_id = @situacao";
                }

                if (pago >= -1)
                {
                    querie += " AND ISNULL(BT.pago,-1) = @pago";
                }

                if (!string.IsNullOrEmpty(loja_id))
                {
                    querie += " AND C.loja_id = @loja_id";
                }

                querie += ";";

                listaRetorno = dbConnection.Query<RelatorioComprasDTO>(querie, new { dataDe, dataAte, situacao, pago, loja_id }).ToList();
            }

            return listaRetorno;
        }

        public void FaturarCompras(List<string> comprasId, string cs_colaborador_logado, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string querie = new CompraQueries().INSERT_FATURAR_COMPRA;

                foreach (var compraId in comprasId)
                {
                    var log = new LogTransacoes()
                    {
                        id = Guid.NewGuid(),
                        log_operacao_id = logOperacaoId.ToString(),
                        data_hora_inicio = DateTime.Now,
                        data_hora_fim = null,
                        observacao = "Compra Enviada para Faturamento - Id.: " + compraId,
                        transacao = querie
                    };

                    try
                    {
                        log.data_hora_fim = DateTime.Now;
                        querie = _logQueries.INSERT;
                        dbConnection.Execute(querie, log);

                        querie = new CompraQueries().INSERT_FATURAR_COMPRA;
                        dbConnection.Execute(querie, new { compraId, cs_colaborador_logado });
                    }
                    catch (Exception err)
                    {
                        log.observacao = "Ocorereu um erro ao tentar enviar Compra para Faturamento.: " + err.Message;
                        log.data_hora_fim = DateTime.Now;
                        querie = _logQueries.INSERT;
                        dbConnection.Execute(querie, log);
                    }
                }
            }
        }

        public void PagarCompras(List<string> faturamentosId, string cs_colaborador_logado, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string querie = string.Empty;

                foreach (var id in faturamentosId)
                {
                    var log = new LogTransacoes()
                    {
                        id = Guid.NewGuid(),
                        log_operacao_id = logOperacaoId.ToString(),
                        data_hora_inicio = DateTime.Now,
                        data_hora_fim = null,
                        observacao = "Pagar a Compra Faturada - Faturamento.: " + id,
                        transacao = querie
                    };

                    try
                    {
                        log.data_hora_fim = DateTime.Now;
                        querie = _logQueries.INSERT;
                        dbConnection.Execute(querie, log);

                        querie = new CompraQueries().UPDATE_PAGAR_COMPRA;
                        dbConnection.Execute(querie, new { id, cs_colaborador_logado });
                    }
                    catch (Exception err)
                    {
                        log.observacao = "Ocorereu um erro ao tentar para a Compra do Faturamento.: " + err.Message;
                        log.data_hora_fim = DateTime.Now;
                        querie = _logQueries.INSERT;
                        dbConnection.Execute(querie, log);
                    }
                }
            }
        }
    }
}
