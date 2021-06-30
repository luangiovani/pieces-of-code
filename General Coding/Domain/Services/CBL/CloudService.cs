using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;

namespace Framework.Domain.Services.CBL
{
   public class CloudService : ServiceBase<Cloud, CloudViewModel>
   {
      private readonly CloudRepository _repository;

      public CloudService(CloudRepository repository)
          : base(repository)
      {
         _repository = repository;
      }
      public void DeleteCloudAndDueDates(int idCloud)
      {
         _repository.DeleteCloudAndDueDates(idCloud);
      }
   }
}