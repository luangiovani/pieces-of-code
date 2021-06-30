using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository;

namespace Framework.Domain.Services.CBL
{
    public class ServiceOrderPaymentsService : ServiceBase<ServiceOrderPayments, ServiceOrderPaymentsViewModel>
    {
        private readonly ServiceOrderPaymentsRepository _repository;

        public ServiceOrderPaymentsService(ServiceOrderPaymentsRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
