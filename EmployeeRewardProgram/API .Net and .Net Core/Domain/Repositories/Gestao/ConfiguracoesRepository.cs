using Dapper;
using Database.Models.Gestao;
using Database.Queries.Gestao;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories.Gestao
{
    public class ConfiguracaoVerbaRepository : BaseRepository<ConfiguracaoDistribuicaoVerbas>
    {
        public ConfiguracaoVerbaRepository(IConfiguration configuration, ConfiguracaoDistribuicaoVerbasQueries queries)
            : base(configuration, queries) { }

        public ConfiguracaoDistribuicaoVerbas VerificarSeExiste()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new ConfiguracaoDistribuicaoVerbasQueries().SELECT;

                return dbConnection.Query<ConfiguracaoDistribuicaoVerbas>(sQuery).FirstOrDefault();
            }
        }
    }

    public class ConfiguracaoExpiracaoRepository : BaseRepository<ConfiguracaoExpiracaoPontos>
    {
        public ConfiguracaoExpiracaoRepository(IConfiguration configuration, ConfiguracaoExpiracaoPontosQueries queries)
            : base(configuration, queries) { }

        public ConfiguracaoExpiracaoPontos VerificarSeExiste()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new ConfiguracaoExpiracaoPontosQueries().SELECT;

                return dbConnection.Query<ConfiguracaoExpiracaoPontos>(sQuery).FirstOrDefault();
            }
        }
    }
}
