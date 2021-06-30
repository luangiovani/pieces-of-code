using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Services.CBL
{
    public class LogSistemaService : ServiceBase<LogSistema, LogSistemaViewModel>
    {
        private readonly LogSistemaRepository _repository;

        public LogSistemaService(LogSistemaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public void salvaLog(string descricao)
        {
            _repository.gravalog(descricao);
        }

        public ICollection<string> GetAllModels()
        {
            return _repository.GetAllModels().ToList();
        }
    }
}
