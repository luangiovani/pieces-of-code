using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Framework.Database.Entity;

namespace Framework.Domain.Repository
{
    public class UsuarioRepository : RepositoryBase<ApplicationUser>
    {
        public IEnumerable<IdentityUserRole> GetByPerfil(string id_perfil)
        {
            return Db.Roles.Find(id_perfil).Users;
        }

        public void DeletePerfil(IdentityUserRole usuario_perfil)
        {
            Db.Roles.Find(usuario_perfil.RoleId).Users.Remove(usuario_perfil);
            Db.SaveChanges();
        }
    }
}