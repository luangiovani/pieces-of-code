using System.Collections.Generic;
using System.Linq;
using Framework.Database.Entity;

namespace Framework.Domain.Repository
{
    public class PerfilAreaRepository : RepositoryBase<Perfil_Area>
    {
        public Perfil_Area GetPermissao(string controller, ICollection<string> perfilIds)
        {
            return (from a in Db.Area
                join pa in Db.Perfil_Area on a.id_area equals pa.id_area
                join p in Db.Roles on pa.id_perfil equals p.Id
                where a.controller == controller && perfilIds.Contains(p.Id)
                select pa)
                .FirstOrDefault();
        }
    }
}