using Database.Models.Gestao;
using Microsoft.Extensions.Configuration;
using Database.Queries.Gestao;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Domain.DTO.Gestao;

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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Avaliacao
    /// </summary>
    public class AvaliacaoRepository : BaseRepository<Avaliacao>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public AvaliacaoRepository(IConfiguration configuration, AvaliacaoQueries queries) 
            : base(configuration, queries) { }

        /// <summary>
        /// Recupera a Listagem de Avaliações Pendentes de Análise pelo Gestor
        /// </summary>
        /// <param name="cs_gestor">CS do Gestor que está listando as Avaliações</param>
        /// <returns>Listagem de Avaliações do Gestor que estão Pendentes</returns>
        public IEnumerable<AvaliacoesPendentesGestorDTO> ListarAvaliacoesPendendesGestor(string cs_gestor)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new AvaliacaoQueries().SELECT_AVALIACOES_GESTOR;

                return dbConnection.Query<AvaliacoesPendentesGestorDTO>(sQuery, new { cs_gestor });
            }
        }

        public IEnumerable<AvaliacoesRealizadasGestorDTO> ListarAvaliacoesRealizadasGestor(string cs_gestor)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new AvaliacaoQueries().SELECT_AVALIACOES_REALIZADAS_GESTOR;

                return dbConnection.Query<AvaliacoesRealizadasGestorDTO>(sQuery, new { cs_gestor });
            }
        }

        public IEnumerable<AvaliacaoDTO> ListarAvaliacoesPorRecomendacao(string recomendacao_id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new AvaliacaoQueries().SELECT_AVALIACOES_ATIVAS_RECOMENDACAO;

                return dbConnection.Query<AvaliacaoDTO>(sQuery, new { recomendacao_id });
            }
        }
    }
}
