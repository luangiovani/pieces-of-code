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
    public class ServiceOrderShippingService : ServiceBase<ServiceOrderShipping, ServiceOrderShippingViewModel>
    {
        private readonly ServiceOrderShippingRepository _repository;

        public ServiceOrderShippingService(ServiceOrderShippingRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
