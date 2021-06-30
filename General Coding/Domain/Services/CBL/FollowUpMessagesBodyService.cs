using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;

namespace Framework.Domain.Services.CBL
{
    public class FollowUpMessagesBodyService : ServiceBase<FollowUpMessagesBody, FollowUpMessagesBodyViewModel>
    {
        private readonly FollowUpMessagesBodyRepository _repository;

        public FollowUpMessagesBodyService(FollowUpMessagesBodyRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
