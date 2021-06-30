using Dapper;
using Database.Models.Gestao;
using Database.Queries.Gestao;
using Domain.DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela TipoRecomendacao
    /// </summary>
    public class TipoRecomendacaoRepository : BaseRepository<TipoRecomendacao>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public TipoRecomendacaoRepository(IConfiguration configuration, TipoRecomendacaoQueries queries)
            : base(configuration, queries) { }

        public IEnumerable<BaseDTO> ListarTipoRecomendacaoCombo()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<TipoRecomendacao>(new TipoRecomendacaoQueries().SELECT)
                    .Where(c => c.ativo == true).Select(c =>
                    new BaseDTO()
                    {
                        id = c.id,
                        nome = c.descricao
                    }).ToList();
            }
        }

        public IEnumerable<TipoRecomendacao> ListarTodosAtivos()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<TipoRecomendacao>(new TipoRecomendacaoQueries().SELECT_WHERE_ATIVO);
            }
        }

        /// <summary>
        /// Verifica se o Tipo de Recomendação que está sendo cadastrado já existe
        /// </summary>
        /// <param name="nome">Nome do Tipo de Recomendação</param>
        /// <returns>Objeto com Tipo de Recomendação de acordo com o nome informado.</returns>
        public TipoRecomendacao VerificarSeExiste(string nome)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new TipoRecomendacaoQueries().SELECT + @" WHERE nome = @nome";

                return dbConnection.Query<TipoRecomendacao>(sQuery, new { nome }).FirstOrDefault();
            }
        }
    }
}

