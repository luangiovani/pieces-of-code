using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;

namespace Framework.Domain.Services.CBL
{
    public class MadeInService : ServiceBase<MadeIn, MadeInViewModel>
    {
        private readonly MadeInRepository _repository;

        public MadeInService(MadeInRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
