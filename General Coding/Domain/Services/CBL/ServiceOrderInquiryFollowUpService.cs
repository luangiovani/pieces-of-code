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
    public class ServiceOrderInquiryFollowUpService : ServiceBase<ServiceOrderInquiryFollowUp, ServiceOrderInquiryFollowUpViewModel>
    {
        private readonly ServiceOrderInquiryFollowUpRepository _repository;

        public ServiceOrderInquiryFollowUpService(ServiceOrderInquiryFollowUpRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ICollection<ServiceOrderInquiryFollowUpViewModel> GetEstruturado()
        {
            List<ServiceOrderInquiryFollowUp> final = new List<ServiceOrderInquiryFollowUp>();

            var orders = _repository.GetAll();

            return Mapper.Map<ICollection<ServiceOrderInquiryFollowUpViewModel>>(orders);
        }
    }
}
