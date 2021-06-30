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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Verba
    /// </summary>
    public class VerbaRepository : BaseRepository<Verba>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public VerbaRepository(IConfiguration configuration, VerbaQueries queries)
            : base(configuration, queries) { }

        public ExtratoAtribuicoesDTO RelatorioDeExtratoDeAtribuicoes()
        {
            ExtratoAtribuicoesDTO objRetorno = new ExtratoAtribuicoesDTO();

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = new VerbaQueries().SELECT_ATRIBUICOES_REALIZADAS;
                dbConnection.Open();
                objRetorno.realizadas = dbConnection.Query<ExtratoAtribuicaoDTO>(sQuery);
            }

            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
               string sQuery = new VerbaQueries().SELECT_ATRIBUICOES_RECEBIDAS;
                dbConnection.Open();
                objRetorno.recebidas = dbConnection.Query<ExtratoAtribuicaoDTO>(sQuery);
            }

            return objRetorno;
        }

        public IEnumerable<VerbaDTO> Listar()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new VerbaQueries().SELECT_LISTAR;
                dbConnection.Open();
                return dbConnection.Query<VerbaDTO>(sQuery);
            }
        }

        public ColaboradorGestorSaldoVerba ObterSaldoVerbaGestor(string cs_gestor)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new VerbaQueries().SELECT_SALDO_VERBA_GESTOR;
                dbConnection.Open();
                return dbConnection.Query<ColaboradorGestorSaldoVerba>(sQuery, new { cs_gestor }).FirstOrDefault();
            }
        }

        public void AtualizarSaldoVerbaGestor(string id, decimal quantidade_pontos)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new VerbaQueries().UPDATE_SALDO_VERBA_GESTOR;
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { id, quantidade_pontos });
            }
        }
    }
}