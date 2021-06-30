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
    public class ServiceOrderMediasService : ServiceBase<ServiceOrderMedias, ServiceOrderMediasViewModel>
    {
        private readonly ServiceOrderMediasRepository _repository;

        public ServiceOrderMediasService(ServiceOrderMediasRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public void DeleteByMedia(int media_id)
        {
            var orderMedias = _repository.Find(o => o.media_id == media_id).ToList();
            orderMedias.ForEach(o =>
            {
                _repository.Delete(o);
            });
        }
    }
}
