using Framework.Database.Entity.CBL;
using Framework.Domain.Model;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Services.CBL
{
    public class SmsMenuService : ServiceBase<SmsMenu, SmsMenuViewModel>
    {
        private readonly SmsMenuRepository _repository;

        public SmsMenuService(SmsMenuRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public int buscaQuantidadeCaracterMenuSms(string id)
        {
            return _repository.buscaQuantidadeCaracterMenuSms(id);
        }
    }
}
