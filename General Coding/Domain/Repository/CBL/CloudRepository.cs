using Framework.Database.Entity.CBL;

namespace Framework.Domain.Repository.CBL
{
   public class CloudRepository : RepositoryBase<Cloud>
   {
      public void DeleteCloudAndDueDates(int idCloud)
      {
         string sSql = "DELETE FROM VencimentosCloud WHERE idCloud = " + idCloud + ";";
         sSql += "DELETE FROM Cloud WHERE idCloud = " + idCloud + ";";

         Db.Database.ExecuteSqlCommand(sSql);
      }

   }
}
