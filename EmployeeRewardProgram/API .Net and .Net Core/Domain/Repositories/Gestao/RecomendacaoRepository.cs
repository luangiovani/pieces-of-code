using Dapper;
using Database.Models.Gestao;
using Database.Queries.Gestao;
using Domain.DTO.Gestao;
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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Recomendacao
    /// </summary>
    public class RecomendacaoRepository : BaseRepository<Recomendacao>
    {
        private readonly AvaliacaoRepository _avaliacaoRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public RecomendacaoRepository(IConfiguration configuration, RecomendacaoQueries queries, AvaliacaoRepository avaliacaoRepository)
            : base(configuration, queries) {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public IEnumerable<Avaliacao> AvaliacoesRecomendacao(string recomendacao_id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Avaliacao>(new AvaliacaoQueries().SELECT_AVALIACOES_ATIVAS_RECOMENDACAO, new { recomendacao_id });
            }
        }

        public IEnumerable<SituacaoRecomendacaoDTO> StatusRecomendacoesColaborador(string cs)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<SituacaoRecomendacaoDTO>(new RecomendacaoQueries().SELECT_POSICAO_RECOMENDACOES_APROVADAS, new { cs });
            }
        }

        public IEnumerable<SituacaoRecomendacaoDTO> StatusRecomendacoesGestor(string cs_solicitante)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<SituacaoRecomendacaoDTO>(new RecomendacaoQueries().SELECT_POSICAO_RECOMENDACOES_GESTOR, new { cs_solicitante });
            }
        }

        public DetalhesStatusRecomendacaoModelDTO DetalheRecomendacao(string id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                var recObj = dbConnection.Query<DetalhesStatusRecomendacaoModelDTO>(new RecomendacaoQueries().SELECT_DETALHE_RECOMENDACAO, new { id }).FirstOrDefault();
                if (recObj != null)
                {
                    recObj.Avaliacoes = _avaliacaoRepository.ListarAvaliacoesPorRecomendacao(recObj.id.ToString());
                }
                return recObj;
            }
        }

        public IEnumerable<DetalhesStatusRecomendacaoModelDTO> ObterRecomendacoesColaborador(string cs_colaborador, string cs_gestor)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<DetalhesStatusRecomendacaoModelDTO>(new RecomendacaoQueries().SELECT_DETALHE_PONTUACAO, new { cs_colaborador, cs_gestor });
            }
        }

        public IndicadorQuantitativoRecomendacoesDTO ObterQuantitativo()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<IndicadorQuantitativoRecomendacoesDTO>(new RecomendacaoQueries().SELECT_INDICADORES).FirstOrDefault();
            }
        }
    }
}
