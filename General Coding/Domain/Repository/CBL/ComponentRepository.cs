using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Framework.Domain.Repository.CBL
{
    public class ComponentRepository : RepositoryBase<Component>
    {
        public IEnumerable<string> GetAllModels()
        {
            string sSql = "SELECT Description FROM Component GROUP BY Description";
            return Db.Database.SqlQuery<string>(sSql).ToList();
        }
    }
}
