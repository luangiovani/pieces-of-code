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
    public class PackageConditionsService : ServiceBase<PackageConditions, PackageConditionsViewModel>
    {
        private readonly PackageConditionsRepository _repository;

        public PackageConditionsService(PackageConditionsRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
