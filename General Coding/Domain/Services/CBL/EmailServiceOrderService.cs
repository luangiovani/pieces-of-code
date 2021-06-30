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
    public class EmailServiceOrderService : ServiceBase<EmailServiceOrder, EmailServiceOrderViewModel>
    {
        private readonly EmailServiceOrderRepository _repository;
        public EmailServiceOrderService(EmailServiceOrderRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<EmailServiceOrder> GetByLocationOrderId(int locationOrderId)
        {
            return _repository.GetByLocationOrderId(locationOrderId);
        }
    }
}
