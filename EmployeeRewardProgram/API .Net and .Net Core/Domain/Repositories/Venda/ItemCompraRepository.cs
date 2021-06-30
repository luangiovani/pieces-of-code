using Dapper;
using Database.Models.Venda;
using Database.Queries.Venda;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela ItemCompra
    /// </summary>
    public class ItemCompraRepository : BaseRepository<ItemCompra>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public ItemCompraRepository(IConfiguration configuration, ItemCompraQueries queries)
            : base(configuration, queries) { }

        public IEnumerable<ItemCompra> ListarItemsCompra(string compra_id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<ItemCompra>(new ItemCompraQueries().SELECT_WHERE_COMPRA_ID, new { compra_id });
            }
        }
    }
}