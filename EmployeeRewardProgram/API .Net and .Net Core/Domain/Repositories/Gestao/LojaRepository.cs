using Database.Queries.Gestao;
using Database.Models.Gestao;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Domain.DTO.Gestao;
using System.Collections.Generic;
using Domain.Helpers;
using System;
using Database.Models.APP;

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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Loja
    /// </summary>
    public class LojaRepository : BaseRepository<Lojas>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public LojaRepository(IConfiguration configuration, LojaQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Verifica se a Loja que está sendo cadastrada já existe
        /// </summary>
        /// <param name="nome">Nome da Loja</param>
        /// <returns>Objeto com a Loja de acordo com o nome informado.</returns>
        public Lojas VerificarSeExiste(string nome)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new LojaQueries().SELECT + @" WHERE nome = @nome";

                return dbConnection.Query<Lojas>(sQuery, new { nome }).FirstOrDefault();
            }
        }

        public ColaboradoresLojasDTO VerificarSeExisteVinculo(string cs, string lojaId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new LojaQueries().SELECT_COLABORADORES_LOJA;
                return dbConnection.Query<ColaboradoresLojasDTO>(sQuery, new { perfilId = PerfilAcessoEnum.Loja, lojaId, cs }).FirstOrDefault();
            }
        }

        public IEnumerable<ColaboradoresLojasDTO> ObterColaboradoresLoja(string lojaId, string cs)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new LojaQueries().SELECT_COLABORADORES_LOJA;
                return dbConnection.Query<ColaboradoresLojasDTO>(sQuery, new { perfilId = PerfilAcessoEnum.Loja, lojaId, cs });
            }
        }

        public ColaboradoresLojasDTO GravarAtualizarVinculo(ColaboradoresLojasDTO dto, string cs_colaborador_logado, Guid logOperacaoId)
        {
            dto.cs_colaborador_logado = cs_colaborador_logado;
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = string.Empty;

                if (string.IsNullOrEmpty(dto.id))
                {
                    sQuery = new LojaQueries().INSERT_COLABORADOR_LOJA;
                    var log = new LogTransacoes()
                    {
                        id = Guid.NewGuid(),
                        log_operacao_id = logOperacaoId.ToString(),
                        data_hora_inicio = DateTime.Now,
                        data_hora_fim = null,
                        observacao = "INSERIR Vinculo Colaborador Loja",
                        transacao = sQuery
                    };

                    dbConnection.Open();
                    dto = dbConnection.Query<ColaboradoresLojasDTO>(sQuery, dto).FirstOrDefault();

                    log.data_hora_fim = DateTime.Now;
                    sQuery = _logQueries.INSERT;
                    dbConnection.Execute(sQuery, log);

                }
                else
                {
                    sQuery = new LojaQueries().UPDATE_COLABORADOR_LOJA;
                    var log = new LogTransacoes()
                    {
                        id = Guid.NewGuid(),
                        log_operacao_id = logOperacaoId.ToString(),
                        data_hora_inicio = DateTime.Now,
                        data_hora_fim = null,
                        observacao = "ATUALIZAR Vinculo Colaborador Loja",
                        transacao = sQuery
                    };

                    dbConnection.Open();
                    dto = dbConnection.Query<ColaboradoresLojasDTO>(sQuery, dto).FirstOrDefault();

                    log.data_hora_fim = DateTime.Now;
                    sQuery = _logQueries.INSERT;
                    dbConnection.Execute(sQuery, log);
                }

                return dto;
            }
        }

        public void DesvincularColaboradorLoja(string id, string cs_colaborador_logado, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = string.Empty;

                if (!string.IsNullOrEmpty(id))
                {
                    sQuery = new LojaQueries().DESVINCULAR_COLABORADOR_LOJA;
                    var log = new LogTransacoes()
                    {
                        id = Guid.NewGuid(),
                        log_operacao_id = logOperacaoId.ToString(),
                        data_hora_inicio = DateTime.Now,
                        data_hora_fim = null,
                        observacao = "Desvincular Colaborador Loja " + cs_colaborador_logado,
                        transacao = sQuery
                    };

                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { id });

                    log.data_hora_fim = DateTime.Now;
                    sQuery = _logQueries.INSERT;
                    dbConnection.Execute(sQuery, log);

                }
            }
        }
    }
}