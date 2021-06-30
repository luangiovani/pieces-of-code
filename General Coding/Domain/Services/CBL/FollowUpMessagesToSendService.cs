using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;

namespace Framework.Domain.Services.CBL
{
    public class FollowUpMessagesToSendService : ServiceBase<FollowUpMessagesToSend, FollowUpMessagesToSendViewModel>
    {
        private readonly FollowUpMessagesToSendRepository _repository;

        public FollowUpMessagesToSendService(FollowUpMessagesToSendRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ICollection<FollowUpMessagesToSendViewModel> getAllFromView()
        {
            return _repository.getAllFromView().ToList();
        }

        public ICollection<FollowUpMessagesToSend> getAllPrimitiveByServiceOrderId(decimal serviceOrderId)
        {
            return _repository.getAllByServiceOrderId(serviceOrderId).OrderBy(a => a.followUpMessagesToSend_id).ToList();
        }

        public ICollection<FollowUpMessagesToSendViewModel> getAllByServiceOrderId(decimal serviceOrderId)
        {
            return Mapper.Map<ICollection<FollowUpMessagesToSendViewModel>>(_repository.getAllByServiceOrderId(serviceOrderId)).OrderBy(a => a.followUpMessagesToSend_id).ToList();
        }
    }
}
