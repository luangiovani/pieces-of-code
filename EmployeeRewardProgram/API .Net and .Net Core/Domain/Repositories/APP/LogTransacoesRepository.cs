using Microsoft.Extensions.Configuration;
using Database.Queries.APP;
using Database.Models.APP;
using System;
using System.Data;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain.Repositories.APP
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
    /// Implementação do Repositório para operações de banco de dados da tabela LogTransacoes
    /// </summary>
    public class LogTransacoesRepository : IDisposable
    {
        /// <summary>
        /// Variável que irá armazenar o caminho com o banco de dados
        /// </summary>
        protected string ConnectionString { get; }

        /// <summary>
        /// Objeto das queries de banco de dados
        /// </summary>
        private readonly LogTransacoesQueries queries = new LogTransacoesQueries();

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        public LogTransacoesRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("RMPConn");
        }

        /// <summary>
        /// Adicionar um novo Log de Transação no banco de dados
        /// </summary>
        /// <param name="item">Objeto LogTransacoes com os dados para inserção</param>
        /// <returns>Id do registro gravado.</returns>
        public LogTransacoes Add(LogTransacoes item)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = queries.INSERT;
                dbConnection.Open();
                return dbConnection.Query<LogTransacoes>(sQuery, item).FirstOrDefault();
            }
        }

        /// <summary>
        /// Obter todos os Logs do banco de dados
        /// </summary>
        /// <returns>Lista de Objetos do tipo LogTransacoes</returns>
        public IEnumerable<LogTransacoes> FindAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<LogTransacoes>(queries.SELECT);
            }
        }

        /// <summary>
        /// Obter LogOperacoes pelo ID informado
        /// </summary>
        /// <param name="id">Id do Log que se deseja obter o detalhamento</param>
        /// <returns>Objeto LogTransacoes localizado pelo Id Informado</returns>
        public LogTransacoes FindByID(string id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = queries.SELECT_WHERE_ID;
                dbConnection.Open();
                return dbConnection.Query<LogTransacoes>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Remover ou Inativar um LogTransacoes
        /// </summary>
        /// <param name="id">Id do Log que se deseja realizar a operação</param>
        public void Remove(string id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = queries.DELETE;
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        /// <summary>
        /// Atualizar um LogTransacoes no banco de dados
        /// </summary>
        /// <param name="item">Objeto de parâmetro com as informações para atualizar o registro no banco de dados</param>
        public void Update(LogTransacoes item)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = queries.UPDATE;
                dbConnection.Open();
                dbConnection.Query(sQuery, item);
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