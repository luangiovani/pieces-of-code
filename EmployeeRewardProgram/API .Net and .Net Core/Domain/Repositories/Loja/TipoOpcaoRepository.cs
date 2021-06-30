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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela TipoOpcao
    /// </summary>
    public class TipoOpcaoRepository : BaseRepository<TipoOpcao>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public TipoOpcaoRepository(IConfiguration configuration, TipoOpcaoQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Verifica se o Tipo de Opção que está sendo cadastrado já existe
        /// </summary>
        /// <param name="nome">Nome do tipo de Opção</param>
        /// <returns>Objeto com o Tipo de Opção de acordo com o nome informado.</returns>
        public TipoOpcao VerificarSeExiste(string nome)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new TipoOpcaoQueries().SELECT + @" WHERE nome = @nome";

                return dbConnection.Query<TipoOpcao>(sQuery, new { nome }).FirstOrDefault();
            }
        }
    }
}
