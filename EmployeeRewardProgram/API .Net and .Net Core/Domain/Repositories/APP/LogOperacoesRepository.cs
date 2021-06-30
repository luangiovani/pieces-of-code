using Microsoft.Extensions.Configuration;
using Dapper;
using Database.Models.APP;
using Database.Queries.APP;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using Domain.DTO.APP;

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
    /// Implementação do Repositório para operações de banco de dados da tabela LogOperacoes
    /// </summary>
    public class LogOperacoesRepository : IDisposable
    {
        /// <summary>
        /// Variável que irá armazenar o caminho com o banco de dados
        /// </summary>
        protected string ConnectionString { get; }

        /// <summary>
        /// Objeto das queries de banco de dados
        /// </summary>
        private readonly LogOperacoesQueries queries = new LogOperacoesQueries();

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        public LogOperacoesRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("RMPConn");
        }

        /// <summary>
        /// Adicionar um novo Log de Operação no banco de dados
        /// </summary>
        /// <param name="item">Objeto LogOperacoes com os dados para inserção</param>
        /// <returns>Id do registro gravado.</returns>
        public LogOperacoes Add(LogOperacoes item)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = queries.INSERT;
                dbConnection.Open();
                return dbConnection.Query<LogOperacoes>(sQuery, item).FirstOrDefault();
            }
        }

        /// <summary>
        /// Obter todos os Logs do banco de dados
        /// </summary>
        /// <returns>Lista de Objetos do tipo LogOperacoes</returns>
        public IEnumerable<LogOperacoes> FindAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<LogOperacoes>(queries.SELECT);
            }
        }

        /// <summary>
        /// Obter LogOperacoes pelo ID informado
        /// </summary>
        /// <param name="id">Id do Log que se deseja obter o detalhamento</param>
        /// <returns>Objeto LogOperacoes localizado pelo Id Informado</returns>
        public LogOperacoes FindByID(string id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = queries.SELECT_WHERE_ID;
                dbConnection.Open();
                return dbConnection.Query<LogOperacoes>(sQuery, new { id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Remover ou Inativar um LogOperacao
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
        /// Atualizar um LogOperacao no banco de dados
        /// </summary>
        /// <param name="item">Objeto de parâmetro com as informações para atualizar o registro no banco de dados</param>
        public void Update(LogOperacoes item)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = queries.UPDATE;
                dbConnection.Open();
                dbConnection.Query(sQuery, item);
            }
        }

        /// <summary>
        /// Obtém os Logs de acordo com os filtros informados
        /// </summary>
        /// <param name="dto">DTO com os filtros para consulta de Logs</param>
        /// <returns>Lista de Logs de acordo com os filtros informados</returns>
        public IEnumerable<LogsDTO> ListarLogs(LogsBuscaDTO dto)
        {
            if (dto == null)
            {
                dto = new LogsBuscaDTO();
            }

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = queries.SELECT + @" WHERE 1=1";

                if (!string.IsNullOrEmpty(dto.dataInicial.ToString()))
                {
                    sQuery += @" AND CONVERT(DATE, ISNULL(LO.data_hora_inicio, GETDATE()), 103) >= @dtIni";
                }

                if (!string.IsNullOrEmpty(dto.dataFinal.ToString()))
                {
                    sQuery += @" AND CONVERT(DATE, ISNULL(LO.data_hora_fim, GETDATE()), 103) <= @dtFim";
                }

                if (!string.IsNullOrEmpty(dto.operacao))
                {
                    dto.operacao = "%" + dto.operacao + "%";
                    sQuery += @" AND LO.operacao LIKE @operacao";
                }

                if (!string.IsNullOrEmpty(dto.observacoes))
                {
                    dto.observacoes = "%" + dto.observacoes + "%";
                    sQuery += @" AND LO.observacao LIKE @observacoes";
                }

                dbConnection.Open();
                return dbConnection.Query<LogsDTO>(sQuery, dto);
            }
        }

        public IEnumerable<string> ListarOperacoes()
        {
            string sQuery = @"
                SELECT DISTINCT operacao 
                  FROM APP.LogOperacoes(NOLOCK)";

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<string>(sQuery);
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
