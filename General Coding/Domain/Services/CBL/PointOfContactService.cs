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
    public class PointOfContactService : ServiceBase<PointOfContact, PointOfContactViewModel>
    {
        private readonly PointOfContactRepository _repository;

        public PointOfContactService(PointOfContactRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
