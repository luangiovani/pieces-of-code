using System.Collections.Generic;
using System.Linq;
using Framework.Database.Entity;

namespace Framework.Domain.Repository
{
    public class PerfilRepository : RepositoryBase<ApplicationRole>
    {
        public IEnumerable<string> GetPerfilUsuario(string id_usuario)
        {
            var usuario = (from u in Db.Users
                           where u.Id == id_usuario
                           select u).FirstOrDefault();

            return usuario.Roles.Select(o => o.RoleId);
        }
    }
}