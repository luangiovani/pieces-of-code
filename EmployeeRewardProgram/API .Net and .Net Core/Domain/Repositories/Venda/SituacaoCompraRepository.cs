using Dapper;
using Database.Models.Venda;
using Database.Queries.Venda;
using Microsoft.Extensions.Configuration;
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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela SituacaoCompra
    /// </summary>
    public class SituacaoCompraRepository : BaseRepository<SituacaoCompra>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public SituacaoCompraRepository(IConfiguration configuration, SituacaoCompraQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Verifica se a Situacao da Compra que está sendo cadastrada já existe
        /// </summary>
        /// <param name="descricao">Nome da Situacao da Compra</param>
        /// <returns>Objeto com a Situacao da Compra de acordo com o nome informado.</returns>
        public SituacaoCompra VerificarSeExiste(string descricao)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new SituacaoCompraQueries().SELECT + @" WHERE descricao = @descricao";

                return dbConnection.Query<SituacaoCompra>(sQuery, new { descricao }).FirstOrDefault();
            }
        }
    }
}