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
   public class VencimentosCloudService : ServiceBase<VencimentosCloud, VencimentoCloudViewModel>
   {
      private readonly VencimentosCloudRepository _repository;

      public VencimentosCloudService(VencimentosCloudRepository repository)
          : base(repository)
      {
         _repository = repository;
      }

      public void AtualizarVencimentoPadraoCloud(int idCloud)
      {
         _repository.AtualizarVencimentoPadraoCloud(idCloud);
      }

      public IEnumerable<VencimentosCloud> GetByCloudId(int cloudId)
      {
         return _repository.GetAll(v => v.idCloud == cloudId).ToList();
      }
   }
}
