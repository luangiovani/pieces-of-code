using AutoMapper;
using System.Collections.Generic;
using Framework.Database.Entity;
using Framework.Domain.Model;
using Framework.Domain.Repository;

namespace Framework.Domain.Services
{
    public class PerfilService : ServiceBase<ApplicationRole, PerfilViewModel>
    {
         private readonly PerfilRepository _repository;

        public PerfilService(PerfilRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> GetPerfilUsuario(string id_usuario)
        {
            return _repository.GetPerfilUsuario(id_usuario);
        }

        public void DeleteById(string id_perfil)
        {
            var perfil = _repository.GetById(id_perfil);
            _repository.Delete(perfil);
        }
    }
}