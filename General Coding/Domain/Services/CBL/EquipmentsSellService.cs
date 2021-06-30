using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Database.Entity.CBL;
using Framework.Domain.Model.CBL;
using Framework.Domain.Repository.CBL;
using Framework.Domain.Model;

namespace Framework.Domain.Services.CBL
{
    public class EquipmentsSellService : ServiceBase<EquipmentsSell, EquipmentsSellViewModel>
    {
        private readonly EquipmentsSellRepository _repository;

        public EquipmentsSellService(EquipmentsSellRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
