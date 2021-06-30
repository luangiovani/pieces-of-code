using Dapper;
using Database.Models.Loja;
using Database.Queries.Loja;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories.Loja
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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela OpcaoEntrega
    /// </summary>
    public class OpcaoEntregaRepository : BaseRepository<OpcaoEntrega>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public OpcaoEntregaRepository(IConfiguration configuration, OpcaoEntregaQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Verifica se a Opção de Entrega que está sendo cadastrada já existe
        /// </summary>
        /// <param name="label">Label da Opção de Entrega</param>
        /// <returns>Objeto com a Opção de Entrega de acordo com o nome informado.</returns>
        public OpcaoEntrega VerificarSeExiste(string label)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new OpcaoEntregaQueries().SELECT + @" WHERE label = @label";

                return dbConnection.Query<OpcaoEntrega>(sQuery, new { label }).FirstOrDefault();
            }
        }
    }
}