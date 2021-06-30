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
    public class MediaService : ServiceBase<Media, MediaViewModel>
    {
        private readonly MediaRepository _repository;

        public MediaService(MediaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ICollection<string> GetAllModels()
        {
            return _repository.GetAllModels().ToList();
        }
        /*
        public override MediaViewModel Update(MediaViewModel obj)
        {
            var stkService = new StockService(new StockRepository());
            var stocks = stkService.GetAllPrimitiveType(s => s.media_id == obj.media_id).ToList();
           
            foreach (var item in stocks)
            {
                item.stockAddress = obj.stockAddress;
                //if (item.material != obj.model)
                {
                    item.material = obj.model;
                    stkService.UpdatePrimitiveType(item);
                }
            }
            return base.Update(obj);
        }*/
    }
}
