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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela TaxaConversao
    /// </summary>
    public class TaxaConversaoRepository : BaseRepository<TaxaConversao>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public TaxaConversaoRepository(IConfiguration configuration, TaxaConversaoQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Obter do Banco de Dados a Taxa de Conversão que está ativa
        /// </summary>
        /// <returns>Retorna o objeto que está ativo no banco de dados</returns>
        public TaxaConversao ObterAtivo()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<TaxaConversao>(new TaxaConversaoQueries().SELECT_WHERE_ATIVO).FirstOrDefault();
            }
        }

        /// <summary>
        /// Verifica se a Taxa de Conversão que está sendo cadastrada já existe
        /// </summary>
        /// <param name="valor_moeda">Valor em Moeda da Taxa</param>
        /// <param name="fator">Valor do fator percentual de conversão</param>
        /// <returns>Objeto com a Taxa de acordo com o valor e fator informados.</returns>
        public TaxaConversao VerificarSeExiste(decimal valor_moeda, decimal fator)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new TaxaConversaoQueries().SELECT + @" WHERE valor_moeda = @valor_moeda AND fator = @fator";

                return dbConnection.Query<TaxaConversao>(sQuery, new { valor_moeda, fator }).FirstOrDefault();
            }
        }
    }
}
