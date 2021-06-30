using Framework.Database.Entity.CBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Repository.CBL
{
    public class MediaRepository : RepositoryBase<Media>
    {
        public void DeleteByQuery(int id)
        {
        }

        public IEnumerable<string> GetAllModels()
        {
            //string sSql = "SELECT LTRIM(RTRIM(Model)) Model FROM Media WHERE model IS NOT NULL GROUP BY Model";
            string sSql = @"  SELECT LTRIM(RTRIM(M.Model)) Model 
    FROM Media M
	JOIN MediaModels MM ON MM.model = M.model
   WHERE M.model IS NOT NULL 
GROUP BY M.Model";
            return Db.Database.SqlQuery<string>(sSql).ToList();
        }
    }
}
