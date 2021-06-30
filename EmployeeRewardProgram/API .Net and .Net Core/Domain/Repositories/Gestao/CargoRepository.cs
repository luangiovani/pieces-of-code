using Dapper;
using Database.Models.APP;
using Database.Models.Gestao;
using Database.Queries.Gestao;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Cargo
    /// </summary>
    public class CargoRepository : BaseRepository<Cargo>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public CargoRepository(IConfiguration configuration, CargoQueries queries)
            : base(configuration, queries) { }

        /// <summary>
        /// Marcar o Cargo como Elegível / Inelegível
        /// </summary>
        /// <param name="id">Id do Cargo para Marcar</param>
        /// <param name="elegivel">true se Elegível e false se Inelegível</param>
        /// <param name="cs_colaborador_logado">Código do Colaborador que está realizando a transação</param>
        /// <param name="logOperacaoId">Id do Log para gravação de Log</param>
        public void ElegivelInelegivel(string id, bool elegivel, string cs_colaborador_logado, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = new CargoQueries().ELEGIVEL_INELEGIVEL;

                dbConnection.Open();

                var log = new LogTransacoes()
                {
                    id = Guid.NewGuid(),
                    log_operacao_id = logOperacaoId.ToString(),
                    data_hora_inicio = DateTime.Now,
                    data_hora_fim = null,
                    observacao = "MARCAR CARGO COMO " + (elegivel ? "ELEGÍVEL" : "INELEGÍVEL"),
                    transacao = sQuery
                };

                dbConnection.Execute(sQuery, new { id, elegivel, cs_colaborador_alteracao = cs_colaborador_logado });

                log.data_hora_fim = DateTime.Now;
                sQuery = _logQueries.INSERT;
                dbConnection.Execute(sQuery, log);
            }
        }
    }
}