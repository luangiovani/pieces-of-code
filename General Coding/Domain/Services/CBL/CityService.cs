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
    public class CityService : ServiceBase<City, CityViewModel>
    {
        private readonly CityRepository _repository;

        public CityService(CityRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public void InsereCidade(string UF, string Name)
        {
            _repository.InsereCidade(UF, Name);
        }

        public CityViewModel GetByNameAndUF(string city, string UF)
        {
            return _repository.GetByNameAndUF(city, UF);
        }

        public CityViewModel GetByName(string city)
        {
            return _repository.GetByName(city);
        }
    }
}
