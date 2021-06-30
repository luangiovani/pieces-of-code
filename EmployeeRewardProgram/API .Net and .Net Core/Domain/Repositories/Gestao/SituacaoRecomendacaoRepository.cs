using Dapper;
using Database.Models.Gestao;
using Database.Queries.Gestao;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela SituacaoRecomendacao
    /// </summary>
    public class SituacaoRecomendacaoRepository : BaseRepository<SituacaoRecomendacao>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public SituacaoRecomendacaoRepository(IConfiguration configuration, SituacaoRecomendacaoQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Verifica se a Situacao da Recomendação que está sendo cadastrada já existe
        /// </summary>
        /// <param name="nome">Nome da Situacao da Recomendação</param>
        /// <returns>Objeto com a Aplicação de acordo com o nome informado.</returns>
        public SituacaoRecomendacao VerificarSeExiste(string nome)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new SituacaoRecomendacaoQueries().SELECT + @" WHERE nome = @nome";

                return dbConnection.Query<SituacaoRecomendacao>(sQuery, new { nome }).FirstOrDefault();
            }
        }
    }
}
