using Database.Models.Loja;
using Domain.Repositories.Loja;

namespace Domain.Services.Loja
{
    /// <autor>
    /// Luan Fernandes - Ewave | 04/2019
    /// </autor>
    /// <tarefa>
    /// Development of the Employee Reward Program application
    /// </tarefa>
    /// <atividades>
    /// Service do Middleware para operações entre Front e Backend
    /// </atividades>
    /// <summary>
    /// Implementação da Interface de Service para chamadas das operações de banco de dados
    /// </summary>
    public class ProdutoOpcoesService : BaseService<ProdutoOpcoes>
    {
        /// <summary>
        /// Objeto do repositório de manipulação do service
        /// </summary>
        private readonly ProdutoOpcoesRepository _repository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository">Objeto injetado do repositório de banco de dados</param>
        public ProdutoOpcoesService(ProdutoOpcoesRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
