using Microsoft.Extensions.Configuration;
using Database.Queries.APP;
using Database.Models.APP;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Domain.Repositories.APP
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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela PerfilMenuDeNavegacao
    /// </summary>
    public class PerfilMenuDeNavegacaoRepository : BaseRepository<PerfilMenuDeNavegacao>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public PerfilMenuDeNavegacaoRepository(IConfiguration configuration, PerfilMenuDeNavegacaoQueries queries) 
            : base(configuration, queries) { }

        /// <summary>
        /// Verifica se o vínculo entre Perfil e Menu e permissões que está sendo cadastrado já existe
        /// </summary>
        /// <param name="descricao">Nome da Aplicação</param>
        /// <returns>Objeto com a Aplicação de acordo com o nome informado.</returns>
        public PerfilMenuDeNavegacao VerificarSeExiste(string perfil_id, string menu_id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new PerfilMenuDeNavegacaoQueries().SELECT + @" WHERE perfil_id = @perfil_id AND menu_navegacao_id = @menu_id";

                return dbConnection.Query<PerfilMenuDeNavegacao>(sQuery, new { perfil_id, menu_id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Vincular / Desvincular (No cadastro ou atualização do Perfil)
        /// </summary>
        /// <param name="menus_id">Lista de Menus que este perfil tem acesso</param>
        /// <param name="perfil_id">Perfil</param>
        /// <param name="cs_colaborador">Colaborador que está realizando a transação</param>
        /// <param name="logOperacaoId">Identificador do Log</param>
        public void VincularDesvincular(List<string> menus_id, string perfil_id, string cs_colaborador, Guid logOperacaoId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new PerfilMenuDeNavegacaoQueries().VINCULAR_DESVINCULAR;
                var log = new LogTransacoes()
                {
                    id = Guid.NewGuid(),
                    log_operacao_id = logOperacaoId.ToString(),
                    data_hora_inicio = DateTime.Now,
                    data_hora_fim = null,
                    observacao = "VINCULAR-DESVINCULAR PERMISSÕES DE MENU",
                    transacao = sQuery
                };

                dbConnection.Execute(sQuery, new { cs_colaborador, perfil_id, menu_list = menus_id.ToArray() });

                log.data_hora_fim = DateTime.Now;
                sQuery = _logQueries.INSERT;
                dbConnection.Execute(sQuery, log);
            }
        }

        public IEnumerable<string> ListarMenusIdPerfil(string perfil_id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();

                string sQuery = new PerfilMenuDeNavegacaoQueries().SELECT_MENUS_ATIVO_PERFIL;
                return dbConnection.Query<string>(sQuery, new { perfil_id });
            }
        }
    }
}
