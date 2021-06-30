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
    public class LocalMicrocomputadorService : ServiceBase<LocalMicrocomputador, LocalMicrocomputadorViewModel>
    {
        private readonly LocalMicrocomputadorRepository _repository;

        public LocalMicrocomputadorService(LocalMicrocomputadorRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
