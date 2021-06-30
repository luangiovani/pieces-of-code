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
    public class ServiceOrderSmsService : ServiceBase<ServiceOrderSms, ServiceOrderSmsViewModel>
    {
        private readonly ServiceOrderSmsRepository _repository;

        public ServiceOrderSmsService(ServiceOrderSmsRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public int numeroContadorSms(string serviceOrderId)
        {
            return _repository.numeroContadorSms(serviceOrderId);
        }
    }
}
