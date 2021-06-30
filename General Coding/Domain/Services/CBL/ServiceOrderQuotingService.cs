using AutoMapper;
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
    public class ServiceOrderQuotingService : ServiceBase<ServiceOrderQuoting, ServiceOrderQuotingViewModel>
    {
        private readonly ServiceOrderQuotingRepository _repository;

        public ServiceOrderQuotingService(ServiceOrderQuotingRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ICollection<ServiceOrderQuotingViewModel> GetEstruturado()
        {
            List<ServiceOrderQuoting> final = new List<ServiceOrderQuoting>();

            var orders = _repository.GetAll();

            return Mapper.Map<ICollection<ServiceOrderQuotingViewModel>>(orders);
        }
    }
}
