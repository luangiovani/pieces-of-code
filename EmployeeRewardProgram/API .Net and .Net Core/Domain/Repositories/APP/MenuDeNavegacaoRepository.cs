using Microsoft.Extensions.Configuration;
using Database.Queries.APP;
using Database.Models.APP;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela MenuDeNavegacao
    /// </summary>
    public class MenuDeNavegacaoRepository : BaseRepository<MenuDeNavegacao>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public MenuDeNavegacaoRepository(IConfiguration configuration, MenuDeNavegacaoQueries queries) 
            : base(configuration, queries) { }

        public IEnumerable<MenuDeNavegacao> ListarMenusAtivosAplicacao(string aplicacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sSqlQuery = new MenuDeNavegacaoQueries().SELECT_WHERE_ATIVO + @"
                 AND aplicacao_id = @aplicacaoId";

                dbConnection.Open();
                return dbConnection.Query<MenuDeNavegacao>(sSqlQuery, aplicacaoId).ToList();
            }
        }

        public IEnumerable<MenuDTO> ListarTodosOsMenus()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<MenuDTO>(new MenuDeNavegacaoQueries().SELECT_LISTAR_MENUS).ToList();
            }
        }
    }
}
