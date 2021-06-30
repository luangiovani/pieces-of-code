using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Framework.Database.Entity;
using Framework.Domain.Model;
using Framework.Domain.Repository;

namespace Framework.Domain.Services
{
    public class PerfilAreaService : ServiceBase<Perfil_Area, PerfilAreaViewModel>
    {
        private readonly PerfilAreaRepository _repository;

        public PerfilAreaService(PerfilAreaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public PerfilAreaViewModel GetPermissao(string controller, ICollection<string> perfilIds)
        {
            return Mapper.Map<PerfilAreaViewModel>(_repository.GetPermissao(controller, perfilIds));
        }

        public IEnumerable<PerfilAreaViewModel> GetByArea(Guid id_area)
        {
            return Mapper.Map<IEnumerable<PerfilAreaViewModel>>(_repository.Find(o => o.id_area == id_area));
        }

        public void DeleteByPerfil(string id_perfil)
        {
            var perfilArea = _repository.Find(o => o.id_perfil == id_perfil).ToList();
            perfilArea.ForEach(o =>
            {
                _repository.Delete(o);
            });
        }

        public void DeleteByArea(Guid id_area)
        {
            var perfilArea = _repository.Find(o => o.id_area == id_area).ToList();
            perfilArea.ForEach(o =>
            {
                _repository.Delete(o);
            });
        }
    }
}