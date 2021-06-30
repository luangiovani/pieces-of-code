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
    public class MakeService : ServiceBase<Make, MakeViewModel>
    {
        private readonly MakeRepository _repository;

        public MakeService(MakeRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
