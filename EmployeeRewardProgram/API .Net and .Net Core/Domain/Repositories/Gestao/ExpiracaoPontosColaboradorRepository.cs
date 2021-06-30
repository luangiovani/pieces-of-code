using Database.Models.Gestao;
using Database.Queries.Gestao;
using Microsoft.Extensions.Configuration;

namespace Domain.Repositories.Gestao
{
    public class ExpiracaoPontosColaboradorRepository : BaseRepository<ExpiracaoPontosColaborador>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public ExpiracaoPontosColaboradorRepository(IConfiguration configuration, ExpiracaoPontosColaboradorQueries queries)
            : base(configuration, queries) { }
    }
}
