using System.Collections.Generic;
using System.Linq;
using Framework.Database.Entity;

namespace Framework.Domain.Repository
{
    public class AreaRepository : RepositoryBase<Area>
    {
        public ICollection<Area> GetMenuUsuario(string id_usuario)
        {
            var usuario = (from u in Db.Users
                where u.Id == id_usuario
                select u).FirstOrDefault();

            var perfilIds = usuario.Roles.Select(o => o.RoleId);

            var lAreaAux = (from a in Db.Area
                join pa in Db.Perfil_Area on a.id_area equals pa.id_area
                join p in Db.Roles on pa.id_perfil equals p.Id
                where perfilIds.Contains(p.Id)
                select a);

            var lArea = lAreaAux
                .Where(o => o.id_area_mae == null)
                .OrderBy(o => o.ordem)
                .ToList();

            lArea.ForEach(x => x.area_filha.Clear());

            lArea.ForEach(x => x.area_filha = lAreaAux
                .Where(o => o.id_area_mae == x.id_area)
                .OrderBy(o => o.ordem)
                .ToList());

            return lArea;
        }
    }
}