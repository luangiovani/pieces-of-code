using System;
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
    public class CustomerContactsService : ServiceBase<CustomerContact, CustomerContactViewModel>
    {
        private readonly CustomerContactsRepository _repository;

        public CustomerContactsService(CustomerContactsRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
