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
    public class WouldBeAReferenceService : ServiceBase<WouldBeAReference, WouldBeAReferenceViewModel>
    {
        private readonly WouldBeAReferenceRepository _repository;

        public WouldBeAReferenceService(WouldBeAReferenceRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
