using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using Database.Queries;
using System;
using System.Data.SqlClient;
using Database.Queries.APP;
using Database.Models.APP;

namespace Domain.Repositories
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Implementação da Interface de Repositórios para operações de banco de dados
    /// </atividades>
    /// <summary>
    /// Implementação da Interface de Repositório para operações de banco de dados
    /// Classe base que será herdada pelas outras implementações
    /// </summary>
    public class BaseRepository<Model> : IBaseRepository<Model>
        where Model : class
    {
        /// <summary>
        /// Variável que irá armazenar o caminho com o banco de dados
        /// </summary>
        protected string ConnectionString { get; }

        /// <summary>
        /// Interface das queries de banco de dados
        /// </summary>
        public IQueries queries;

        /// <summary>
        /// Objeto de queries dos Logs de Transações
        /// </summary>
        protected LogTransacoesQueries _logQueries = new LogTransacoesQueries();

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connection string do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public BaseRepository(IConfiguration configuration, IQueries pqueries)
        {
            queries = pqueries;
            ConnectionString = configuration.GetConnectionString("RMPConn");
        }

        /// <summary>
        /// Adicionar um novo registro no banco de dados
        /// </summary>
        /// <param name="item">Objeto com os dados para inserção</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        /// <returns>Id do registro gravado.</returns>
        public Model Add(Model item, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = queries.INSERT;
                var log = new LogTransacoes()
                {
                    id = Guid.NewGuid(),
                    log_operacao_id = logOperacaoId.ToString(),
                    data_hora_inicio = DateTime.Now,
                    data_hora_fim = null,
                    observacao = "INSERIR",
                    transacao = sQuery
                };

                dbConnection.Open();
                var obj = dbConnection.Query<Model>(sQuery, item).FirstOrDefault();

                log.data_hora_fim = DateTime.Now;
                sQuery = _logQueries.INSERT;
                dbConnection.Execute(sQuery, log);

                return obj;
            }
        }

        /// <summary>
        /// Remover ou Inativar um registro
        /// </summary>
        /// <param name="id">Id do registro que se deseja realizar a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        public void Remove(string id, string cs_colaborador_logado, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = queries.DELETE;
                var log = new LogTransacoes()
                {
                    id = Guid.NewGuid(),
                    log_operacao_id = logOperacaoId.ToString(),
                    data_hora_inicio = DateTime.Now,
                    data_hora_fim = null,
                    observacao = "INATIVAR",
                    transacao = sQuery
                };

                dbConnection.Open();
                dbConnection.Execute(sQuery, new { id, cs_colaborador_alteracao = cs_colaborador_logado });

                log.data_hora_fim = DateTime.Now;
                sQuery = _logQueries.INSERT;
                dbConnection.Execute(sQuery, log);

            }
        }

        /// <summary>
        /// Ativar um registro
        /// </summary>
        /// <param name="id">Id do registro que se deseja realizar a operação</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        public void Activate(string id, string cs_colaborador_logado, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = queries.ACTIVATE;
                var log = new LogTransacoes()
                {
                    id = Guid.NewGuid(),
                    log_operacao_id = logOperacaoId.ToString(),
                    data_hora_inicio = DateTime.Now,
                    data_hora_fim = null,
                    observacao = "ATIVAR",
                    transacao = sQuery
                };

                dbConnection.Open();
                dbConnection.Execute(sQuery, new { id, cs_colaborador_alteracao = cs_colaborador_logado });

                log.data_hora_fim = DateTime.Now;
                sQuery = _logQueries.INSERT;
                dbConnection.Execute(sQuery, log);

            }
        }

        /// <summary>
        /// Atualizar um registro no banco de dados
        /// </summary>
        /// <param name="item">Objeto de parâmetro com as informações para atualizar o registro no banco de dados</param>
        /// <param name="logOperacaoId">Id do Log de Operação que a transação pertence</param>
        public void Update(Model item, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = queries.UPDATE;

                var log = new LogTransacoes()
                {
                    id = Guid.NewGuid(),
                    log_operacao_id = logOperacaoId.ToString(),
                    data_hora_inicio = DateTime.Now,
                    data_hora_fim = null,
                    observacao = "ATUALIZAR",
                    transacao = sQuery
                };

                dbConnection.Open();
                dbConnection.Query(sQuery, item);

                log.data_hora_fim = DateTime.Now;
                sQuery = _logQueries.INSERT;
                dbConnection.Execute(sQuery, log);
            }
        }

        /// <summary>
        /// Obter registro pelo ID informado
        /// </summary>
        /// <param name="id">Id do registro que se deseja obter o detalhamento</param>
        /// <returns>Objeto Model localizado pelo Id Informado</returns>
        public Model FindByID(string id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = queries.SELECT_WHERE_ID;
                dbConnection.Open();
                return dbConnection.Query<Model>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Obter todos os registros do banco de dados
        /// </summary>
        /// <returns>Lista de Objetos do tipo Model</returns>
        public IEnumerable<Model> FindAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Model>(queries.SELECT);
            }
        }

        /// <summary>
        /// Método para encerrar e liberar a memória deste objeto injetado
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
