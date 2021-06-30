using Microsoft.Extensions.Configuration;
using Database.Queries.APP;
using Database.Models.APP;

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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela Perfil
    /// </summary>
    public class PerfilRepository : BaseRepository<Perfil>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public PerfilRepository(IConfiguration configuration, PerfilQueries queries) 
            : base(configuration, queries) { }
    }
}
