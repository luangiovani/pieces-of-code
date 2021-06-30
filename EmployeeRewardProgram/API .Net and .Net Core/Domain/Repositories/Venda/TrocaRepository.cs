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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Troca
    /// </summary>
    public class TrocaRepository : BaseRepository<Troca>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public TrocaRepository(IConfiguration configuration, TrocaQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Verifica se a entidade que está sendo cadastrada já existe
        /// </summary>
        /// <param name="compra_id"></param>
        /// <param name="item_compra_troca_id"></param>
        /// <param name="item_compra_novo_id"></param>
        /// <returns>Objeto com as informações de acordo com o(s) parâmetro(s) informado(s).</returns>
        public Troca VerificarSeExiste(string compra_id, string item_compra_troca_id, string item_compra_novo_id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new TrocaQueries().SELECT +
                    @" WHERE compra_id = @compra_id
                         AND item_compra_troca_id = @item_compra_troca_id
                         AND item_compra_novo_id = @item_compra_novo_id ";

                return dbConnection.Query<Troca>(sQuery, new { compra_id, item_compra_troca_id, item_compra_novo_id }).FirstOrDefault();
            }
        }
    }
}