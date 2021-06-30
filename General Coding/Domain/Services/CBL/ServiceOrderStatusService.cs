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
    public class ServiceOrderStatusService : ServiceBase<ServiceOrderStatus, ServiceOrderStatusViewModel>
    {
        private readonly ServiceOrderStatusRepository _repository;

        public ServiceOrderStatusService(ServiceOrderStatusRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public List<ServiceOrderStatusUtil> GetOrdersByStatusAndCount()
        {
            return _repository.GetOrdersByStatusAndCount();
        }

        public List<ServiceOrderUsersUtil> GetOrdersByUsersAndCount()
        {
            return _repository.GetOrdersByUsersAndCount();
        }

        public int CountByStatus(string status)
        {
            return _repository.CountByStatus(status);
        }
    }
}
