using Microsoft.Extensions.Configuration;
using Database.Queries.APP;
using Database.Models.APP;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Aplicacao
    /// </summary>
    public class AplicacaoRepository : BaseRepository<Aplicacao>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public AplicacaoRepository(IConfiguration configuration, AplicacaoQueries queries) 
            : base(configuration, queries) { }

        /// <summary>
        /// Verifica se a Aplicação que está sendo cadastrada já existe
        /// </summary>
        /// <param name="descricao">Nome da Aplicação</param>
        /// <returns>Objeto com a Aplicação de acordo com o nome informado.</returns>
        public Aplicacao VerificarSeExiste(string descricao)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new AplicacaoQueries().SELECT + @" WHERE descricao = @descricao";

                return dbConnection.Query<Aplicacao>(sQuery, new { descricao }).FirstOrDefault();
            }
        }
    }
}
