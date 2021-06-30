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
    public class TypeRAIDService : ServiceBase<TypeOfRAID, TypeRAIDViewModel>
    {
        private readonly TypeRAIDRepository _repository;

        public TypeRAIDService(TypeRAIDRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
