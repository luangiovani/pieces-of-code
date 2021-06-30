using Database.Models.Gestao;
using Domain.Repositories.Gestao;

namespace Domain.Services.Gestao
{
    public class ExpiracaoPontosColaboradorService : BaseService<ExpiracaoPontosColaborador>
    {
        private readonly ExpiracaoPontosColaboradorRepository _repository;

        public ExpiracaoPontosColaboradorService(ExpiracaoPontosColaboradorRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
