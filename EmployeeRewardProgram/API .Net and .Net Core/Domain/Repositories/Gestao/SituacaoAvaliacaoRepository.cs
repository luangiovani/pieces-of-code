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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela SituacaoAvaliacao
    /// </summary>
    public class SituacaoAvaliacaoRepository : BaseRepository<SituacaoAvaliacao>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public SituacaoAvaliacaoRepository(IConfiguration configuration, SituacaoAvaliacaoQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Verifica se a Situacao Avaliacao que está sendo cadastrada já existe
        /// </summary>
        /// <param name="nome">Nome da Situacao Avaliacao</param>
        /// <returns>Objeto com a Aplicação de acordo com o nome informado.</returns>
        public SituacaoAvaliacao VerificarSeExiste(string nome)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new SituacaoAvaliacaoQueries().SELECT + @" WHERE nome = @nome";

                return dbConnection.Query<SituacaoAvaliacao>(sQuery, new { nome }).FirstOrDefault();
            }
        }
    }
}
