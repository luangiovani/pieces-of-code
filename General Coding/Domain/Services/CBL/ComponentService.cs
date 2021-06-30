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
    public class ComponentService : ServiceBase<Component, ComponentViewModel>
    {
        private readonly ComponentRepository _repository;

        public ComponentService(ComponentRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public ICollection<string> GetAllModels()
        {
            return _repository.GetAllModels().ToList();
        }
    }
}
