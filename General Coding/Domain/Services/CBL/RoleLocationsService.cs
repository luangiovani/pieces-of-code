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
    public class RoleLocationsService : ServiceBase<RoleLocations, RoleLocationsViewModel>
    {
        private readonly RoleLocationsRepository _repository;

        public RoleLocationsService(RoleLocationsRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public void DeleteByPerfil(string id_perfil)
        {
            var roleLocation = _repository.Find(o => o.id_perfil == id_perfil).ToList();
            roleLocation.ForEach(o =>
            {
                _repository.Delete(o);
            });
        }
    }
}
