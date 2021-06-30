using Database.Models.Venda;
using Database.Queries.Venda;
using Microsoft.Extensions.Configuration;

namespace Domain.Repositories.Venda
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
    /// Implementação da Interface de Repositório para operações de banco de dados da tabela ItemCompraOpcaoValor
    /// </summary>
    public class ItemCompraOpcaoValorRepository : BaseRepository<ItemCompraOpcaoValor>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configuration para obter a connectionstring do banco de dados</param>
        /// <param name="queries">Objeto Queries para obter os comandos de banco de dados que serão utilizados pelo Repositório</param>
        public ItemCompraOpcaoValorRepository(IConfiguration configuration, ItemCompraOpcaoValorQueries queries)
            : base(configuration, queries) { }
    }
}
